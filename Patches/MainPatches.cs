using System;
using HarmonyLib;

namespace MioShield.Patches;

/**
 * Manager for all the patches
 */
public class MainPatches
{
    public static void PatchAll()
    {
        {
            try
            {
                Harmony.CreateAndPatchAll(typeof(ShieldPatches), null);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError("Harmony Patching Error: " + ex);
            }
        }
    }
}