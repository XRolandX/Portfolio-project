using UnityEditor;
using UnityEngine;
using Unity.Burst;
using System.IO;

public class DisableBurstAndClean
{
    [MenuItem("Tools/Burst/Wipe + Disable Burst")]
    public static void Execute()
    {
        Debug.Log("=== 🔧 B U R S T   D I S A B L E   &   W I P E ===");

        // 1. Disable Burst options at runtime
        BurstCompiler.Options.EnableBurstCompilation = false;
        BurstCompiler.Options.EnableBurstSafetyChecks = false;

        // 2. Add define symbol DISABLE_BURST_COMPILATION
        AddDefine("DISABLE_BURST_COMPILATION", BuildTargetGroup.Standalone);
        AddDefine("DISABLE_BURST_COMPILATION", BuildTargetGroup.Android);

        Debug.Log("✅ Burst compilation and safety checks DISABLED.");
        Debug.Log("✅ Define symbol 'DISABLE_BURST_COMPILATION' added to Standalone & Android.");

        // 3. Delete Burst cache folder
        string burstCachePath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), @"Unity\cache\burst");

        if (Directory.Exists(burstCachePath))
        {
            try
            {
                Directory.Delete(burstCachePath, true);
                Debug.Log($"🧹 Burst cache cleared: {burstCachePath}");
            }
            catch (System.Exception e)
            {
                Debug.LogWarning($"⚠️ Failed to delete Burst cache: {e.Message}");
            }
        }
        else
        {
            Debug.Log("ℹ️ No Burst cache folder found.");
        }

        AssetDatabase.Refresh();
        Debug.Log("=== ✅ Burst fully disabled. Restart Unity to complete the process. ===");
    }

    private static void AddDefine(string define, BuildTargetGroup group)
    {
        string currentDefines = PlayerSettings.GetScriptingDefineSymbolsForGroup(group);
        if (!currentDefines.Contains(define))
        {
            PlayerSettings.SetScriptingDefineSymbolsForGroup(group,
                string.IsNullOrEmpty(currentDefines) ? define : currentDefines + ";" + define);
        }
    }
}
