namespace UkiDukiRPG.Core.Domain.Attributes;

public class DefenseAttribute(int level = 0) : Attribute(AttributeType.Defense, level)
{
    public static DefenseAttribute operator+(DefenseAttribute left, DefenseAttribute right) => new(left.Level + right.Level);
    
    public float EffectMultiplier() => 0.01f *  Level;
}
