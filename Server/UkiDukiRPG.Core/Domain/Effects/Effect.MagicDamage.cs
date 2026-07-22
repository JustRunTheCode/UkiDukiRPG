using UkiDukiRPG.Core.Domain.Battle;
using UkiDukiRPG.Core.Domain.Battle.Events;
using UkiDukiRPG.Core.Domain.Stats;
using UkiDukiRPG.Core.Domain.Utilities.Extensions;

using Combatant = UkiDukiRPG.Core.Domain.Battle.Combatant;

namespace UkiDukiRPG.Core.Domain.Effects;

// @formatter:off
//NOTE: Used by Shadow Bolt, Drain Life, Flame Breath, Firebolt, Mana Drain.
public class MagicDamageEffect(
    float                  baseDamage,
    Func<Combatant, float> casterModifierFunction,
    Func<Combatant, float> targetModifierFunction
) : InstantEffect(nameof(MagicDamageEffect))
// @formatter:on
{
    private readonly float                  m_BaseDamage             = baseDamage;
    private readonly Func<Combatant, float> m_CasterModifierFunction = casterModifierFunction;
    private readonly Func<Combatant, float> m_TargetModifierFunction = targetModifierFunction;

    public override void Apply(Combatant caster, Combatant target, IBattleEngine battle)
    {
        var casterModifier = m_CasterModifierFunction(caster);
        var targetModifier = m_TargetModifierFunction(target);

        var healthDecrease = float.Min(target.CurrentHealth, m_BaseDamage * casterModifier * targetModifier);

        target.DecreaseStat(CombatStatType.Health, healthDecrease);
        
        battle.AddEvent(BattleEvent.CombatantHurt.Create(target.Id, healthDecrease));
    }
}
