using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene2UIHandler : MonoBehaviour
{
    [SerializeField] GameObject androidOverlay;
    private PlayerControls playerControls;
    private void Awake()
    {
        
#if PLATFORM_STANDALONE_WIN
        androidOverlay.SetActive(false);
#endif
#if UNITY_ANDROID
        androidOverlay.SetActive(true);
#endif

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        playerControls = new PlayerControls();
        playerControls.Player.Enable();
        playerControls.Player.RestartScene.performed += ctx => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        playerControls.Player.ToMainMenu.performed += ctx => SceneManager.LoadScene(0);
#if UNITY_EDITOR
        playerControls.Player.StopPlayMode.performed += ctx => UnityEditor.EditorApplication.isPlaying = false;
#endif
        playerControls.Player.CursorUnlock.performed += ctx => Cursor.lockState = CursorLockMode.None;
    }
    private void OnDisable()
    {
        playerControls.Player.Disable();
    }
}
