using GlobalEnums;
using GlobalSettings;
using HarmonyLib;
using MioShield.Behaviors;
using MioShield.Common;
using UnityEngine;

namespace MioShield.Patches;

public class ShieldPatches
{
    [HarmonyPatch(typeof(PlayerData), nameof(PlayerData.TakeHealth))]
    [HarmonyPostfix]
    public static void TakeDamagePostfix(PlayerData __instance, int amount, bool hasBlueHealth, bool allowFracturedMaskBreak)
    {
        if (!MioShieldBehavior.IsShielded && !MioShieldBehavior.IsSecondHitShielded) return;
        Plugin.Log.LogInfo("[MS] Player is taking damage while shielded - negating damage...");
        __instance.health += amount;
        MioShieldBehavior.BreakShield();
    }

    [HarmonyPatch(typeof(HeroController), nameof(HeroController.Respawn))]
    [HarmonyPostfix]
    public static void RespawnPostfix(HeroController __instance, Transform spawnPoint)
    {
        MioShieldBehavior.ResetShield();
    }

    [HarmonyPatch(typeof(PlayerData), nameof(PlayerData.SetBenchRespawn), typeof(string), typeof(string), typeof(int), typeof(bool))]
    [HarmonyPostfix]
    public static void SetBenchRespawnPostfix(PlayerData __instance, 
        string spawnMarker,
        string sceneName,
        int spawnType,
        bool facingRight)
    {
        MioShieldBehavior.ResetShield();
    }
}