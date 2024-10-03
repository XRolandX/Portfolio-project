using UnityEngine;

public class ManualLightUpdate : MonoBehaviour
{
    private void Awake()
    {
        LightProbes.Tetrahedralize();
        DynamicGI.UpdateEnvironment();
    }
}
