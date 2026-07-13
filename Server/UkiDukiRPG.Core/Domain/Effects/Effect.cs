using UkiDukiRPG.Core.Domain.Characters;
using UkiDukiRPG.Core.Domain.Time;

namespace UkiDukiRPG.Core.Domain.Effects;

public abstract class Effect(string name, IScheduler scheduler)
{
    public string Name { get; } = name;

    public int TimeActivated { get; } = scheduler.CurrentTick;

    protected readonly IScheduler m_Scheduler = scheduler;

    public abstract void Apply(Character caster, Character target);
}

public abstract class InstantEffect(string name, IScheduler scheduler) : Effect(name, scheduler) { }

public enum StatusEffectCategory
{
    Buff,
    Debuff,
}

public abstract class StatusEffect(string name, TimeInterval duration, IScheduler scheduler) : Effect(name, scheduler)
{
    public TimeInterval Duration { get; } = duration;

    protected void ScheduleClear(Character hero) => m_Scheduler.Schedule(() => Clear(hero), Duration);

    public abstract void Clear(Character hero);

    public abstract StatusEffectCategory Category();
}

public abstract class BuffEffect(string name, TimeInterval duration, IScheduler scheduler) : StatusEffect(name, duration, scheduler)
{
    public override StatusEffectCategory Category() => StatusEffectCategory.Buff;
}

public abstract class DebuffEffect(string name, TimeInterval duration, IScheduler scheduler) : StatusEffect(name, duration, scheduler)
{
    public override StatusEffectCategory Category() => StatusEffectCategory.Debuff;
}
