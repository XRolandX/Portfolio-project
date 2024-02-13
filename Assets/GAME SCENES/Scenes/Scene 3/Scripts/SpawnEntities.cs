using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;

public partial class SpawnEntities : SystemBase
{
    private EndSimulationEntityCommandBufferSystem _ecbSystem;
    private BlobAssetStore _blobAssetStore;
    private Entity _prefabEntity;
    private GameObject cubePrefab;

    protected override void OnCreate()
    {
        cubePrefab = Resources.Load<GameObject>("New Prefab");

        _ecbSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        _blobAssetStore = new BlobAssetStore();


        var settings = GameObjectConversionSettings
            .FromWorld(World.DefaultGameObjectInjectionWorld, _blobAssetStore);

        _prefabEntity = GameObjectConversionUtility
        .ConvertGameObjectHierarchy(cubePrefab, settings);
    }
    protected override void OnStartRunning()
    {
        var ecb = _ecbSystem.CreateCommandBuffer();

        for (int i = 0; i < 100000; i++)
        {
            Entity entity = ecb.Instantiate(_prefabEntity);
            float3 position = new(UnityEngine.Random.Range(-100f, 100f), UnityEngine.Random.Range(10f, 1000f), UnityEngine.Random.Range(-100f, 100f));
            ecb.SetComponent(entity, new Translation { Value = position });
        }

    }
    protected override void OnUpdate()
    {
        //
    }

    protected override void OnDestroy()
    {
        _blobAssetStore.Dispose();
    }


}

