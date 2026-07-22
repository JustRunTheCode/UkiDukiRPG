using UkiDukiRPG.Core.Domain.Battle;
using UkiDukiRPG.Core.Domain.Battle.Events;
using UkiDukiRPG.Core.Domain.Stats;
using UkiDukiRPG.Core.Domain.Utilities.Extensions;

using Combatant = UkiDukiRPG.Core.Domain.Battle.Combatant;

namespace UkiDukiRPG.Core.Domain.Effects;

// @formatter:off
//NOTE: Used by the Knight's Second Wind.
public class RestoreHealthEffect(
    float baseHeal,
    Func<Combatant, float> casterModifierFunction,
    Func<Combatant, float> targetModifierFunction
) : InstantEffect(nameof(RestoreHealthEffect))
// @formatter:on
{
    private readonly float                  m_BaseHeal               = baseHeal;
    private readonly Func<Combatant, float> m_CasterModifierFunction = casterModifierFunction;
    private readonly Func<Combatant, float> m_TargetModifierFunction = targetModifierFunction;

    public override void Apply(Combatant caster, Combatant target, IBattleEngine battle)
    {
        var casterModifier = m_CasterModifierFunction(caster);
        var targetModifier = m_TargetModifierFunction(target);

        var restoreHealth = float.Min(target.MaxHealth - target.CurrentHealth, m_BaseHeal * casterModifier * targetModifier);

        target.IncreaseStat(CombatStatType.Health, restoreHealth);

        battle.AddEvent(BattleEvent.CombatantHeal.Create(target.Id, restoreHealth));
    }
}
