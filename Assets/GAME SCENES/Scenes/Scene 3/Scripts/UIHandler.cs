using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Entities;

public class UIHandler : MonoBehaviour
{
    //[SerializeField] GameObject androidOverlay;
    private PlayerControls playerControls;
    private EntityManager entityManager;
    private void Awake()
    {

#if PLATFORM_STANDALONE_WIN
        //androidOverlay.SetActive(false);
#endif
#if UNITY_ANDROID
        //androidOverlay.SetActive(true);
#endif
        Cursor.lockState = CursorLockMode.Locked;
        playerControls = new PlayerControls();
        //playerControls.Player.RestartScene.performed += ctx => SceneReloading();
        //playerControls.Player.ToMainMenu.performed += ctx => MainSceneLoading();
        playerControls.Player.StopPlayMode.performed += ctx => StopPlayMode();
        playerControls.Player.CursorUnlock.performed += ctx => CursorUnlocking();

        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
    }


    public void SceneReloading()
    {
        entityManager.DestroyEntity(entityManager.UniversalQuery);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MainSceneLoading()
    {
        entityManager.DestroyEntity(entityManager.UniversalQuery);
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
