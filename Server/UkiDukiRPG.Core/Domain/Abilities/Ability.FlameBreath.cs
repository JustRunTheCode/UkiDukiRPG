using UkiDukiRPG.Core.Domain.Effects;
using UkiDukiRPG.Core.Domain.Characters;
using UkiDukiRPG.Core.Domain.Time;
using UkiDukiRPG.Core.Domain.Utilities;

namespace UkiDukiRPG.Core.Domain.Abilities;

//NOTE: Effect 1: MagicDamageEffect (Target: Defender, Value: Heavy)
public class FlameBreathAbility(IScheduler scheduler) : Ability(nameof(FlameBreathAbility))
{
    private const float c_BaseDamage = 20.0f;

    private readonly IScheduler m_Scheduler = scheduler;

    public override void Use(Character caster, Character target)
    {
        var effect = new MagicDamageEffect(c_BaseDamage, ModifierFunction.MagicAmplification, ModifierFunction.NoEffect, m_Scheduler);

        effect.Apply(caster, target);
    }
}
