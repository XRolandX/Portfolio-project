using UnityEngine;
using Unity.Entities;

// MonoBehaviour, який додаємо на prefab‑асет
public class ProjectileAuthoring : MonoBehaviour
{
    // тут можна додати налаштування (наприклад швидкість, mesh)
}

// Baker, який конвертує цей MonoBehaviour у ECS-entity з тегом
public class ProjectileBaker : Baker<ProjectileAuthoring>
{
    public override void Bake(ProjectileAuthoring authoring)
    {
        // отримуємо Entity‑інстанс із динамічним трансформом
        Entity e = GetEntity(TransformUsageFlags.Dynamic);
        // додаємо маркер‑компонент
        AddComponent(e, new ProjectileTag());
    }
}
