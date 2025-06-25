using UnityEngine;
using UnityEngine.SceneManagement;
public class AppleFallUIHandler : MonoBehaviour
{
    [SerializeField] GameObject androidOverlay;
    private PlayerControls playerControls;

    private void Awake()
    {
        playerControls = new PlayerControls();

#if UNITY_EDITOR
        playerControls.Player.CursorUnlock.performed += ctx => Cursor.lockState = CursorLockMode.None;
        playerControls.Player.StopPlayMode.performed += ctx => UnityEditor.EditorApplication.isPlaying = false;
#endif

#if UNITY_STANDALONE_WIN
        playerControls.Player.RestartScene.performed += ctx => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        playerControls.Player.ToMainMenu.performed += ctx => SceneManager.LoadScene(0);
        
        androidOverlay.SetActive(false);
#endif

#if UNITY_ANDROID
        androidOverlay.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
#endif
    }

    #region A N D R O I D   B U T T O N S   M E T H O D S
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
#if UNITY_ANDROID
        if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft)
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        }
        else if(Input.deviceOrientation == DeviceOrientation.LandscapeRight)
        {
            Screen.orientation = ScreenOrientation.LandscapeRight;
        }
        else Screen.orientation = ScreenOrientation.LandscapeRight;
#endif
    }
    #endregion

    private void OnEnable()
    {
        playerControls.Player.Enable();
    }
    private void OnDisable()
    {
        playerControls.Player.Disable();
    }
}


