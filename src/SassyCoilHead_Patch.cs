using HarmonyLib;
using UnityEngine;

namespace SassyCoilheadMod.Patch
{
    [HarmonyPatch(typeof(EnemyAI))]
    internal class SassyCoilHead_Patch
    {
        [HarmonyPatch("Start")]
        [HarmonyPrefix]
        private static void CheckCoilhead(EnemyAI __instance)
        {
            if (__instance is SpringManAI)
            {
                SassyCoilhead_PluginEntry.Log.LogInfo("Found the coilhead.");
                SassyCoilhead_Helpers.CreateDetectorOn(__instance as SpringManAI);
            }
        }
    }

#if DEBUG
    [HarmonyPatch(typeof(RoundManager))]
    internal class SassyCoilhead_DebugPatch
    {
        [HarmonyPatch("LoadNewLevel")]
        [HarmonyPrefix]
        private static bool ChangeSpawn(ref SelectableLevel newLevel)
        {
            foreach (SpawnableEnemyWithRarity enemy in newLevel.Enemies)
            {
                enemy.rarity = 0;
                if (enemy.enemyType.enemyPrefab.GetComponent<SpringManAI>() != null)
                {
                    enemy.rarity = 999;
                }
            }
            SassyCoilhead_PluginEntry.Log.LogInfo("DEBUG: All enemies but Coilhead disabled.");
            return true;
        }
    }
#endif
}
