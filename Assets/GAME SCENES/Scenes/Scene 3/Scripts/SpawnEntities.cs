using Unity.Entities;
using UnityEngine;
using Unity.Transforms;

public class SpawnEntities : MonoBehaviour
{
    public GameObject prefab;
    private BlobAssetStore blobAssetStore;

    void Start()
    {
        blobAssetStore = new BlobAssetStore();

        var settings = GameObjectConversionSettings
            .FromWorld(World.DefaultGameObjectInjectionWorld, blobAssetStore);

        var prefabEntity = GameObjectConversionUtility
            .ConvertGameObjectHierarchy(prefab, settings);

        var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        for (int i = 0; i < 50000; i++)
        {
            Entity entity = entityManager.Instantiate(prefabEntity);
            Vector3 position = new(Random.Range(-100f, 100f), Random.Range(10f, 100f), Random.Range(-100f, 100f));
            entityManager.SetComponentData(entity, new Translation { Value = new(position.x, position.y, position.z) });
        }
    }

    private void OnDestroy()
    {
        blobAssetStore.Dispose();
    }
}