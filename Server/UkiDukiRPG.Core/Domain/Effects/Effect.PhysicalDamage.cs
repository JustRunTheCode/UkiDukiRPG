using UkiDukiRPG.Core.Domain.Battle;
using UkiDukiRPG.Core.Domain.Battle.Events;
using UkiDukiRPG.Core.Domain.Stats;
using UkiDukiRPG.Core.Domain.Utilities.Extensions;

using Combatant = UkiDukiRPG.Core.Domain.Battle.Combatant;

namespace UkiDukiRPG.Core.Domain.Effects;

// @formatter:off
//NOTE: Used by Slash, Bite, Pounce, Claw Swipe, Rusty Blade, Dirty Kick, Headbutt, Web Throw.
public class PhysicalDamageEffect(
    float                  baseDamage,
    Func<Combatant, float> casterModifierFunction,
    Func<Combatant, float> targetModifierFunction
) : InstantEffect(nameof(PhysicalDamageEffect))
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
