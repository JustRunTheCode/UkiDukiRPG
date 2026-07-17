namespace UkiDukiRPG.Core.Domain.Attributes;

public class MagicAttribute(int level = 0) : Attribute(AttributeType.Magic, level)
{
    public static MagicAttribute operator +(MagicAttribute left, MagicAttribute right) => new(left.Level + right.Level);

    public float EffectMultiplier() => 1 + 0.067f * Level;
}
