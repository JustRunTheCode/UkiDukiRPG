using UkiDukiRPG.Core.Domain.Characters;
using UkiDukiRPG.Core.Domain.Stats;
using UkiDukiRPG.Core.Domain.Time;
using UkiDukiRPG.Core.Domain.Utilities.Extensions;

namespace UkiDukiRPG.Core.Domain.Effects;

//NOTE: Used by Slash, Bite, Pounce, Claw Swipe, Rusty Blade, Dirty Kick, Headbutt, Web Throw.
public class PhysicalDamageEffect(float baseDamage, Func<Combatant, float> attackerModifierFunction, Func<Combatant, float> defenderModifierFunction, IScheduler scheduler)
: InstantEffect(nameof(PhysicalDamageEffect), scheduler)
{
    private readonly float                  m_BaseDamage               = baseDamage;
    private readonly Func<Combatant, float> m_AttackerModifierFunction = attackerModifierFunction;
    private readonly Func<Combatant, float> m_DefenderModifierFunction = defenderModifierFunction;

    public override void Apply(Combatant caster, Combatant target)
    {
        var attackerModifier = m_AttackerModifierFunction(caster);
        var defenderModifier = m_DefenderModifierFunction(target);

        var healthDecrease = float.Min(target.CurrentHealth, m_BaseDamage * attackerModifier * defenderModifier);

        target.DecreaseStat(CombatStatType.Health, healthDecrease);
    }
}
