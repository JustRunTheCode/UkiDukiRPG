using UkiDukiRPG.Core.Domain.Characters;
using UkiDukiRPG.Core.Domain.Time;

namespace UkiDukiRPG.Core.Domain.Effects;

//NOTE: Used by Goblin Mage's Mana Drain.
public class MagicDecreaseEffect(
    float                  baseDecrease,
    float                  decreaseFactor,
    TimeInterval           duration,
    Func<Character, float> attackerModifierFunction,
    Func<Character, float> defenderModifierFunction,
    IScheduler             scheduler
) : DebuffEffect(nameof(MagicDecreaseEffect), duration, scheduler)
{
    private readonly Func<Character, float> m_AttackerModifierFunction = attackerModifierFunction;
    private readonly Func<Character, float> m_DefenderModifierFunction = defenderModifierFunction;

    private readonly float m_BaseDecrease   = baseDecrease;
    private readonly float m_DecreaseFactor = decreaseFactor;
    private          int   m_Decrease       = 0;

    public override void Apply(Character caster, Character target)
    {
        var attackerModifier = m_AttackerModifierFunction(caster);
        var defenderModifier = m_DefenderModifierFunction(target);

        m_Decrease = (int)Math.Round(((target.EffectiveAttributes.Magic.Level - m_BaseDecrease) * (1 - m_DecreaseFactor) * attackerModifier * defenderModifier));

        target.InBattleAttributes.Magic.Descend(m_Decrease);

        ScheduleClear(target);
    }

    public override void Clear(Character defender)
    {
        defender.InBattleAttributes.Magic.Ascend(m_Decrease);
    }
}
