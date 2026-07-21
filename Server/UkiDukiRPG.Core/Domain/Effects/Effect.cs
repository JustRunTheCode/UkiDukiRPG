using UkiDukiRPG.Core.Domain.Characters;
using UkiDukiRPG.Core.Domain.Time;

namespace UkiDukiRPG.Core.Domain.Effects;

public abstract class Effect(string name, IScheduler scheduler)
{
    public string Name { get; } = name;

    public int TimeActivated { get; } = scheduler.CurrentTick;

    protected readonly IScheduler m_Scheduler = scheduler;

    public abstract void Apply(Combatant caster, Combatant target);
}

public abstract class InstantEffect(string name, IScheduler scheduler) : Effect(name, scheduler) { }

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

public abstract partial class StatusEffect(string name, StatusEffectType type, TimeInterval duration, IScheduler scheduler) : Effect(name, scheduler)
{
    public TimeInterval Duration { get; } = duration;

    public StatusEffectType Type { get; } = type;
    
    protected void ScheduleClear(Combatant combatant) => m_Scheduler.Schedule(() => Clear(combatant), Duration);

    public abstract void Clear(Combatant combatant);

    public abstract StatusEffectCategory Category();
}

public abstract class BuffEffect(string name, StatusEffectType type, TimeInterval duration, IScheduler scheduler) : StatusEffect(name, type, duration, scheduler)
{
    public override StatusEffectCategory Category() => StatusEffectCategory.Buff;
}

public abstract class DebuffEffect(string name, StatusEffectType type, TimeInterval duration, IScheduler scheduler) : StatusEffect(name, type, duration, scheduler)
{
    public override StatusEffectCategory Category() => StatusEffectCategory.Debuff;
}
