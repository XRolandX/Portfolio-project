using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public partial class CubeMovementSystem : SystemBase
{
    protected override void OnUpdate()
    {
        float deltaTime = Time.DeltaTime;

        Entities.ForEach((ref Translation translation, in MoveSpeed moveSpeed) =>
        {
            var value = moveSpeed.Value * deltaTime;
            // Update the position of the entity based on its speed
            translation.Value += new float3(value, value, value);
        }).ScheduleParallel();
    }

    
}
