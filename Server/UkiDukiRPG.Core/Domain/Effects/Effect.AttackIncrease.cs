using UkiDukiRPG.Core.Domain.Characters;
using UkiDukiRPG.Core.Domain.Time;

namespace UkiDukiRPG.Core.Domain.Effects;

//NOTE: Used by Knight's Battle Cry and Goblin Warrior's Frenzy.
public class AttackIncreaseEffect(
    float                  baseIncrease,
    float                  increaseFactor,
    TimeInterval           duration,
    Func<Character, float> attackerModifierFunction,
    Func<Character, float> defenderModifierFunction,
    IScheduler             scheduler
) : BuffEffect(nameof(AttackIncreaseEffect), duration, scheduler)
{
    private readonly Func<Character, float> m_AttackerModifierFunction = attackerModifierFunction;
    private readonly Func<Character, float> m_DefenderModifierFunction = defenderModifierFunction;

    private readonly float m_BaseIncrease   = baseIncrease;
    private readonly float m_IncreaseFactor = increaseFactor;
    private          int   m_Increase       = 0;

    public override void Apply(Character caster, Character target)
    {
        var attackerModifier = m_AttackerModifierFunction(caster);
        var defenderModifier = m_DefenderModifierFunction(target);

        m_Increase = (int)Math.Round((caster.EffectiveAttributes.Attack.Level + m_BaseIncrease) * m_IncreaseFactor * attackerModifier * defenderModifier);

        caster.InBattleAttributes.Attack.Ascend(m_Increase);

        ScheduleClear(caster);
    }

    public override void Clear(Character attacker)
    {
        attacker.InBattleAttributes.Attack.Descend(m_Increase);
    }
}
