using UkiDukiRPG.Core.Domain.Effects;
using UkiDukiRPG.Core.Domain.Characters;
using UkiDukiRPG.Core.Domain.Time;
using UkiDukiRPG.Core.Domain.Utilities;

namespace UkiDukiRPG.Core.Domain.Abilities;

//NOTE: Effect 1: RestoreHealthEffect (Target: Caster, Value: Moderate)
public class SecondWindAbility(IScheduler scheduler) : Ability(nameof(SecondWindAbility))
{
    private const float c_BaseHeal = 15.0f;

    private readonly IScheduler m_Scheduler = scheduler;

    public override void Use(Character caster, Character target)
    {
        var effect = new RestoreHealthEffect(c_BaseHeal, ModifierFunction.MagicAmplification, ModifierFunction.NoEffect, m_Scheduler);

        effect.Apply(caster, caster);
    }
}
