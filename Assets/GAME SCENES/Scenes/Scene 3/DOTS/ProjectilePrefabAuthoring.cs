using UnityEngine;
using Unity.Entities;

// MonoBehaviour для вказівки Asset‑префабу
public class ProjectilePrefabAuthoring : MonoBehaviour
{
    [Tooltip("Asset‑префаб, який спавнить система")]
    public GameObject prefab;
}

// Singleton‑компонент, що зберігає посилання на Entity‑префаб
public struct ProjectilePrefab : IComponentData
{
    public Entity Value;
}

// Baker, що конвертує цей авторинг у ECS‑світ і додає компонент ProjectilePrefab
public class ProjectilePrefabBaker : Baker<ProjectilePrefabAuthoring>
{
    public override void Bake(ProjectilePrefabAuthoring authoring)
    {
        // Конвертуємо сам Asset‑префаб у Entity
        Entity prefabEntity = GetEntity(authoring.prefab, TransformUsageFlags.Dynamic);

        // Отримуємо Entity для самого Holder’а (GameObject із MonoBehaviour)
        Entity holderEntity = GetEntity(TransformUsageFlags.None);

        // Додаємо singleton‑компонент з посиланням на prefabEntity
        AddComponent(holderEntity, new ProjectilePrefab { Value = prefabEntity });
    }
}
