namespace UkiDukiRPG.Core.Domain.Attributes;

public class HealthAttribute(int level = 0) : Attribute(AttributeType.Health, level)
{
    public static HealthAttribute operator +(HealthAttribute left, HealthAttribute right) => new(left.Level + right.Level);

    public float MaxHealth() => Level * 16.75f;
}
