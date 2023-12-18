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
    }
}
