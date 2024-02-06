using UnityEngine;
using UnityEngine.SceneManagement;
public class AppleFallUIHandler : MonoBehaviour
{
    [SerializeField] GameObject canvasOverlay;

    private void Awake()
    {
#if PLATFORM_STANDALONE_WIN
        canvasOverlay.SetActive(false);
#endif
#if UNITY_ANDROID
        canvasOverlay.SetActive(true);
#endif
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(1);
    }
    public void MenuScene()
    {
        SceneManager.LoadScene(0);
    }

    private void LateUpdate()
    {
        if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft)
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        }
        else if(Input.deviceOrientation == DeviceOrientation.LandscapeRight)
        {
            Screen.orientation = ScreenOrientation.LandscapeRight;
        }
        else Screen.orientation = ScreenOrientation.LandscapeRight;
    }
}


