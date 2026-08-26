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
        if (!MioShieldBehavior.IsShielded) return;
        Plugin.Log.LogInfo("Player is taking damage while shielded - negating damage...");
        __instance.health += amount;
        PreventFracturedMaskBreak();
        MioShieldBehavior.IsShielded = false;
        MioShieldBehavior.Instance.StartCoroutine(MioShieldBehavior.RecoverShield(5f));
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