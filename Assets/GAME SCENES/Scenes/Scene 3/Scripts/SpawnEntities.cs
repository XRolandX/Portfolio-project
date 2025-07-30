using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Physics;
using Unity.Mathematics;
using Unity.Collections;

public partial class SpawnEntities : SystemBase
{
    private EndSimulationEntityCommandBufferSystem _ecbSystem;
    private Entity _prefabEntity = Entity.Null;
    private bool _lastClick = false;
    private const float EntityForce = 100f;

    protected override void OnCreate()
    {
        // Система чекатиме, поки в світі з’явиться саме один singleton-компонент ProjectilePrefab
        RequireForUpdate<ProjectilePrefab>();
        _ecbSystem = World.GetOrCreateSystemManaged<EndSimulationEntityCommandBufferSystem>();
    }

    protected override void OnStartRunning()
    {
        // Тут гарантовано існує єдиний singleton—компонент ProjectilePrefab
        var prefabData = SystemAPI.GetSingleton<ProjectilePrefab>();
        _prefabEntity = prefabData.Value;
        Debug.Log($"✅ ProjectilePrefab acquired: {_prefabEntity}");
    }

    protected override void OnUpdate()
    {
        // 1) Чи взагалі система оновлюється?
        Debug.Log("[SpawnEntities] OnUpdate");

        // 2) Скільки у світі префабів з нашим тегом?
        int count = EntityManager
            .CreateEntityQuery(ComponentType.ReadOnly<Prefab>(),
                            ComponentType.ReadOnly<ProjectileTag>())
            .CalculateEntityCount();
        Debug.Log($"[SpawnEntities] PrefabTag count = {count}");

        // 3) Якщо count==0 — далі нема сенсу перевіряти кліки
        if (count == 0)
            return;

        // 4) Якщо вперше знайшли, збережімо в _prefabEntity
        if (_prefabEntity == Entity.Null)
        {
            using var arr = EntityManager
                .CreateEntityQuery(ComponentType.ReadOnly<Prefab>(), ComponentType.ReadOnly<ProjectileTag>())
                .ToEntityArray(Allocator.Temp);
            _prefabEntity = arr[0];
            Debug.Log("[SpawnEntities] Cached _prefabEntity = " + _prefabEntity);
        }

        // 5) Тепер перевіримо клік і спавнимо
        bool click = SystemAPI.GetSingleton<MouseInput>().LeftClickPerformed;
        Debug.Log($"[SpawnEntities] click={click}");
        if (click)
        {
            Debug.Log("[SpawnEntities] SPAWNING!");
            // … тут твій ecb.Instantiate() …
        }
    }

}
