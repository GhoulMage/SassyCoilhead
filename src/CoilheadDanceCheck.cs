using System;
using BepInEx.Logging;
using UnityEngine;

namespace SassyCoilheadMod
{
    public class CoilheadDanceCheck : MonoBehaviour
    {
        public static event Action<SpringManAI> OnCoilheadDance;

        private SpringManAI _coilhead;
        private Animator _coilheadAnimator;
        private RuntimeAnimatorController _originalController;
        private RuntimeAnimatorController _danceController;
        private float _checkTime;
        private float _timeNearPlayer;
        private bool _dancing;

        public bool IsDancing => _dancing;

        const float TurnToPlayerWhileDancingSpeed = 5f;
        const float CheckTimeInterval = 2f;

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
            _checkTime = CheckTimeInterval;

            _danceController = SassyCoilhead_PluginEntry.DanceControllerAsset;
        }

        private bool NearAnyPlayer()
        {
            var nearestPlayer = _coilhead?.GetClosestPlayer(false, false, false);
            if (nearestPlayer != null)
            {
                Vector3 closestPlayerPosition = nearestPlayer.transform.position;
                //Sphere check
                return (_coilhead.transform.position - closestPlayerPosition).sqrMagnitude <= (SassyCoilhead_PluginEntry.DetectionRadius * SassyCoilhead_PluginEntry.DetectionRadius);
            }

            return false;
        }

        private void LateUpdate()
        {
            //Shouldn't happen because if this script gets attached to the coilhead, it should be gone alongside the entity whenever it gets destroyed.
            if (_coilhead == null)
            {
                Destroy(this);
                return;
            }

            if (_dancing)
            {
                if (!NearAnyPlayer())
                {
                    _coilheadAnimator.runtimeAnimatorController = _originalController;
                    _dancing = false;
                }
                else
                {
                    Vector3 deltaTowardsNearestPlayer = _coilhead.GetClosestPlayer(false, false, false).transform.position - _coilhead.transform.position;
                    _coilhead.transform.forward = Vector3.RotateTowards(_coilhead.transform.forward, deltaTowardsNearestPlayer, TurnToPlayerWhileDancingSpeed * Time.deltaTime, 0f);
                }

                return;
            }


            if (NearAnyPlayer())
            {
                _timeNearPlayer += Time.deltaTime;
                if (_timeNearPlayer >= SassyCoilhead_PluginEntry.DanceWaitMinTime)
                {
                    _checkTime -= Time.deltaTime;
                    if (_checkTime > 0)
                        return;
                    _checkTime = CheckTimeInterval;

                    AttemptDance();
                }
            }
            else
            {
                _timeNearPlayer = 0f;
            }
        }

        private void AttemptDance()
        {
            if (UnityEngine.Random.value <= SassyCoilhead_PluginEntry.DanceChance)
            {
                _dancing = true;
                _coilheadAnimator.runtimeAnimatorController = _danceController;
                OnCoilheadDance?.Invoke(_coilhead);
            }
        }
    }
}
