using System.IO;
using MioShield.Common;
using MioShield.Config;
using UnityEngine;

namespace MioShield.Util;

public class FileUtil
{
    public static ConfigData ReadConfigFromFile(string configFolder, string filename)
    {
        string filePath = Path.Combine(configFolder, filename);
        string jsonStr;
        if (File.Exists(filePath))
        {
            jsonStr = File.ReadAllText(filePath).Replace("\r", "").Replace("\n", "").Replace("\t", "").Trim();
        }
        else
        {
            Plugin.Log.LogWarning("[MioShield] Config file not found. Creating a default one at: " + filePath);
            Directory.CreateDirectory(configFolder);
            ConfigData defaultConfig = new ConfigData { regenerationTime = CommonConstants.SHIELD_RECOVERY_PERIOD };
            jsonStr = JsonUtility.ToJson(defaultConfig, true);
            File.WriteAllText(filePath, jsonStr);
        }
        return JsonUtility.FromJson<ConfigData>(jsonStr);
    }
}