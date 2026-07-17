using UkiDukiRPG.Core.Domain.Attributes;
using UkiDukiRPG.Core.Domain.Characters;
using UkiDukiRPG.Core.Domain.Time;
using UkiDukiRPG.Core.Domain.Utilities.Extensions;

namespace UkiDukiRPG.Core.Domain.Effects;

//NOTE: Used by Knight's Battle Cry and Goblin Warrior's Frenzy.
public class AttackIncreaseEffect(
    float                  baseIncrease,
    float                  increaseFactor,
    TimeInterval           duration,
    Func<Combatant, float> casterModifierFunction,
    Func<Combatant, float> targetModifierFunction,
    IScheduler             scheduler
) : BuffEffect(nameof(AttackIncreaseEffect), duration, scheduler)
{
    private readonly Func<Combatant, float> m_CasterModifierFunction = casterModifierFunction;
    private readonly Func<Combatant, float> m_TargetModifierFunction = targetModifierFunction;

    private readonly float m_BaseIncrease   = baseIncrease;
    private readonly float m_IncreaseFactor = increaseFactor;
    private          int   m_Amount         = 0;

    public override void Apply(Combatant caster, Combatant target)
    {
        var casterModifier = m_CasterModifierFunction(caster);
        var targetModifier = m_TargetModifierFunction(target);

        m_Amount = (int)float.Round((m_BaseIncrease + target.AttackLevel * m_IncreaseFactor) * casterModifier * targetModifier);

        target.AscendAttribute(AttributeType.Attack, m_Amount);

        ScheduleClear(target);
    }

    public override void Clear(Combatant combatant) => combatant.DescendAttribute(AttributeType.Attack, m_Amount);
}
