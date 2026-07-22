using UkiDukiRPG.Core.Domain.Battle;
using UkiDukiRPG.Core.Domain.Time;

using Combatant = UkiDukiRPG.Core.Domain.Battle.Combatant;

namespace UkiDukiRPG.Core.Domain.Effects;

public abstract class Effect(string name)
{
    public string Name { get; } = name;

    public abstract void Apply(Combatant caster, Combatant target, IBattleEngine battle);
}

public abstract class InstantEffect(string name) : Effect(name) { }

public enum StatusEffectCategory
{
    Buff,
    Debuff,
}

public enum StatusEffectType
{
    None = 0,

    AttackDecrease,
    AttackIncrease,
    DefenseDecrease,
    DefenseIncrease,
    MagicDecrease,
    MagicIncrease,

    Count, //NOTE: hack to get array size value required to hold the types, always keep as last element
}

public abstract partial class StatusEffect(string name, StatusEffectType type, TimeInterval duration) : Effect(name)
{
    public TimeInterval Duration { get; } = duration;

    public StatusEffectType Type { get; } = type;

    public abstract void Clear(Combatant combatant);

    public abstract StatusEffectCategory Category();
}

public abstract class BuffEffect(string name, StatusEffectType type, TimeInterval duration) : StatusEffect(name, type, duration)
{
    public override StatusEffectCategory Category() => StatusEffectCategory.Buff;
}

public abstract class DebuffEffect(string name, StatusEffectType type, TimeInterval duration) : StatusEffect(name, type, duration)
{
    public override StatusEffectCategory Category() => StatusEffectCategory.Debuff;
}
