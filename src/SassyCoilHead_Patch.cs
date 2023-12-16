using HarmonyLib;
using UnityEngine;

namespace SassyCoilheadMod.Patch
{
    internal class SassyCoilHead_PatchCoilhead
    {
        [HarmonyPatch(typeof(EnemyAI), "Start")]
        [HarmonyPostfix]
        private static void StartPostFix(EnemyAI enemyInstance)
        {
            if (enemyInstance is SpringManAI)
            {
                Debug.Log("Found the coilhead.");
                SassyCoilhead_Helpers.CreateDetectorOn(enemyInstance as SpringManAI);
            }
        }
    }
}
