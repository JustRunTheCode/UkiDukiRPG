using UkiDukiRPG.Core.Domain.Characters;
using UkiDukiRPG.Core.Domain.Time;

namespace UkiDukiRPG.Core.Domain.Effects;

//NOTE: Used by the Knight's Second Wind.
public class RestoreHealthEffect(float baseHeal, Func<Character, float> attackerModifierFunction, Func<Character, float> defenderModifierFunction, IScheduler scheduler)
: InstantEffect(nameof(RestoreHealthEffect), scheduler)
{
    private readonly float                  m_BaseHeal                 = baseHeal;
    private readonly Func<Character, float> m_AttackerModifierFunction = attackerModifierFunction;
    private readonly Func<Character, float> m_DefenderModifierFunction = defenderModifierFunction;

    public override void Apply(Character caster, Character target)
    {
        var attackerModifier = m_AttackerModifierFunction(caster);
        var defenderModifier = m_DefenderModifierFunction(target);

        var maxHealth = caster.EffectiveStats.Health.Value;
        var newHealth = caster.EffectiveStats.Health.Value + m_BaseHeal * attackerModifier * defenderModifier;

        caster.EffectiveStats.Health.Value = float.Min(maxHealth, newHealth);
    }
}
