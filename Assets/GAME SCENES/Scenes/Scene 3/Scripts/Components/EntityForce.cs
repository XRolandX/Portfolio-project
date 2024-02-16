using Unity.Entities;

public readonly struct EntityForce : IComponentData
{
    public readonly float Value;
    public EntityForce(float value)
    {
        Value = value;
    }
}
