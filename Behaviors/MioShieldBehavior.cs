using System.Collections;
using GlobalSettings;
using MioShield.Common;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MioShield.Behaviors;

public class MioShieldBehavior : MonoBehaviour
{
    
    public static MioShieldBehavior Instance { get; set; }
    
    public static bool IsShielded { get; private set; }
    public static bool IsSecondHitShielded { get; private set; }

    private void Awake()
    {
        Instance = this;
        IsShielded = true;
        IsSecondHitShielded = false;
    }

    public static IEnumerator RecoverShield(float delay)
    {
        Plugin.Log.LogInfo("[MS] Shield broken! Recovering...");
        yield return new WaitForSeconds(delay);
        Plugin.Log.LogInfo("[MS] Shield recovered");
        ResetShield();
    }

    public static IEnumerator BlockSecondHit(float period)
    {
        IsSecondHitShielded = true;
        Plugin.Log.LogInfo("[MS] Shielded from consecutive hits!");
        yield return new WaitForSeconds(period);
        Plugin.Log.LogInfo("[MS] Stopped being shielded from consecutive hits...");
        IsSecondHitShielded = false;
    }

    public static void ResetShield()
    {
        Plugin.Log.LogInfo("[MS] Shield Reset");
        IsShielded = true;
        IsSecondHitShielded = false;
        CollectableItemHeroReaction.DoReaction(new Vector2(0.0f, -0.76f));
    }

    public static void BreakShield()
    {
        IsShielded = false;
        Instance.StartCoroutine(RecoverShield(CommonConstants.SHIELD_RECOVERY_PERIOD));
        Instance.StartCoroutine(BlockSecondHit(CommonConstants.SHIELD_DOUBLE_HIT_IMMUNITY_PERIOD));
        PreventFracturedMaskBreak();
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