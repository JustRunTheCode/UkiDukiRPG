using UkiDukiRPG.Core.Domain.Effects;
using UkiDukiRPG.Core.Domain.Characters;
using UkiDukiRPG.Core.Domain.Time;
using UkiDukiRPG.Core.Domain.Utilities;

namespace UkiDukiRPG.Core.Domain.Abilities;

//NOTE: Effect 1: PhysicalDamageEffect (Target: Defender, Value: Light)
//      Effect 2: DefenseDecreaseEffect (Target: Defender, Duration: 2 Turns)
public class DirtyKickAbility(IScheduler scheduler) : Ability(nameof(DirtyKickAbility), AbilityType.DirtyKick)
{
    private const float c_BaseDamage     = 10.0f;
    private const float c_BaseDecrease   = 0.0f;
    private const float c_DecreaseFactor = 0.25f;

    private readonly IScheduler m_Scheduler = scheduler;

    public override void Use(Character caster, Character target)
    {
        var effect1 = new DefenseDecreaseEffect(c_BaseDecrease, c_DecreaseFactor, TimeInterval.FromRounds(2), ModifierFunction.NoEffect, ModifierFunction.NoEffect, m_Scheduler);
        var effect2 = new PhysicalDamageEffect(c_BaseDamage, ModifierFunction.AttackAmplification, ModifierFunction.DefenseReduction, m_Scheduler);

        effect1.Apply(caster, target);
        effect2.Apply(caster, target);
    }
}
