using UkiDukiRPG.Core.Domain.Effects;
using UkiDukiRPG.Core.Domain.Characters;
using UkiDukiRPG.Core.Domain.Time;
using UkiDukiRPG.Core.Domain.Utilities;

namespace UkiDukiRPG.Core.Domain.Abilities;

//NOTE: Effect 1: MagicDamageEffect (Target: Defender, Value: Heavy)
public class ShadowBoltAbility() : Ability(nameof(ShadowBoltAbility), AbilityType.ShadowBolt)
{
    private const float c_BaseDamage = 20.0f;

    public override void Use(Combatant caster, Combatant target, IScheduler scheduler)
    {
        var effect = new MagicDamageEffect(c_BaseDamage, ModifierFunction.MagicAmplification, ModifierFunction.NoEffect, scheduler);

        effect.Apply(caster, target);
    }
}
