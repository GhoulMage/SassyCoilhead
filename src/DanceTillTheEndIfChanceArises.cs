using System;
using UnityEngine;

namespace SassyCoilheadMod
{
    public class CoilheadDanceCheck : MonoBehaviour
    {
        public static event Action<SpringManAI> OnCoilheadDance;

        private SpringManAI _coilhead;
        private Animator _coilheadAnimator;
        private RuntimeAnimatorController _originalController;
        private AnimatorOverrideController _danceController;
        private float _checkTime;
        private bool _dancing;

        public void StopDance()
        {
            if (!_dancing)
                return;
            _coilheadAnimator.runtimeAnimatorController = _originalController;
            _dancing = false;
        }

        internal void SetSpringMan(SpringManAI springMan)
        {
            _coilhead = springMan;
            _coilheadAnimator = _coilhead.GetComponentInChildren<Animator>();
            _originalController = _coilheadAnimator.runtimeAnimatorController;
            _checkTime = 2f;

            _danceController = SassyCoilhead_Helpers.CreateAnimatorOverride(_coilheadAnimator, SassyCoilhead_PluginEntry.DanceClipAsset);
        }

        private bool NearAnyPlayer()
        {
            Vector3 closestPlayerPosition = _coilhead.GetClosestPlayer(false, false, false).serverPlayerPosition;
            //Sphere check
            return (_coilhead.serverPosition - closestPlayerPosition).sqrMagnitude <= (SassyCoilhead_PluginEntry.DetectionRadius * SassyCoilhead_PluginEntry.DetectionRadius);
        }

        private void LateUpdate()
        {
            if (_dancing)
            {
                if (!NearAnyPlayer())
                {
                    _coilheadAnimator.runtimeAnimatorController = _originalController;
                    _dancing = false;
                }
                return;
            }

            _checkTime -= Time.deltaTime;
            if (_checkTime > 0)
                return;
            _checkTime = 2f;
            AttemptDance();
        }

        private void AttemptDance()
        {
            if (UnityEngine.Random.value <= SassyCoilhead_PluginEntry.DanceChance)
            {
                //Within range
                if (NearAnyPlayer())
                {
                    _dancing = true;
                    _coilheadAnimator.runtimeAnimatorController = _danceController;
                    OnCoilheadDance?.Invoke(_coilhead);
                }
            }
        }
    }
}
