using UkiDukiRPG.Core.Domain.Effects;
using UkiDukiRPG.Core.Domain.Characters;
using UkiDukiRPG.Core.Domain.Time;
using UkiDukiRPG.Core.Domain.Utilities;

namespace UkiDukiRPG.Core.Domain.Abilities;

//NOTE: Effect 1: MagicDamageEffect (Target: Defender, Value: Light)
//      Effect 2: RestoreHealthEffect (Target: Caster, Value: Light)
public class DrainLifeAbility(IScheduler scheduler) : Ability(nameof(DrainLifeAbility), AbilityType.DrainLife)
{
    private const float c_BaseDamage = 7.5f;

    private readonly IScheduler m_Scheduler = scheduler;

    public override void Use(Character caster, Character target)
    {
        var effect1 = new MagicDamageEffect(c_BaseDamage, ModifierFunction.MagicAmplification, ModifierFunction.NoEffect, m_Scheduler);

        var oldHealth = target.EffectiveStats.Health.Value;

        effect1.Apply(caster, target);

        var healthTaken = oldHealth - target.EffectiveStats.Health.Value;

        var effect2 = new RestoreHealthEffect(healthTaken, ModifierFunction.NoEffect, ModifierFunction.NoEffect, m_Scheduler);

        effect2.Apply(caster, caster);
    }
}
