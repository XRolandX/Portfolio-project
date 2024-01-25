using Unity.Entities;
using UnityEngine;

public class MoveSpeedAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        var data = new MoveSpeed { Value = Random.Range(-5f, +5f) };
        dstManager.AddComponentData(entity, data);
    }
}
