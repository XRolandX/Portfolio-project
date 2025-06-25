using UnityEngine;
using UnityEngine.Rendering;

public class DisableURPDebugAtRuntime : MonoBehaviour
{
    void Awake()
    {
        var debug = DebugManager.instance;
        if (debug != null)
        {
            debug.enableRuntimeUI = false;
            debug.displayRuntimeUI = false;
        }
    }
}
