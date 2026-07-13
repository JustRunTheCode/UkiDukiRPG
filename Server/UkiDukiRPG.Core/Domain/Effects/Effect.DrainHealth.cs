using UkiDukiRPG.Core.Domain.Characters;
using UkiDukiRPG.Core.Domain.Time;

namespace UkiDukiRPG.Core.Domain.Effects;

//NOTE: Used by the Witch's Dark Pact
public class DrainHealthEffect(float baseDamage, Func<Character, float> attackerModifierFunction, Func<Character, float> defenderModifierFunction, IScheduler scheduler)
: InstantEffect(nameof(DrainHealthEffect), scheduler)
{
    private readonly float                  m_BaseDamage               = baseDamage;
    private readonly Func<Character, float> m_AttackerModifierFunction = attackerModifierFunction;
    private readonly Func<Character, float> m_DefenderModifierFunction = defenderModifierFunction;

    public override void Apply(Character caster, Character target)
    {
        var attackerModifier = m_AttackerModifierFunction(caster);
        var defenderModifier = m_DefenderModifierFunction(target);

        var newHealth = caster.InBattleStats.Health.Value - m_BaseDamage * attackerModifier * defenderModifier;

        caster.InBattleStats.Health.Value = float.Max(0f, newHealth);
    }
}
