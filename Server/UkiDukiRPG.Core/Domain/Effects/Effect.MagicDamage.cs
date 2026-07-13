using UkiDukiRPG.Core.Domain.Characters;
using UkiDukiRPG.Core.Domain.Time;

namespace UkiDukiRPG.Core.Domain.Effects;

//NOTE: Used by Shadow Bolt, Drain Life, Flame Breath, Firebolt, Mana Drain.
public class MagicDamageEffect(float baseDamage, Func<Character, float> attackerModifierFunction, Func<Character, float> defenderModifierFunction, IScheduler scheduler)
: InstantEffect(nameof(MagicDamageEffect), scheduler)
{
    private readonly float                  m_BaseDamage               = baseDamage;
    private readonly Func<Character, float> m_AttackerModifierFunction = attackerModifierFunction;
    private readonly Func<Character, float> m_DefenderModifierFunction = defenderModifierFunction;

    public override void Apply(Character caster, Character target)
    {
        var attackerModifier = m_AttackerModifierFunction(caster);
        var defenderModifier = m_DefenderModifierFunction(target);

        var newHealth = target.EffectiveStats.Health.Value - m_BaseDamage * attackerModifier * defenderModifier;

        target.EffectiveStats.Health.Value = float.Max(0f, newHealth);
    }
}
