//using Unity.Entities;
//using Unity.Rendering;
//using Unity.Transforms;
//using Unity.Mathematics;
//using UnityEngine;

//public partial class CubesInstaniation : SystemBase
//{
//    protected override void OnCreate()
//    {
//        Mesh cubeMesh = Resources
//            .Load<GameObject>("New Prefab").GetComponent<MeshFilter>().sharedMesh;
//        Material cubeMaterial = Resources
//            .Load<Material>("New Material");

//        EntityManager entityManager = World
//            .DefaultGameObjectInjectionWorld.EntityManager;

//        EntityArchetype cubeArchetype = entityManager.CreateArchetype(
//            typeof(RenderMesh),
//            typeof(LocalToWorld),
//            typeof(Translation),
//            typeof(Rotation),
//            typeof(Scale)
//        );

//        Entity cubeEntity = entityManager.CreateEntity(cubeArchetype);


//        entityManager.SetSharedComponentData(cubeEntity, new RenderMesh
//        {
//            mesh = cubeMesh,
//            material = cubeMaterial,
//            layer = LayerMask.NameToLayer("Default")
//        });

//        entityManager
//            .SetComponentData(cubeEntity, new Translation { Value = new(0f, 0f, 0f) });
//        entityManager
//            .SetComponentData(cubeEntity, new Rotation { Value = quaternion.identity });
//        entityManager
//            .SetComponentData(cubeEntity, new Scale { Value = 10f });

        
//    }

//    protected override void OnUpdate()
//    {
//        //
//    }
//}
