namespace UkiDukiRPG.Core.Domain.Attributes;

public class AttackAttribute(int level = 0) : Attribute(AttributeType.Attack, level)
{
    public static AttackAttribute operator+(AttackAttribute left, AttackAttribute right) => new(left.Level + right.Level);
    
    public float EffectMultiplier() => 1 + 0.067f * Level;
}
