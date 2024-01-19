using System;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace SassyCoilheadMod
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInProcess("Lethal Company.exe")]
    public class SassyCoilhead_PluginEntry : BaseUnityPlugin
    {
        public const string GUID = "ghoulmage.funny.sassycoilhead";
        public const string NAME = "Sassy Coilhead";
        public const string VERSION = "1.0.1";

        internal ConfigEntry<float> _config_detectionRadius;
        internal ConfigEntry<float> _config_danceWaitMinTime;
        internal ConfigEntry<byte> _config_danceChance;

        internal static ManualLogSource Log;

        public static float DetectionRadius { get; private set; }
        public static float DanceWaitMinTime { get; private set; }
        public static float DanceChance { get; private set; }
        internal static RuntimeAnimatorController DanceControllerAsset { get; private set; }
        internal static AnimationClip DanceClipAsset { get; private set; }

        private void Awake()
        {
            Log = Logger;
            Logger.LogInfo($"Mod {NAME} ver {VERSION} ({GUID}) is loaded!");
            DoPatch.This(GUID);

            FetchConfigurationValues();
            LoadDanceFromAssetBundle();
        }

        const string ConfigName = "SassyCoilhead";

        private void LoadDanceFromAssetBundle()
        {
            AssetBundle bundle = AssetBundle.LoadFromFile(Paths.PluginPath + "\\GhoulMage\\funny\\sassycoilhead");
            if (bundle == null)
            {
                Logger.LogError("Failed to load Coilhead's dance...");
                return;
            }
            DanceClipAsset = bundle.LoadAsset<AnimationClip>("Assets/sassy_dance.anim");
            if (DanceClipAsset == null)
            {
                Logger.LogInfo("Failed to load Coilhead's dance...");
                return;
            }
            DanceControllerAsset = bundle.LoadAsset<RuntimeAnimatorController>("Assets/sassy_dance_controller.controller");
            if (DanceControllerAsset == null)
            {
                Logger.LogInfo("Failed to load Coilhead's dance...");
                return;
            }
            Logger.LogInfo("Succesfully loaded Coilhead's dance!");
            bundle.Unload(false);
        }
        private void FetchConfigurationValues()
        {
            _config_detectionRadius = Config.Bind(ConfigName, "Detection Range (meters)", 9.5f, "Coilhead has to be inside this range of a player to check if it should dance.");
            _config_danceWaitMinTime = Config.Bind(ConfigName, "Minimum Wait Time (seconds)", 5f, "Minimum time to stay still without dancing near any player.");
            _config_danceChance = Config.Bind<byte>(ConfigName, "Dance Chance (0-255)", 255 / 6, "Chance that the coilhead will dance every 2 seconds.");

            DetectionRadius = _config_detectionRadius.Value;
            DanceWaitMinTime = _config_danceWaitMinTime.Value;
            DanceChance = _config_danceChance.Value / 255f;
        }
    }

    public static class DoPatch
    {
        public static void This(string guid)
        {
            new Harmony(guid).PatchAll();
        }
    }
}
