using System;
using BepInEx;
using BepInEx.Configuration;
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
        public const string VERSION = "0.1.0";

        internal ConfigEntry<float> _config_detectionRadius;
        internal ConfigEntry<byte> _config_danceChance;

        public static float DetectionRadius { get; private set; }
        public static float DanceChance { get; private set; }
        internal static AnimationClip DanceClipAsset { get; private set; }

        private void Awake()
        {
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
            DanceClipAsset = bundle.LoadAsset<AnimationClip>("Assets/sassycoilhead_dance.anim");
            if (DanceClipAsset == null)
            {
                Logger.LogInfo("Failed to load Coilhead's dance...");
                return;
            }
            Logger.LogInfo("Succesfully loaded Coilhead's dance!");
        }
        private void FetchConfigurationValues()
        {
            _config_detectionRadius = Config.Bind(ConfigName, "DetectionRange", 5.5f, "The player has to be inside this range to see the coilhead dance.");
            _config_danceChance = Config.Bind<byte>(ConfigName, "Dance Chance", (byte)255 / 4, "Chance between 0 and 255 that the coilhead will dance.");

            DetectionRadius = _config_detectionRadius.Value;
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
