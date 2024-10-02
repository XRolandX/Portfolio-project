using UnityEngine;

public class ConvertMaterials : MonoBehaviour
{
    // Method to convert all materials in the scene to a specified shader.
    public void ConvertAllMaterialsInScene(string shaderName)
    {
        Shader targetShader = Shader.Find(shaderName);
        if (targetShader == null)
        {
            Debug.LogError("Failed to find the shader: " + shaderName);
            return;
        }

        Renderer[] allRenderers = FindObjectsOfType<Renderer>();
        foreach (var renderer in allRenderers)
        {
            foreach (var material in renderer.sharedMaterials)
            {
                if (material != null)
                {
                    // Assign the target shader to the material
                    material.shader = targetShader;
                }
            }
        }

        Debug.Log("Converted all materials to use shader: " + shaderName);
    }

    // Convenience method to convert materials to the Standard shader
    public void ConvertToStandardShader()
    {
        ConvertAllMaterialsInScene("Standard");
    }

    // Convenience method to convert materials to the URP Lit shader
    public void ConvertToURPLitShader()
    {
        ConvertAllMaterialsInScene("Universal Render Pipeline/Lit");
    }
}