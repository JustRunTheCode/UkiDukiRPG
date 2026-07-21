using UkiDukiRPG.Core.Domain.Effects;
using UkiDukiRPG.Core.Domain.Characters;
using UkiDukiRPG.Core.Domain.Time;
using UkiDukiRPG.Core.Domain.Utilities;
using UkiDukiRPG.Core.Domain.Utilities.Extensions;

namespace UkiDukiRPG.Core.Domain.Abilities;

//NOTE: Effect 1: MagicDamageEffect (Target: Defender, Value: Light)
//      Effect 2: RestoreHealthEffect (Target: Caster, Value: Light)
public class DrainLifeAbility() : Ability(nameof(DrainLifeAbility), AbilityType.DrainLife)
{
    private const float c_BaseDamage = 7.5f;

    public override void Use(Combatant caster, Combatant target, ITimeSystem timeSystem)
    {
        var effect1 = new MagicDamageEffect(c_BaseDamage, ModifierFunction.MagicAmplification, ModifierFunction.NoEffect, timeSystem);

        var oldHealth = target.CurrentHealth;

        effect1.Apply(caster, target);

        var healthTaken = oldHealth - target.CurrentHealth;

        var effect2 = new RestoreHealthEffect(healthTaken, ModifierFunction.NoEffect, ModifierFunction.NoEffect, timeSystem);

        effect2.Apply(caster, caster);
    }
}
