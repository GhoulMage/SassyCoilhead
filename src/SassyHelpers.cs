using UnityEngine;
using System.Collections.Generic;

namespace SassyCoilheadMod
{
    internal static class SassyCoilhead_Helpers
    {
        internal static void CreateDetectorOn(SpringManAI target)
        {
            if (target == null)
            {
                SassyCoilhead_PluginEntry.Log.LogWarning("Attempting to create a dance detector on a null entity. Aborting.");
                return;
            }

            CoilheadDanceCheck danceScript = target.gameObject.AddComponent<CoilheadDanceCheck>();
            danceScript.SetSpringMan(target);
        }
    }
}
