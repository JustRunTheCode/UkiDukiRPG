using UkiDukiRPG.Core.Domain.Attributes;
using UkiDukiRPG.Core.Domain.Characters;
using UkiDukiRPG.Core.Domain.Time;
using UkiDukiRPG.Core.Domain.Utilities.Extensions;

namespace UkiDukiRPG.Core.Domain.Effects;

//NOTE: Used by Spider's Web Throw and Goblin Warrior's Dirty Kick.
public class DefenseDecreaseEffect(
    float                  baseDecrease,
    float                  decreaseFactor,
    TimeInterval           duration,
    Func<Combatant, float> casterModifierFunction,
    Func<Combatant, float> targetModifierFunction,
    IScheduler             scheduler
) : DebuffEffect(nameof(DefenseDecreaseEffect), StatusEffectType.DefenseDecrease, duration, scheduler)
{
    private readonly Func<Combatant, float> m_CasterModifierFunction = casterModifierFunction;
    private readonly Func<Combatant, float> m_TargetModifierFunction = targetModifierFunction;

    private readonly float m_BaseDecrease   = baseDecrease;
    private readonly float m_DecreaseFactor = decreaseFactor;
    private          int   m_Amount         = 0;

    public override void Apply(Combatant caster, Combatant target)
    {
        var casterModifier = m_CasterModifierFunction(caster);
        var targetModifier = m_TargetModifierFunction(target);

        m_Amount = int.Min(target.DefenseLevel, (int)float.Round((m_BaseDecrease + target.DefenseLevel * m_DecreaseFactor) * casterModifier * targetModifier));

        target.DescendAttribute(AttributeType.Defense, m_Amount);

        ScheduleClear(target);
    }

    public override void Clear(Combatant combatant) => combatant.AscendAttribute(AttributeType.Defense, m_Amount);
}
