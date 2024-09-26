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
    private GameObject projectilePrefab;
    private float spawnTimer = 0f;
    private readonly float entityForce = 100f;

    protected override void OnCreate()
    {
        Initialize();
    }
    private void Initialize()
    {
        projectilePrefab = Resources.Load<GameObject>("projectilePrafab");

        _ecbSystem = World
            .GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();

        _blobAssetStore = new BlobAssetStore();
    }

    protected override void OnUpdate()
    {
        CreateEntitiesByClick();
    }
    public void CreateEntitiesByClick()
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

            if (projectilePrefab != null)
            {
                var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, _blobAssetStore);
                _prefabEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(projectilePrefab, settings);

                float3 forwardDirection = math.mul(spawnRotation, new float3(0, 0, 1));

                Entity instance = ecb.Instantiate(_prefabEntity);
                ecb.SetComponent(instance, new Translation { Value = spawnPosition });
                ecb.SetComponent(instance, new Rotation { Value = spawnRotation });
                ecb.AddComponent(instance, new PhysicsVelocity
                {
                    Linear = forwardDirection * entityForce,
                    Angular = float3.zero
                });

                _ecbSystem.AddJobHandleForProducer(Dependency);
            }
            else
            {
                Debug.LogError("Cube prefab is null");
            }
        }
    }

    protected override void OnDestroy()
    {
        _blobAssetStore.Dispose();
        projectilePrefab = null;
    }
}