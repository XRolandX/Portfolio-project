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
            translation.Value += new float3(0, moveSpeed.Value * deltaTime, 0);
        }).ScheduleParallel();
    }
}
