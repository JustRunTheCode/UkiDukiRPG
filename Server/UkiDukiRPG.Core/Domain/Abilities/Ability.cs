using UkiDukiRPG.Core.Domain.Characters;

namespace UkiDukiRPG.Core.Domain.Abilities;

public enum AbilityType
{
    None = 0,

    ClawSwipe,
    DragonScales,
    FlameBreath,
    Intimidate,

    Bite,
    Pounce,
    Skitter,
    WebThrow,

    ArcaneSurge,
    Firebolt,
    HexShield,
    ManaDrain,

    DirtyKick,
    Frenzy,
    Headbutt,
    RustyBlade,

    BattleCry,
    SecondWind,
    ShieldUp,
    Slash,

    Curse,
    DarkPact,
    DrainLife,
    ShadowBolt,

    Count, //NOTE: hack to get array size value required to hold the types, always keep as last element
}

public abstract class Ability(string name, AbilityType type)
{
    public string Name => name;

    public AbilityType Type => type;

    public abstract void Use(Combatant caster, Combatant target);
}
