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
        playerControls = new PlayerControls();
        playerControls.Player.RestartScene.performed += ctx => SceneReloading();
        playerControls.Player.ToMainMenu.performed += ctx => MainSceneLoading();
        playerControls.Player.StopPlayMode.performed += ctx => StopPlayMode();
        playerControls.Player.CursorUnlock.performed += ctx => CursorUnlocking();
    }


    public void SceneReloading()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MainSceneLoading()
    {
        SceneManager.LoadScene(0);
    }
#if UNITY_EDITOR
    void StopPlayMode()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
    void CursorUnlocking()
    {
        Cursor.lockState = CursorLockMode.None;
    }
#endif
    private void OnEnable()
    {
        playerControls.Player.Enable();
    }
    private void OnDisable()
    {
        playerControls.Player.Disable();
    }

}
