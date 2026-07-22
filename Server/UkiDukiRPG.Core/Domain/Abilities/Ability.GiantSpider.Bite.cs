using UkiDukiRPG.Core.Domain.Battle;
using UkiDukiRPG.Core.Domain.Battle.Events;
using UkiDukiRPG.Core.Domain.Effects;
using UkiDukiRPG.Core.Domain.Utilities;

namespace UkiDukiRPG.Core.Domain.Abilities;

//NOTE: Effect 1: PhysicalDamageEffect (Target: Defender, Value: Moderate)
public class BiteAbility() : Ability(nameof(BiteAbility), AbilityType.Bite)
{
    private const float c_BaseDamage = 15.0f;

    public override void Use(Combatant caster, Combatant target, IBattleEngine battle)
    {
        battle.AddEvent(BattleEvent.AbilityUsed.Create(caster.Id, target.Id, Type));

        var effect = new PhysicalDamageEffect(c_BaseDamage, ModifierFunction.AttackAmplification, ModifierFunction.DefenseReduction);

        effect.Apply(caster, target, battle);
    }
}
