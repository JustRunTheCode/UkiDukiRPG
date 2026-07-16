using UkiDukiRPG.Core.Domain.Attributes;
using UkiDukiRPG.Core.Domain.Characters;
using UkiDukiRPG.Core.Domain.Time;
using UkiDukiRPG.Core.Domain.Utilities.Extensions;

namespace UkiDukiRPG.Core.Domain.Effects;

//NOTE: Used by Knight's Shield Up, Spider's Skitter, Dragon's Dragon Scales, and Goblin Mage's Hex Shield.
public class DefenseIncreaseEffect(
    float                  baseIncrease,
    float                  increaseFactor,
    TimeInterval           duration,
    Func<Combatant, float> attackerModifierFunction,
    Func<Combatant, float> defenderModifierFunction,
    IScheduler             scheduler
) : BuffEffect(nameof(DefenseIncreaseEffect), duration, scheduler)
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

        m_Amount = (int)Math.Round((target.DefenseLevel + m_BaseIncrease) * m_IncreaseFactor * attackerModifier * defenderModifier);

        target.AscendAttribute(AttributeType.Defense, m_Amount);

        ScheduleClear(target);
    }

    public override void Clear(Combatant combatant) => combatant.DescendAttribute(AttributeType.Defense, m_Amount);
}
