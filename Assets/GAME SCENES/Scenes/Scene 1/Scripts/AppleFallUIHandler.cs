using UnityEngine;
using UnityEngine.SceneManagement;
public class AppleFallUIHandler : MonoBehaviour
{
    [SerializeField] GameObject buttonsOverlay;
    [SerializeField] GameObject joystickOverlay;

    private void Awake()
    {
#if PLATFORM_STANDALONE_WIN
        buttonsOverlay.SetActive(false);
        joystickOverlay.SetActive(false);
#endif
#if UNITY_ANDROID
        buttonsOverlay.SetActive(true);
        joystickOverlay.SetActive(true);
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


