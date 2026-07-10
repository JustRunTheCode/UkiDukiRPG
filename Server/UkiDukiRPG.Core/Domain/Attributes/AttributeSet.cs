using System.Text;

namespace UkiDukiRPG.Core.Domain.Attributes;

public class AttributeSet
{
    public HealthAttribute  Health  { get; }
    public AttackAttribute  Attack  { get; }
    public DefenseAttribute Defense { get; }
    public MagicAttribute   Magic   { get; }

    private readonly Action[]      m_AttributeLevelUpActions;
    private readonly Action<int>[] m_AttributeAscendActions;
    private readonly Action<int>[] m_AttributeDescendActions;

    public AttributeSet(int health = 0, int attack = 0, int defense = 0, int magic = 0) : this(new HealthAttribute(health), new AttackAttribute(attack), new DefenseAttribute(defense),
                                                                                               new MagicAttribute(magic)) { }

    public AttributeSet(HealthAttribute health, AttackAttribute attack, DefenseAttribute defense, MagicAttribute magic)
    {
        Health  = health;
        Attack  = attack;
        Defense = defense;
        Magic   = magic;

        m_AttributeLevelUpActions = new Action[(int)AttributeType.Count];
        m_AttributeAscendActions  = new Action<int>[(int)AttributeType.Count];
        m_AttributeDescendActions = new Action<int>[(int)AttributeType.Count];

        m_AttributeLevelUpActions[(int)Health.Type]  = Health.Ascend;
        m_AttributeLevelUpActions[(int)Attack.Type]  = Attack.Ascend;
        m_AttributeLevelUpActions[(int)Defense.Type] = Defense.Ascend;
        m_AttributeLevelUpActions[(int)Magic.Type]   = Magic.Ascend;

        m_AttributeAscendActions[(int)Health.Type]  = Health.Ascend;
        m_AttributeAscendActions[(int)Attack.Type]  = Attack.Ascend;
        m_AttributeAscendActions[(int)Defense.Type] = Defense.Ascend;
        m_AttributeAscendActions[(int)Magic.Type]   = Magic.Ascend;

        m_AttributeDescendActions[(int)Health.Type]  = Health.Descend;
        m_AttributeDescendActions[(int)Attack.Type]  = Attack.Descend;
        m_AttributeDescendActions[(int)Defense.Type] = Defense.Descend;
        m_AttributeDescendActions[(int)Magic.Type]   = Magic.Descend;
    }

    public void Ascend(AttributeType attribute) => m_AttributeLevelUpActions[(int)attribute]();

    public void Ascend(AttributeType attribute, int amount) => m_AttributeAscendActions[(int)attribute](amount);

    public void Descend(AttributeType attribute, int amount) => m_AttributeDescendActions[(int)attribute](amount);

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
