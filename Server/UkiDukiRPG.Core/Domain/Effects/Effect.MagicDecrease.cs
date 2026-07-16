using UkiDukiRPG.Core.Domain.Attributes;
using UkiDukiRPG.Core.Domain.Characters;
using UkiDukiRPG.Core.Domain.Time;
using UkiDukiRPG.Core.Domain.Utilities.Extensions;

namespace UkiDukiRPG.Core.Domain.Effects;

//NOTE: Used by Goblin Mage's Mana Drain.
public class MagicDecreaseEffect(
    float                  baseDecrease,
    float                  decreaseFactor,
    TimeInterval           duration,
    Func<Combatant, float> attackerModifierFunction,
    Func<Combatant, float> defenderModifierFunction,
    IScheduler             scheduler
) : DebuffEffect(nameof(MagicDecreaseEffect), duration, scheduler)
{
    private readonly Func<Combatant, float> m_AttackerModifierFunction = attackerModifierFunction;
    private readonly Func<Combatant, float> m_DefenderModifierFunction = defenderModifierFunction;

    private readonly float m_BaseDecrease   = baseDecrease;
    private readonly float m_DecreaseFactor = decreaseFactor;
    private          int   m_Amount         = 0;

    public override void Apply(Combatant caster, Combatant target)
    {
        var attackerModifier = m_AttackerModifierFunction(caster);
        var defenderModifier = m_DefenderModifierFunction(target);

        m_Amount = (int)Math.Round(((target.MagicLevel - m_BaseDecrease) * (1 - m_DecreaseFactor) * attackerModifier * defenderModifier));

        target.DescendAttribute(AttributeType.Magic, m_Amount);

        ScheduleClear(target);
    }

    public override void Clear(Combatant combatant) => combatant.AscendAttribute(AttributeType.Magic, m_Amount);
}
