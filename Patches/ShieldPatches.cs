using GlobalEnums;
using GlobalSettings;
using HarmonyLib;
using MioShield.Behaviors;
using UnityEngine;

namespace MioShield.Patches;

public class ShieldPatches
{
    [HarmonyPatch(typeof(PlayerData), nameof(PlayerData.TakeHealth))]
    [HarmonyPostfix]
    public static void TakeDamagePostfix(PlayerData __instance, int amount, bool hasBlueHealth, bool allowFracturedMaskBreak)
    {
        if (!MioShieldBehavior.IsShielded && !MioShieldBehavior.IsSecondHitShielded) return;
        Plugin.Log.LogInfo("Player is taking damage while shielded - negating damage...");
        __instance.health += amount;
        PreventFracturedMaskBreak();
        MioShieldBehavior.BreakShield();
        MioShieldBehavior.Instance.StartCoroutine(MioShieldBehavior.RecoverShield(5f));
        MioShieldBehavior.Instance.StartCoroutine(MioShieldBehavior.BlockSecondHit(0.5f));
    }

    [HarmonyPatch(typeof(HeroController), nameof(HeroController.Respawn))]
    [HarmonyPostfix]
    public static void RespawnPostfix(HeroController __instance, Transform spawnPoint)
    {
        ResetShield();
    }

    [HarmonyPatch(typeof(PlayerData), nameof(PlayerData.SetBenchRespawn), typeof(string), typeof(string), typeof(int), typeof(bool))]
    [HarmonyPostfix]
    public static void SetBenchRespawnPostfix(PlayerData __instance, 
        string spawnMarker,
        string sceneName,
        int spawnType,
        bool facingRight)
    {
        ResetShield();
    }

    private static void ResetShield()
    {
        Plugin.Log.LogInfo("Player is respawning or resting at a bench, resetting shield");
        MioShieldBehavior.ResetShield();
    }

    private static void PreventFracturedMaskBreak()
    {
        ToolItem fracturedMaskTool = Gameplay.FracturedMaskTool;
        if (fracturedMaskTool.IsEquipped)
        {
            ToolItemsData.Data savedData = fracturedMaskTool.SavedData with
            {
                AmountLeft = 1
            };
            fracturedMaskTool.SavedData = savedData;
        }
    }
}