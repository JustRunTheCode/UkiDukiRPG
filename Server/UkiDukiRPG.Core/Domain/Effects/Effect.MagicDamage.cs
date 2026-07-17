using UkiDukiRPG.Core.Domain.Characters;
using UkiDukiRPG.Core.Domain.Stats;
using UkiDukiRPG.Core.Domain.Time;
using UkiDukiRPG.Core.Domain.Utilities.Extensions;

namespace UkiDukiRPG.Core.Domain.Effects;

//NOTE: Used by Shadow Bolt, Drain Life, Flame Breath, Firebolt, Mana Drain.
public class MagicDamageEffect(float baseDamage, Func<Combatant, float> casterModifierFunction, Func<Combatant, float> targetModifierFunction, IScheduler scheduler)
: InstantEffect(nameof(MagicDamageEffect), scheduler)
{
    private readonly float                  m_BaseDamage             = baseDamage;
    private readonly Func<Combatant, float> m_CasterModifierFunction = casterModifierFunction;
    private readonly Func<Combatant, float> m_TargetModifierFunction = targetModifierFunction;

    public override void Apply(Combatant caster, Combatant target)
    {
        var casterModifier = m_CasterModifierFunction(caster);
        var targetModifier = m_TargetModifierFunction(target);

        var healthDecrease = float.Min(target.CurrentHealth, m_BaseDamage * casterModifier * targetModifier);

        target.DecreaseStat(CombatStatType.Health, healthDecrease);
    }
}
