using UnityEngine;
using System.Collections.Generic;

namespace SassyCoilheadMod
{
    internal static class SassyCoilhead_Helpers
    {
        internal static void CreateDetectorOn(SpringManAI target)
        {
            CoilheadDanceCheck danceScript = target.gameObject.AddComponent<CoilheadDanceCheck>();
            danceScript.SetSpringMan(target);
        }

        internal static AnimatorOverrideController CreateAnimatorOverride(Animator animator, AnimationClip clip)
        {
            AnimatorOverrideController result = new AnimatorOverrideController(animator.runtimeAnimatorController);
            var newAnimationClips = new List<KeyValuePair<AnimationClip, AnimationClip>>();
            foreach (var animationClip in result.animationClips)
            {
                newAnimationClips.Add(new KeyValuePair<AnimationClip, AnimationClip>(animationClip, clip));
            }
            result.ApplyOverrides(newAnimationClips);

            return result;
        }
    }
}
