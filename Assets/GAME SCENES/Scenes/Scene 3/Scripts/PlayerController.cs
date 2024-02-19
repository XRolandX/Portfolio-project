using Unity.Entities;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 50f;
    public float sensitivity = 50f;
    private float cameraPitch = 0f;
    
    private Vector2 moveInput;
    private Vector2 lookInput;

    private EntityManager entityManager;
    private Entity mouseInputEntity;
    private Entity spawnTransformEntity;
    private Entity spawnRotationEntity;
    
    public PlayerControls controls;
    private Camera playerCamera;
    public Transform spawnPoint;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        controls = new PlayerControls();
        playerCamera = Camera.main;

        EnsureMouseInputEntity();
        EnsureSpawnPositionEntity();
        EnsureSpawnRotationEntity();

        InputSystemInput();
    }
    private void OnEnable()
    {
        controls.Player.Enable();
    }
    private void OnDisable()
    {
        controls.Player.Disable();
    }
    private void Update()
    {
        PlayerMovesAndLooks();
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
        cameraPitch -= look.y;
        cameraPitch = Mathf.Clamp(cameraPitch, -85f, 85f);
        playerCamera.transform.localEulerAngles = new Vector3(cameraPitch, playerCamera.transform.localEulerAngles.y, 0f);
    }
    private void InputSystemInput()
    {
        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;
        controls.Player.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        controls.Player.Look.canceled += ctx => lookInput = Vector2.zero;
        controls.Player.MouseClick.performed += ctx => UpdateMouseInput(true);
        controls.Player.MouseClick.canceled += ctx => UpdateMouseInput(false);
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
    private void SpawnTransformUpdate()
    {
        SpawnPositionUpdate();
        SpawnRotationUpdate();
    }
}
