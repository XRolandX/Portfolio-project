// 7/24/2025 AI-Tag
// This was created with assistance from Muse, a Unity Artificial Intelligence product

using Unity.Entities;
using Unity.Transforms;
using Unity.Physics;
using UnityEngine;
using Unity.Mathematics;

public partial class SpawnEntities : SystemBase
{
    private EndSimulationEntityCommandBufferSystem _ecbSystem;
    private Entity _prefabEntity;
    private float spawnTimer = 0f;
    private readonly float entityForce = 100f;

    protected override void OnCreate()
    {
        Initialize();
    }

    private void Initialize()
    {
        // Initialize the EndSimulationEntityCommandBufferSystem
        _ecbSystem = World.GetExistingSystemManaged<EndSimulationEntityCommandBufferSystem>();

        // Load the prefab entity from the SubScene
        _prefabEntity = GetEntityPrefab("projectilePrefab");
    }

    protected override void OnUpdate()
    {
        CreateEntitiesByClick();
    }

    public void CreateEntitiesByClick()
    {
        // Use SystemAPI.Time for DeltaTime
        var deltaTime = SystemAPI.Time.DeltaTime;
        spawnTimer += deltaTime;

        var mouseInput = GetSingleton<MouseInput>();
        var spawnPosition = GetSingleton<SpawnPosition>().Position;
        var spawnRotation = GetSingleton<SpawnRotation>().Rotation;

        if (mouseInput.LeftClickPerformed && spawnTimer >= 0.1f)
        {
            spawnTimer = 0f;
            var ecb = _ecbSystem.CreateCommandBuffer();

            if (_prefabEntity != Entity.Null)
            {
                float3 forwardDirection = math.mul(spawnRotation, new float3(0, 0, 1));

                // Instantiate the entity prefab and set its components
                Entity instance = ecb.Instantiate(_prefabEntity);

                // Set the LocalTransform component (replaces Translation and Rotation)
                ecb.SetComponent(instance, LocalTransform.FromPositionRotation(spawnPosition, spawnRotation));

                ecb.AddComponent(instance, new PhysicsVelocity
                {
                    Linear = forwardDirection * entityForce,
                    Angular = float3.zero
                });

                _ecbSystem.AddJobHandleForProducer(Dependency);
            }
            else
            {
                Debug.LogError("The prefab entity is null. Ensure the prefab was successfully converted.");
            }
        }
    }

    private Entity GetEntityPrefab(string prefabName)
    {
        // Replace this with your logic to get the prefab from the SubScene
        // For example, if the prefab was baked, use EntityManager queries or prefab references
        Debug.Log("Load prefab entity from SubScene here");
        return Entity.Null; // Placeholder
    }

    protected override void OnDestroy()
    {
        // Cleanup (if necessary)
    }
}