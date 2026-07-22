using UkiDukiRPG.Core.Domain.Battle;
using UkiDukiRPG.Core.Domain.Battle.Events;
using UkiDukiRPG.Core.Domain.Effects;
using UkiDukiRPG.Core.Domain.Time;
using UkiDukiRPG.Core.Domain.Utilities;

namespace UkiDukiRPG.Core.Domain.Abilities;

//NOTE: Effect 1: AttackIncreaseEffect (Target: Caster, Duration: 2 Turns)
public class FrenzyAbility() : Ability(nameof(FrenzyAbility), AbilityType.Frenzy)
{
    private const float c_BaseIncrease   = 0.0f;
    private const float c_IncreaseFactor = 0.50f;

    public override void Use(Combatant caster, Combatant target, IBattleEngine battle)
    {
        battle.AddEvent(BattleEvent.AbilityUsed.Create(caster.Id, target.Id, Type));

        var effect = new AttackIncreaseEffect(c_BaseIncrease, c_IncreaseFactor, TimeInterval.FromRounds(2), ModifierFunction.NoEffect, ModifierFunction.NoEffect);

        effect.Apply(caster, target, battle);
    }
}
