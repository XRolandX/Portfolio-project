using Unity.Entities;
using Unity.Transforms;
using Unity.Physics;
using UnityEngine;
using Unity.Mathematics;

public partial class SpawnEntities : SystemBase
{
    private EndSimulationEntityCommandBufferSystem _ecbSystem;
    private BlobAssetStore _blobAssetStore;
    private Entity _prefabEntity;
    private GameObject cubePrefab;
    public PlayerControls controls;
    private float spawnTimer;
    private readonly float entityForce = 100f;

    protected override void OnCreate()
    {
        spawnTimer = 0f;

        controls = new PlayerControls();
        controls.Enable();

        cubePrefab = Resources.Load<GameObject>("New Prefab");

        _ecbSystem = World
            .GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        _blobAssetStore = new BlobAssetStore();


        var settings = GameObjectConversionSettings
            .FromWorld(World.DefaultGameObjectInjectionWorld, _blobAssetStore);

        _prefabEntity = GameObjectConversionUtility
            .ConvertGameObjectHierarchy(cubePrefab, settings);

    }
    protected override void OnUpdate()
    {
        var deltaTime = Time.DeltaTime;
        spawnTimer += deltaTime;

        var mouseInput = GetSingleton<MouseInput>();
        var spawnPosition = GetSingleton<SpawnPosition>().Position;
        var spawnRotation = GetSingleton<SpawnRotation>().Rotation;

        if (mouseInput.LeftClickPerformed && spawnTimer >= 0.1f)
        {
            spawnTimer = 0f;
            var ecb = _ecbSystem.CreateCommandBuffer();

            float3 forwardDirection = math.mul(spawnRotation, new float3(0, 0, 1));

            Debug.Log("Entity is instantiated");
            Entity instance = ecb.Instantiate(_prefabEntity);
            ecb.SetComponent(instance, new Translation { Value = spawnPosition });
            ecb.AddComponent(instance, new PhysicsVelocity
            {
                Linear = forwardDirection * entityForce,
                Angular = float3.zero
            });

            _ecbSystem.AddJobHandleForProducer(Dependency);
            
        }
    }
    protected override void OnDestroy()
    {
        _blobAssetStore.Dispose();
    }
}