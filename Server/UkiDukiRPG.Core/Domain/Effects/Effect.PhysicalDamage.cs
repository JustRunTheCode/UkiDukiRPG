using UkiDukiRPG.Core.Domain.Characters;
using UkiDukiRPG.Core.Domain.Time;

namespace UkiDukiRPG.Core.Domain.Effects;

//NOTE: Used by Slash, Bite, Pounce, Claw Swipe, Rusty Blade, Dirty Kick, Headbutt, Web Throw.
public class PhysicalDamageEffect(float baseDamage, Func<Character, float> attackerModifierFunction, Func<Character, float> defenderModifierFunction, IScheduler scheduler)
: InstantEffect(nameof(PhysicalDamageEffect), scheduler)
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
