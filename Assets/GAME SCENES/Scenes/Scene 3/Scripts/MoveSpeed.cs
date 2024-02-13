using Unity.Entities;

public readonly struct MoveSpeed : IComponentData
{
    public readonly float Value;

    public MoveSpeed(float value)
    {
        Value = value;
    }
}
