using System.Collections;
using UnityEngine;

namespace MioShield.Behaviors;

public class MioShieldBehavior : MonoBehaviour
{
    
    public static MioShieldBehavior Instance { get; set; }
    
    public static bool IsShielded { get; set; }

    private void Awake()
    {
        Instance = this;
        IsShielded = true;
    }

    public static IEnumerator RecoverShield(float delay)
    {
        Plugin.Log.LogInfo("Shield broken! Recovering...");
        yield return new WaitForSeconds(delay);
        Plugin.Log.LogInfo("Shield recovered");
        IsShielded = true;
    }
}