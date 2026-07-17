using UkiDukiRPG.Core.Domain.Characters;
using UkiDukiRPG.Core.Domain.Stats;
using UkiDukiRPG.Core.Domain.Time;
using UkiDukiRPG.Core.Domain.Utilities.Extensions;

namespace UkiDukiRPG.Core.Domain.Effects;

//NOTE: Used by the Knight's Second Wind.
public class RestoreHealthEffect(float baseHeal, Func<Combatant, float> casterModifierFunction, Func<Combatant, float> targetModifierFunction, IScheduler scheduler)
: InstantEffect(nameof(RestoreHealthEffect), scheduler)
{
    private readonly float                  m_BaseHeal               = baseHeal;
    private readonly Func<Combatant, float> m_CasterModifierFunction = casterModifierFunction;
    private readonly Func<Combatant, float> m_TargetModifierFunction = targetModifierFunction;

    public override void Apply(Combatant caster, Combatant target)
    {
        var casterModifier = m_CasterModifierFunction(caster);
        var targetModifier = m_TargetModifierFunction(target);

        var restoreHealth = float.Min(target.MaxHealth - target.CurrentHealth, m_BaseHeal * casterModifier * targetModifier);

        target.IncreaseStat(CombatStatType.Health, restoreHealth);
    }
}
