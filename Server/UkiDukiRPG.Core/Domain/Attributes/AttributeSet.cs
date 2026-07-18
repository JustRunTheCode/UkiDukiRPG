using System.Runtime.CompilerServices;
using System.Text;

using JustRunTheCode.Optimization.Attributes;

namespace UkiDukiRPG.Core.Domain.Attributes;

public partial class AttributeSet(HealthAttribute health, AttackAttribute attack, DefenseAttribute defense, MagicAttribute magic)
{
    public HealthAttribute  Health  { get; } = health;
    public AttackAttribute  Attack  { get; } = attack;
    public DefenseAttribute Defense { get; } = defense;
    public MagicAttribute   Magic   { get; } = magic;

    public AttributeSet(int health = 0, int attack = 0, int defense = 0, int magic = 0) : this(new HealthAttribute(health), new AttackAttribute(attack), new DefenseAttribute(defense),
                                                                                               new MagicAttribute(magic)) { }

    public void Ascend(AttributeType attribute)
    {
        AscendLookup(attribute, 1);
    }

    [LookupTable<AttributeType>(AttributeType.Count, [AttributeType.None, AttributeType.Count])]
    public void Ascend(AttributeType attribute, int amount)
    {
        AscendLookup(attribute, amount);
    }

    [LookupTable<AttributeType>(AttributeType.Count, [AttributeType.None, AttributeType.Count])]
    public void Descend(AttributeType attribute, int amount)
    {
        DescendLookup(attribute, amount);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [LookupTableReference<AttributeType>(nameof(Ascend), AttributeType.Health)]
    private static void AscendHealth(AttributeSet attributeSet, int value) => attributeSet.Health.Ascend(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [LookupTableReference<AttributeType>(nameof(Ascend), AttributeType.Attack)]
    private static void AscendAttack(AttributeSet attributeSet, int value) => attributeSet.Attack.Ascend(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [LookupTableReference<AttributeType>(nameof(Ascend), AttributeType.Defense)]
    private static void AscendDefense(AttributeSet attributeSet, int value) => attributeSet.Defense.Ascend(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [LookupTableReference<AttributeType>(nameof(Ascend), AttributeType.Magic)]
    private static void AscendMagic(AttributeSet attributeSet, int value) => attributeSet.Magic.Ascend(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [LookupTableReference<AttributeType>(nameof(Descend), AttributeType.Health)]
    private static void DescendHealth(AttributeSet attributeSet, int value) => attributeSet.Health.Descend(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [LookupTableReference<AttributeType>(nameof(Descend), AttributeType.Attack)]
    private static void DescendAttack(AttributeSet attributeSet, int value) => attributeSet.Attack.Descend(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [LookupTableReference<AttributeType>(nameof(Descend), AttributeType.Defense)]
    private static void DescendDefense(AttributeSet attributeSet, int value) => attributeSet.Defense.Descend(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [LookupTableReference<AttributeType>(nameof(Descend), AttributeType.Magic)]
    private static void DescendMagic(AttributeSet attributeSet, int value) => attributeSet.Magic.Descend(value);
    
    // @formatter:off
    public static AttributeSet operator +(AttributeSet left, AttributeSet right) => new(left.Health  + right.Health,
                                                                                        left.Attack  + right.Attack,
                                                                                        left.Defense + right.Defense,
                                                                                        left.Magic   + right.Magic);
    // @formatter:on

    public override string ToString()
    {
        var builder = new StringBuilder();

        builder.AppendLine($"Health: {Health.Level}")
               .AppendLine($"Attack: {Attack.Level}")
               .AppendLine($"Defense: {Defense.Level}")
               .AppendLine($"Magic: {Magic.Level}");

        return builder.ToString();
    }
}
