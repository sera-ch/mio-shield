using System.Collections;
using GlobalSettings;
using UnityEngine;

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
        Plugin.Log.LogInfo("Shield broken! Recovering...");
        yield return new WaitForSeconds(delay);
        Plugin.Log.LogInfo("Shield recovered");
        ResetShield();
    }

    public static IEnumerator BlockSecondHit(float period)
    {
        IsSecondHitShielded = true;
        Plugin.Log.LogInfo("Shielded from consecutive hits!");
        yield return new WaitForSeconds(period);
        Plugin.Log.LogInfo("Stopped being shielded from consecutive hits...");
        IsSecondHitShielded = false;
    }

    public static void ResetShield()
    {
        IsShielded = true;
        IsSecondHitShielded = false;
    }

    public static void BreakShield()
    {
        IsShielded = false;
    }
}