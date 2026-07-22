using UkiDukiRPG.Core.Domain.Attributes;
using UkiDukiRPG.Core.Domain.Battle;
using UkiDukiRPG.Core.Domain.Battle.Events;
using UkiDukiRPG.Core.Domain.Time;
using UkiDukiRPG.Core.Domain.Utilities.Extensions;

using Combatant = UkiDukiRPG.Core.Domain.Battle.Combatant;

namespace UkiDukiRPG.Core.Domain.Effects;

// @formatter:off
//NOTE: Used by Knight's Battle Cry and Goblin Warrior's Frenzy.
public class AttackIncreaseEffect(
    float                  baseIncrease,
    float                  increaseFactor,
    TimeInterval           duration,
    Func<Combatant, float> casterModifierFunction,
    Func<Combatant, float> targetModifierFunction
) : BuffEffect(nameof(AttackIncreaseEffect), StatusEffectType.AttackIncrease, duration)
// @formatter:on
{
    private readonly Func<Combatant, float> m_CasterModifierFunction = casterModifierFunction;
    private readonly Func<Combatant, float> m_TargetModifierFunction = targetModifierFunction;

    private readonly float m_BaseIncrease   = baseIncrease;
    private readonly float m_IncreaseFactor = increaseFactor;
    private          int   m_Amount         = 0;

    public override void Apply(Combatant caster, Combatant target, IBattleEngine battle)
    {
        var casterModifier = m_CasterModifierFunction(caster);
        var targetModifier = m_TargetModifierFunction(target);

        m_Amount = (int)float.Round((m_BaseIncrease + target.AttackLevel * m_IncreaseFactor) * casterModifier * targetModifier);

        target.AscendAttribute(AttributeType.Attack, m_Amount);

        battle.AddEvent(BattleEvent.StatusEffectApplied.Create(target.Id, Type, Duration));
        
        battle.TimeSystem.Schedule(() => Clear(target), Duration);
    }

    public override void Clear(Combatant combatant) => combatant.DescendAttribute(AttributeType.Attack, m_Amount);
}
