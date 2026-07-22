using UkiDukiRPG.Core.Domain.Attributes;
using UkiDukiRPG.Core.Domain.Battle;
using UkiDukiRPG.Core.Domain.Battle.Events;
using UkiDukiRPG.Core.Domain.Time;
using UkiDukiRPG.Core.Domain.Utilities.Extensions;

using Combatant = UkiDukiRPG.Core.Domain.Battle.Combatant;

namespace UkiDukiRPG.Core.Domain.Effects;

// @formatter:off
//NOTE: Used by Goblin Mage's Mana Drain.
public class MagicDecreaseEffect(
    float                  baseDecrease,
    float                  decreaseFactor,
    TimeInterval           duration,
    Func<Combatant, float> casterModifierFunction,
    Func<Combatant, float> targetModifierFunction
) : DebuffEffect(nameof(MagicDecreaseEffect), StatusEffectType.MagicDecrease, duration)
// @formatter:on
{
    private readonly Func<Combatant, float> m_CasterModifierFunction = casterModifierFunction;
    private readonly Func<Combatant, float> m_TargetModifierFunction = targetModifierFunction;

    private readonly float m_BaseDecrease   = baseDecrease;
    private readonly float m_DecreaseFactor = decreaseFactor;
    private          int   m_Amount         = 0;

    public override void Apply(Combatant caster, Combatant target, IBattleEngine battle)
    {
        var casterModifier = m_CasterModifierFunction(caster);
        var targetModifier = m_TargetModifierFunction(target);

        m_Amount = int.Min(target.MagicLevel, (int)float.Round((m_BaseDecrease + target.MagicLevel * m_DecreaseFactor) * casterModifier * targetModifier));

        target.DescendAttribute(AttributeType.Magic, m_Amount);

        target.AddStatusEffect(Type);

        battle.AddEvent(BattleEvent.StatusEffectApplied.Create(target.Id, Type, Duration));

        battle.TimeSystem.Schedule(() => Clear(target), Duration);
    }

    public override void Clear(Combatant combatant)
    {
        combatant.RemoveStatusEffect(Type);

        combatant.AscendAttribute(AttributeType.Magic, m_Amount);
    }
}
