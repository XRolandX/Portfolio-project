using Unity.Entities;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 50f;
    [SerializeField] private float sensitivity = 50f;
    [SerializeField] private float lookPitch = 0f;
    
    private Vector2 moveInput;
    private Vector2 lookInput;

    private EntityManager entityManager;
    private Entity mouseInputEntity;
    private Entity spawnTransformEntity;
    private Entity spawnRotationEntity;
    
    public PlayerControls playerControls;
    public Transform spawnPoint;


    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        playerControls = new PlayerControls();

        EnsureMouseInputEntity();
        EnsureSpawnPositionEntity();
        EnsureSpawnRotationEntity();
        InputSystemSetting();
    }
    
    private void Update()
    {
#if PLATFORM_STANDALONE_WIN
        PlayerMovesAndLooks();
#endif
        SpawnTransformUpdate();
    }

    private void EnsureMouseInputEntity()
    {
        var mouseInputQuery = entityManager.CreateEntityQuery(typeof(MouseInput));
        if (mouseInputQuery.IsEmptyIgnoreFilter)
        {
            mouseInputEntity = entityManager.CreateEntity(typeof(MouseInput));
        }
        else
        {
            mouseInputEntity = mouseInputQuery.GetSingletonEntity();
        }
    }
    private void EnsureSpawnPositionEntity()
    {
        var spawnPositonQuery = entityManager.CreateEntityQuery(typeof(SpawnPosition));
        if (spawnPositonQuery.IsEmptyIgnoreFilter)
        {
            spawnTransformEntity = entityManager.CreateEntity(typeof(SpawnPosition));
        }
        else
        {
            spawnTransformEntity = spawnPositonQuery.GetSingletonEntity();
        }
    }
    private void EnsureSpawnRotationEntity()
    {
        var spawnRotationQuery = entityManager.CreateEntityQuery(typeof(SpawnRotation));
        if(spawnRotationQuery.IsEmptyIgnoreFilter)
        {
            spawnRotationEntity = entityManager.CreateEntity(typeof (SpawnRotation));
        }
        else
        {
            spawnRotationEntity = spawnRotationQuery.GetSingletonEntity();
        }
    }
    private void InputSystemSetting()
    {
        playerControls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        playerControls.Player.Move.canceled += ctx => moveInput = Vector2.zero;
        playerControls.Player.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        playerControls.Player.Look.canceled += ctx => lookInput = Vector2.zero;
        playerControls.Player.MouseClick.performed += ctx => UpdateMouseInput(true);
        playerControls.Player.MouseClick.canceled += ctx => UpdateMouseInput(false);
    }
    private void UpdateMouseInput(bool leftClickPerformed)
    {
        entityManager
            .SetComponentData(mouseInputEntity, new MouseInput { LeftClickPerformed = leftClickPerformed });
    }

    private void PlayerMovesAndLooks()
    {
        Vector3 move = moveSpeed * Time.deltaTime * new Vector3(moveInput.x, 0f, moveInput.y);
        transform.Translate(move, Space.Self);

        Vector2 look = sensitivity * Time.deltaTime * lookInput;
        transform.Rotate(0, look.x, 0, Space.World);
        lookPitch -= look.y;
        lookPitch = Mathf.Clamp(lookPitch, -85f, 85f);
        transform.localEulerAngles = new Vector3(lookPitch, transform.localEulerAngles.y, 0f);
    }
    private void SpawnTransformUpdate()
    {
        SpawnPositionUpdate();
        SpawnRotationUpdate();
    }
    private void SpawnPositionUpdate()
    {
        if (spawnPoint != null)
        {
            Transform spawnPointTransform = spawnPoint;
            entityManager
                .SetComponentData(spawnTransformEntity, new SpawnPosition { Position = spawnPointTransform.position });
        }
        else
        {
            Debug.LogError("Spawn Point object doesn't assigned to PlayerController script");
        }
    }
    private void SpawnRotationUpdate()
    {
        if(spawnPoint != null)
        {
            Transform spawnPointTransform = spawnPoint;
            entityManager
                .SetComponentData(spawnRotationEntity, new SpawnRotation { Rotation =  spawnPointTransform.rotation });
        }
        else
        {
            Debug.LogError("Spawn Point object doesn't assigned to PlayerController script");
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
