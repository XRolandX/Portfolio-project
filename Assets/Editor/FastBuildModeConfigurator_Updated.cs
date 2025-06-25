using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using System.IO;

public class FastBuildModeConfigurator_Updated : EditorWindow
{
    [MenuItem("Tools/Build/Fast Build Optimizer (Updated)")]
    public static void RunFastBuildOptimization()
    {
        string logPath = @"D:\UnityBuildSpeedup_Log.txt";
        using StreamWriter log = new StreamWriter(logPath, false);

        log.WriteLine("=== FAST BUILD OPTIMIZER (Updated) ===");
        log.WriteLine("Date: " + System.DateTime.Now);
        log.WriteLine("---------------------------------");

        // 1. Player Settings
#if UNITY_2021_2_OR_NEWER
        PlayerSettings.stripEngineCode = true;
        PlayerSettings.stripUnusedMeshComponents = true;
        log.WriteLine("✅ PlayerSettings: stripEngineCode + stripUnusedMeshComponents ENABLED");
#else
        log.WriteLine("⚠️ stripEngineCode not supported in this Unity version.");
#endif
        PlayerSettings.gcIncremental = true;

        // 2. Fast Shader Compilation
#if UNITY_2021_2_OR_NEWER
        EditorPrefs.SetBool("ShaderCompilation.FastPlatformShaderCompiler", true);
        log.WriteLine("✅ Fast Shader Compilation ENABLED (EditorPrefs)");
#endif

        // 3. URP Optimization via SerializedObject
        var rpAsset = GraphicsSettings.currentRenderPipeline;
        if (rpAsset == null)
        {
            log.WriteLine("❌ Render Pipeline Asset is NULL. URP not assigned.");
        }
        else
        {
            log.WriteLine($"🔍 RP Asset detected: {rpAsset.GetType().Name}");

            try
            {
                SerializedObject urpObj = new SerializedObject(rpAsset);
                SerializedProperty iterator = urpObj.GetIterator();

                log.WriteLine("🔎 Scanning URP asset properties...");
                bool foundAny = false;

                while (iterator.NextVisible(true))
                {
                    if (iterator.name == "m_EnableSRPBatcher")
                    {
                        iterator.boolValue = true;
                        foundAny = true;
                        log.WriteLine("✅ m_EnableSRPBatcher set to TRUE");
                    }

                    if (iterator.name == "m_StripUnusedVariants")
                    {
                        iterator.boolValue = true;
                        foundAny = true;
                        log.WriteLine("✅ m_StripUnusedVariants set to TRUE");
                    }
                }

                if (foundAny)
                {
                    urpObj.ApplyModifiedProperties();
                    AssetDatabase.SaveAssets();
                    log.WriteLine("✅ URP asset updated.");
                }
                else
                {
                    log.WriteLine("⚠️ No known URP fields found. Possibly a custom pipeline.");
                }
            }
            catch (System.Exception e)
            {
                log.WriteLine("❌ Exception while updating URP asset: " + e.Message);
            }
        }

        // 4. Optimize all imported Models
        string[] modelGUIDs = AssetDatabase.FindAssets("t:Model");
        int optimizedCount = 0;

        foreach (string guid in modelGUIDs)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            if (AssetImporter.GetAtPath(path) is ModelImporter importer)
            {
                importer.meshCompression = ModelImporterMeshCompression.Off;
                importer.isReadable = false;

#if UNITY_2021_2_OR_NEWER
                importer.optimizeMeshPolygons = true;
                importer.optimizeMeshVertices = false;
#else
                importer.optimizeMesh = false;
#endif

                importer.importBlendShapes = false;
                importer.importVisibility = false;
                importer.importCameras = false;
                importer.importLights = false;
                importer.SaveAndReimport();

                optimizedCount++;
                log.WriteLine($"🧹 Optimized: {path}");
            }
        }

        log.WriteLine($"✅ Total models optimized: {optimizedCount}");

        AssetDatabase.Refresh();
        log.WriteLine("\n--- DONE ---");
        Debug.Log($"✔️ Fast Build optimization complete. Log saved to {logPath}");
    }
}
