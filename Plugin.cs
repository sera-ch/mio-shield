using BepInEx;
using BepInEx.Logging;
using MioShield.Behaviors;
using MioShield.Patches;
using UnityEngine;

namespace MioShield;

[BepInPlugin("com.sera.MioShield", "Mio Shield", "1.0.0")]
public class Plugin : BaseUnityPlugin
{
    public static Plugin Instance { get; private set; }
    public static ManualLogSource Log { get; private set; }
    
    private void Awake()
    {
        Instance = this;
        Log = base.Logger;
        GameObject engineContainer = new GameObject("LE_RematchManagerObject");
        DontDestroyOnLoad(engineContainer);
        engineContainer.AddComponent<MioShieldBehavior>();
        MainPatches.PatchAll();
    }
}