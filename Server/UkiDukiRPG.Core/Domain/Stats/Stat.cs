namespace UkiDukiRPG.Core.Domain.Stats;

public enum StatType
{
    None = 0,

    Health,
    Damage,
    Defense,
    Mana,

    Count, //NOTE: hack to get array size value required to hold the types, always keep as last element
}

public enum CombatStatType
{
    None = 0,

    MaxHealth,
    MaxMana,
    Health,
    Damage,
    Defense,
    Mana,

    Count, //NOTE: hack to get array size value required to hold the types, always keep as last element
}

public abstract class Stat(StatType type, float value = 0)
{
    public float    Value { get; set; } = value;
    public StatType Type  { get; }      = type;
}
