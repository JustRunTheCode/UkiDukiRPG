using UkiDukiRPG.Core.Domain.Characters;
using UkiDukiRPG.Core.Domain.Time;

namespace UkiDukiRPG.Core.Domain.Effects;

public abstract class Effect(string name, ITimeSystem timeSystem)
{
    public string Name { get; } = name;

    public int TimeActivated { get; } = timeSystem.CurrentTick;

    protected readonly ITimeSystem timeSystem = timeSystem;

    public abstract void Apply(Combatant caster, Combatant target);
}

public abstract class InstantEffect(string name, ITimeSystem timeSystem) : Effect(name, timeSystem) { }

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

public abstract partial class StatusEffect(string name, StatusEffectType type, TimeInterval duration, ITimeSystem timeSystem) : Effect(name, timeSystem)
{
    public TimeInterval Duration { get; } = duration;

    public StatusEffectType Type { get; } = type;
    
    protected void ScheduleClear(Combatant combatant) => timeSystem.Schedule(() => Clear(combatant), Duration);

    public abstract void Clear(Combatant combatant);

    public abstract StatusEffectCategory Category();
}

public abstract class BuffEffect(string name, StatusEffectType type, TimeInterval duration, ITimeSystem timeSystem) : StatusEffect(name, type, duration, timeSystem)
{
    public override StatusEffectCategory Category() => StatusEffectCategory.Buff;
}

public abstract class DebuffEffect(string name, StatusEffectType type, TimeInterval duration, ITimeSystem timeSystem) : StatusEffect(name, type, duration, timeSystem)
{
    public override StatusEffectCategory Category() => StatusEffectCategory.Debuff;
}
