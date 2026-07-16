using UkiDukiRPG.Core.Domain.Attributes;
using UkiDukiRPG.Core.Domain.Characters;
using UkiDukiRPG.Core.Domain.Time;
using UkiDukiRPG.Core.Domain.Utilities.Extensions;

namespace UkiDukiRPG.Core.Domain.Effects;

//IN PROGRESS: Check Amount Calculation for all Increase Attributes, might be incorrect 
//NOTE: Used by Knight's Battle Cry and Goblin Warrior's Frenzy.
public class AttackIncreaseEffect(
    float                  baseIncrease,
    float                  increaseFactor,
    TimeInterval           duration,
    Func<Combatant, float> attackerModifierFunction,
    Func<Combatant, float> defenderModifierFunction,
    IScheduler             scheduler
) : BuffEffect(nameof(AttackIncreaseEffect), duration, scheduler)
{
    private readonly Func<Combatant, float> m_AttackerModifierFunction = attackerModifierFunction;
    private readonly Func<Combatant, float> m_DefenderModifierFunction = defenderModifierFunction;

    private readonly float m_BaseIncrease   = baseIncrease;
    private readonly float m_IncreaseFactor = increaseFactor;
    private          int   m_Amount         = 0;

    public override void Apply(Combatant caster, Combatant target)
    {
        var attackerModifier = m_AttackerModifierFunction(caster);
        var defenderModifier = m_DefenderModifierFunction(target);

        m_Amount = (int)Math.Round((target.AttackLevel + m_BaseIncrease) * m_IncreaseFactor * attackerModifier * defenderModifier);

        target.AscendAttribute(AttributeType.Attack, m_Amount);

        ScheduleClear(target);
    }

    public override void Clear(Combatant combatant) => combatant.DescendAttribute(AttributeType.Attack, m_Amount);
}
