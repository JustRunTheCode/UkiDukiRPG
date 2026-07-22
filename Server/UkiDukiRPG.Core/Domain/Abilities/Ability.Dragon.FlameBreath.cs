using UkiDukiRPG.Core.Domain.Battle;
using UkiDukiRPG.Core.Domain.Battle.Events;
using UkiDukiRPG.Core.Domain.Effects;
using UkiDukiRPG.Core.Domain.Utilities;

namespace UkiDukiRPG.Core.Domain.Abilities;

//NOTE: Effect 1: MagicDamageEffect (Target: Defender, Value: Heavy)
public class FlameBreathAbility() : Ability(nameof(FlameBreathAbility), AbilityType.FlameBreath)
{
    private const float c_BaseDamage = 20.0f;

    public override void Use(Combatant caster, Combatant target, IBattleEngine battle)
    {
        battle.AddEvent(BattleEvent.AbilityUsed.Create(caster.Id, target.Id, Type));

        var effect = new MagicDamageEffect(c_BaseDamage, ModifierFunction.MagicAmplification, ModifierFunction.NoEffect);

        effect.Apply(caster, target, battle);
    }
}
