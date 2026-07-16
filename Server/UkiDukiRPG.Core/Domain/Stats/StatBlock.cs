using UkiDukiRPG.Core.Domain.Attributes;

namespace UkiDukiRPG.Core.Domain.Stats;

public class StatBlock(HealthStat health, DamageStat damage, DefenseStat defense, ManaStat mana)
{
    public HealthStat  Health  { get; } = health;
    public DamageStat  Damage  { get; } = damage;
    public DefenseStat Defense { get; } = defense;
    public ManaStat    Mana    { get; } = mana;

    public StatBlock(float health = 0, float damage = 0, float defense = 0, float mana = 0) : this(new HealthStat(health), new DamageStat(damage), new DefenseStat(defense), new ManaStat(mana)) { }

    public static StatBlock operator +(StatBlock left, StatBlock right) => new(left.Health + right.Health, left.Damage + right.Damage, left.Defense + right.Defense, left.Mana + right.Mana);

    public static StatBlock operator +(AttributeSet right, StatBlock left) => left + right;
    
    // @formatter:off
    public static StatBlock operator +(StatBlock left, AttributeSet right) => new(left.Health.Value  + right.Health.MaxHealth(),
                                                                                  left.Damage.Value  + right.Attack.EffectMultiplier(),
                                                                                  left.Defense.Value + right.Defense.EffectMultiplier(),
                                                                                  left.Mana.Value    + right.Magic.EffectMultiplier());
    // @formatter:on
}

public class CombatStats(HealthStat health, DamageStat damage, DefenseStat defense, ManaStat mana)
{
    public HealthStat  MaxHealth { get; } = health;
    public ManaStat    MaxMana   { get; } = mana;
    public HealthStat  Health    { get; } = health;
    public DamageStat  Damage    { get; } = damage;
    public DefenseStat Defense   { get; } = defense;
    public ManaStat    Mana      { get; } = mana;

    public static implicit operator CombatStats(StatBlock stats) => new(stats.Health, stats.Damage, stats.Defense, stats.Mana);
}
