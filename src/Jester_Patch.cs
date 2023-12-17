using BepInEx;
using HarmonyLib;
using UnityEngine;
using UnityEngine.AI;

namespace DisasterBoxMod.Patch
{
    [HarmonyPatch(typeof(EnemyAI))]
    internal class EnemyAI_Patch
    {
        [HarmonyPatch("SwitchToBehaviourState")]
        [HarmonyPrefix]
        static void AddLoopingOnTop(ref EnemyAI __instance, int stateIndex)
        {
            if (!DisasterBoxMod_Plugin.LoopThemeIfBoxIsOpen.Value)
                return;

#if DEBUG
            DisasterBoxMod_Plugin.Log.LogInfo($"Something something changing state! New state: {stateIndex}");
            DisasterBoxMod_Plugin.Log.LogInfo("Is it a jester? " + (__instance is JesterAI ? "Yes" : "No"));
#endif

            if (__instance is not JesterAI)
                return;// true;

#if DEBUG
            DisasterBoxMod_Plugin.Log.LogInfo($"Jester changing state! New state: {stateIndex}");
#endif

            if (stateIndex == 2)
            {
#if DEBUG
                DisasterBoxMod_Plugin.Log.LogInfo("Jester going psycho mode!");
#endif
                __instance.GetComponentInChildren<DisasterBoxLoopBehaviour>().Play();
                return;// true;
            }
#if DEBUG
            DisasterBoxMod_Plugin.Log.LogInfo("Jester is still calm...");
#endif

            __instance.GetComponentInChildren<DisasterBoxLoopBehaviour>().Stop();

            //return true;
        }
    }
    [HarmonyPatch(typeof(JesterAI))]
    internal class Jester_Patch
    {
        [HarmonyPatch("Start")]
        [HarmonyPrefix]
        static void CreateLooper(JesterAI __instance)
        {
#if DEBUG
            DisasterBoxMod_Plugin.Log.LogInfo("Jester spawned!");
#endif

            if (!DisasterBoxMod_Plugin.LoopThemeIfBoxIsOpen.Value)
                return;

            GameObject looper = new GameObject("Disaster Box Loop");
            looper.transform.parent = __instance.transform;
            looper.transform.localPosition = Vector3.zero;

            looper.AddComponent<DisasterBoxLoopBehaviour>();
        }

        [HarmonyPatch("SetJesterInitialValues")]
        [HarmonyPostfix]
        private static void ReplaceMusic(JesterAI __instance)
        {
#if DEBUG
            DisasterBoxMod_Plugin.Log.LogInfo("Replacing Jester Themes...");
#endif

            __instance.popUpTimer = 39.5f;
            __instance.popGoesTheWeaselTheme = DisasterBoxMod_Plugin.DisasterBox_Theme_Flat;
            __instance.popUpSFX = DisasterBoxMod_Plugin.DisasterBox_Theme_PopUp;
        }
    }

#if DEBUG
    [HarmonyPatch(typeof(RoundManager))]
    internal class Jester_DebugPatch
    {
        [HarmonyPatch("LoadNewLevel")]
        [HarmonyPrefix]
        private static bool ChangeSpawn(ref SelectableLevel newLevel)
        {
            foreach (SpawnableEnemyWithRarity enemy in newLevel.Enemies)
            {
                if (enemy.enemyType.enemyPrefab.GetComponent<JesterAI>() != null)
                {
                    enemy.rarity = 999;
                    enemy.enemyType.numberSpawnedFalloff = AnimationCurve.Constant(0f, 1f, 0f);
                    enemy.enemyType.useNumberSpawnedFalloff = false;

                    enemy.enemyType.MaxCount = 10;
                    enemy.enemyType.PowerLevel = 1;

                    enemy.enemyType.isDaytimeEnemy = true;
                    enemy.enemyType.probabilityCurve = AnimationCurve.Constant(0f, 1f, 1f);
                    DisasterBoxMod_Plugin.Log.LogInfo("There's Jesters in this level.");
                    continue;
                }
                enemy.rarity = 0;
                enemy.enemyType.isOutsideEnemy = false;
                enemy.enemyType.isDaytimeEnemy = false;
            }

            DisasterBoxMod_Plugin.Log.LogInfo("DEBUG: All enemies but Jester disabled.");
            return true;
        }
    }
#endif
}
