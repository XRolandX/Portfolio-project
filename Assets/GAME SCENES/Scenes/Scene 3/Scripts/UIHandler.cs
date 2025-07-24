using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Entities;
using Unity.Transforms; // Correct namespace
using Unity.Collections;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private GameObject androidOverlay;
    private PlayerControls playerControls;
    public EntityManager entityManager;

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
        playerControls.Player.RestartScene.performed += ctx => RestartScene();
        playerControls.Player.ToMainMenu.performed += ctx => MainSceneLoading();

        #if UNITY_EDITOR
        playerControls.Player.StopPlayMode.performed += ctx => StopPlayMode();
        playerControls.Player.CursorUnlock.performed += ctx => CursorUnlocking();
        #endif

        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
    }

    public void RestartScene()
    {
        DestroyAllEntities();
        SceneManager.LoadScene(3);
    }

    public void MainSceneLoading()
    {
        DestroyAllEntities();
        SceneManager.LoadScene(0);
    }

    #if UNITY_EDITOR
    void StopPlayMode()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }

    void CursorUnlocking()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    #endif

    void DestroyAllEntities()
    {
        // Query all entities that have the LocalTransform component
        EntityQuery query = entityManager.CreateEntityQuery(ComponentType.ReadOnly<LocalTransform>());

        // Use a `using` statement for safe disposal of the NativeArray
        using (NativeArray<Entity> entities = query.ToEntityArray(Allocator.TempJob))
        {
            foreach (var entity in entities)
            {
                entityManager.DestroyEntity(entity);
            }
        }
    }

    private void OnEnable()
    {
        playerControls.Player.Enable();
    }

    private void OnDisable()
    {
        playerControls.Player.Disable();
    }
}