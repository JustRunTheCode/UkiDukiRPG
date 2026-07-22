using UkiDukiRPG.Core.Domain.Battle;
using UkiDukiRPG.Core.Domain.Battle.Events;
using UkiDukiRPG.Core.Domain.Effects;
using UkiDukiRPG.Core.Domain.Time;
using UkiDukiRPG.Core.Domain.Utilities;

namespace UkiDukiRPG.Core.Domain.Abilities;

//NOTE: Effect 1: PhysicalDamageEffect (Target: Defender, Value: Light)
//      Effect 2: DefenseDecreaseEffect (Target: Defender, Duration: 2 Turns)
public class WebThrowAbility() : Ability(nameof(WebThrowAbility), AbilityType.WebThrow)
{
    private const float c_BaseDamage     = 10.0f;
    private const float c_BaseDecrease   = 0.0f;
    private const float c_DecreaseFactor = 0.25f;

    public override void Use(Combatant caster, Combatant target, IBattleEngine battle)
    {
        battle.AddEvent(BattleEvent.AbilityUsed.Create(caster.Id, target.Id, Type));

        var effect1 = new DefenseDecreaseEffect(c_BaseDecrease, c_DecreaseFactor, TimeInterval.FromRounds(2), ModifierFunction.NoEffect, ModifierFunction.NoEffect);
        var effect2 = new PhysicalDamageEffect(c_BaseDamage, ModifierFunction.AttackAmplification, ModifierFunction.DefenseReduction);

        effect1.Apply(caster, target, battle);
        effect2.Apply(caster, target, battle);
    }
}
