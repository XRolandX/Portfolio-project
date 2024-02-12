using System;
using Unity.Entities;
using Unity.Mathematics;

public partial class AddComponentSystem : SystemBase
{
    private EndSimulationEntityCommandBufferSystem _ecbSystem;

    protected override void OnCreate()
    {
        _ecbSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate() 
    {
        var ecb = _ecbSystem.CreateCommandBuffer().AsParallelWriter();
        uint seed = (uint)UnityEngine.Random.Range(1, int.MaxValue);
        uint frameCount = (uint)UnityEngine.Time.frameCount;

        Entities.
            WithNone<MoveSpeed>().
            ForEach((int entityInQueryIndex, Entity entity) =>
            {
                uint uniqueSeed = seed + frameCount + (uint)entityInQueryIndex;
                var random = new Unity.Mathematics.Random(uniqueSeed);

                float speedValue = random.NextFloat(1f, 10f);
                ecb.AddComponent(entityInQueryIndex, entity, new MoveSpeed(speedValue));
            })
            .ScheduleParallel();

        _ecbSystem.AddJobHandleForProducer(Dependency);
    }
}


