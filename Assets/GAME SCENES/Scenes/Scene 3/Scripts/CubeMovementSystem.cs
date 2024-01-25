using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public partial class CubeMovementSystem : SystemBase
{
    protected override void OnUpdate()
    {
        float deltaTime = Time.DeltaTime;

        Entities.ForEach((ref Translation translation) =>
        {
            float moveSpeed = 1f;
            translation.Value.y += moveSpeed * deltaTime;
        }).ScheduleParallel();
    }

    
}
