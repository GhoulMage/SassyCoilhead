using System;
using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using GhoulMage.LethalCompany;
using UnityEngine;

namespace DisasterBoxMod
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInProcess("Lethal Company.exe")]
    [BepInDependency(LC_API.MyPluginInfo.PLUGIN_GUID)]
    public class DisasterBoxMod_Plugin : GhoulMagePlugin
    {
        public const string GUID = "ghoulmage.funny.disasterbox";
        public const string NAME = "Disaster Box";
        public const string VERSION = "0.1.0";

        const string ConfigName = "DisasterBox";

        internal static ManualLogSource Log;
        internal static ConfigFile configFile;

        internal static ConfigEntry<bool> LoopThemeIfBoxIsOpen;
        internal static ConfigEntry<float> LoopAudioRange;
        internal static ConfigEntry<float> LoopVolume;

        internal static AudioClip DisasterBox_Theme_Flat;
        internal static AudioClip DisasterBox_Theme_PopUp;
        internal static AudioClip DisasterBox_Theme_Loop;

        protected override LethalGameVersions GameCompatibility => new LethalGameVersions("v40", "v45");

        protected override Assembly AssemblyToPatch => Assembly.GetExecutingAssembly();

        private static void LoadFromAssetBundle()
        {
            Log.LogInfo("Loading Disaster Box music...");

            DisasterBox_Theme_Flat = LC_API.BundleAPI.BundleLoader.GetLoadedAsset<AudioClip>("Assets/letha/disasterbox_1.ogg");
            if (DisasterBox_Theme_Flat == null)
            {
                Log.LogError("Failed to load Disaster Box FLAT!");
                return;
            }
            DisasterBox_Theme_Flat.LoadAudioData();

            DisasterBox_Theme_PopUp = LC_API.BundleAPI.BundleLoader.GetLoadedAsset<AudioClip>("Assets/letha/disasterbox_2_popup.ogg");
            if (DisasterBox_Theme_PopUp == null)
            {
                Log.LogError("Failed to load Disaster Box Popup!");
                return;
            }
            DisasterBox_Theme_PopUp.LoadAudioData();

            DisasterBox_Theme_Loop = LC_API.BundleAPI.BundleLoader.GetLoadedAsset<AudioClip>("Assets/letha/disasterbox_2_loop.ogg");
            if (DisasterBox_Theme_Loop == null)
            {
                Log.LogError("Failed to load Disaster Box Loop!");

                if (LoopThemeIfBoxIsOpen.Value)
                    return;

                Log.LogInfo("But looping is deactivated anyways so continuing...");
                DisasterBox_Theme_Loop.LoadAudioData();
            }

            Log.LogInfo("Disaster Box music loaded..!");
        }
        private static void GetConfig()
        {
            LoopThemeIfBoxIsOpen = configFile.Bind<bool>(ConfigName, "Loop Theme", true, "Loops the metal part of the song as long as the box is open?");
            LoopAudioRange = configFile.Bind<float>(ConfigName, "Loop Audio Range", 9.5f, "Audible range of the looping part.");
            LoopVolume = configFile.Bind<float>(ConfigName, "Loop Volume", 0.65f, "Volume of the looping part, if enabled. Between 0 and 1.");
        }

        protected override void Initialize()
        {
            Log = Logger;
            configFile = Config;
            Startup(GUID, NAME, VERSION, OnSuccesfulLoad);
        }

        static void OnSuccesfulLoad()
        {
            GetConfig();
            LoadFromAssetBundle();
        }
    }

    //Added when looping is enabled
    internal class DisasterBoxLoopBehaviour : MonoBehaviour
    {
        AudioSource _audioSource;

        private void Awake()
        {
            DisasterBoxMod_Plugin.Log.LogInfo("Creating AudioSource for DisasterBox Loop...");
            _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.playOnAwake = false;
            _audioSource.Stop();
            _audioSource.clip = DisasterBoxMod_Plugin.DisasterBox_Theme_Loop;
            _audioSource.rolloffMode = AudioRolloffMode.Logarithmic;

            float baseAudibleDistance = 0.5f + DisasterBoxMod_Plugin.LoopAudioRange.Value;
            _audioSource.maxDistance = baseAudibleDistance;
            _audioSource.minDistance = baseAudibleDistance * 0.15f;
        }

        public void Play()
        {
            DisasterBoxMod_Plugin.Log.LogInfo("Playing DisasterBox Loop!");
            _audioSource.Stop();
            _audioSource.clip = DisasterBoxMod_Plugin.DisasterBox_Theme_Loop;
            _audioSource.volume = Mathf.Clamp01(DisasterBoxMod_Plugin.LoopVolume.Value);
            _audioSource.loop = true;
            _audioSource.Play();
        }
        public void Stop()
        {
            _audioSource.Stop();
        }
    }
}
