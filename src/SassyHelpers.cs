using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Mono.Cecil.Cil;

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

        internal static string FindFolderIn(string folderToSearch, string relativePath)
        {
            string result = Path.Join(folderToSearch, relativePath);
            if (Directory.Exists(result))
            {
                return result;
            }

            foreach (string subDirectories in Directory.EnumerateDirectories(folderToSearch))
            {
                result = Path.Join(subDirectories, relativePath);
                if (Directory.Exists(result))
                {
                    return result;
                }
            }
            return null;
        }
    }
}
