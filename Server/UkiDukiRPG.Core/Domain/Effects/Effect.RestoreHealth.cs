using UkiDukiRPG.Core.Domain.Characters;
using UkiDukiRPG.Core.Domain.Stats;
using UkiDukiRPG.Core.Domain.Time;
using UkiDukiRPG.Core.Domain.Utilities.Extensions;

namespace UkiDukiRPG.Core.Domain.Effects;

//NOTE: Used by the Knight's Second Wind.
public class RestoreHealthEffect(float baseHeal, Func<Combatant, float> attackerModifierFunction, Func<Combatant, float> defenderModifierFunction, IScheduler scheduler)
: InstantEffect(nameof(RestoreHealthEffect), scheduler)
{
    private readonly float                  m_BaseHeal                 = baseHeal;
    private readonly Func<Combatant, float> m_AttackerModifierFunction = attackerModifierFunction;
    private readonly Func<Combatant, float> m_DefenderModifierFunction = defenderModifierFunction;

    public override void Apply(Combatant caster, Combatant target)
    {
        var attackerModifier = m_AttackerModifierFunction(caster);
        var defenderModifier = m_DefenderModifierFunction(target);

        var restoreHealth = float.Min(target.MaxHealth - target.CurrentHealth, m_BaseHeal * attackerModifier * defenderModifier);

        target.IncreaseStat(CombatStatType.Health, restoreHealth);
    }
}
