using UnityEditor;
using UnityEngine;
using System.IO;
using System;

public class CleanBuildCacheTool
{
    [MenuItem("Tools/Build/Clean Build Cache")]
    public static void CleanBuildCache()
    {
        string[] foldersToDelete = new string[]
        {
            "Library/Bee",
            "Library/BurstCache",
            "Library/Il2cppBuildCache",
            "Temp",
            "Build"
        };

        foreach (string folder in foldersToDelete)
        {
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), folder);
            if (Directory.Exists(fullPath))
            {
                try
                {
                    Directory.Delete(fullPath, true);
                    Debug.Log($"🧹 Deleted: {folder}");
                }
                catch (IOException e)
                {
                    Debug.LogWarning($"⚠️ Could not delete {folder}: {e.Message}");
                }
                catch (UnauthorizedAccessException e)
                {
                    Debug.LogWarning($"❌ Access denied to {folder}: {e.Message}");
                }
            }
        }

        AssetDatabase.Refresh();
        EditorUtility.DisplayDialog("Clean Build Cache", "🔄 Очищення завершено (усе, що вдалося видалити). Для 100% — перезапусти Unity.", "OK");
    }
}
