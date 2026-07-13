using UkiDukiRPG.Core.Domain.Effects;
using UkiDukiRPG.Core.Domain.Characters;
using UkiDukiRPG.Core.Domain.Time;
using UkiDukiRPG.Core.Domain.Utilities;

namespace UkiDukiRPG.Core.Domain.Abilities;

//NOTE: Effect 1: AttackDecreaseEffect (Target: Defender, Duration: 2 Turns)
public class CurseAbility(IScheduler scheduler) : Ability(nameof(CurseAbility))
{
    private const float c_BaseDecrease   = 0.0f;
    private const float c_DecreaseFactor = 0.25f;

    private readonly IScheduler m_Scheduler = scheduler;

    public override void Use(Character caster, Character target)
    {
        var effect = new AttackDecreaseEffect(c_BaseDecrease, c_DecreaseFactor, TimeInterval.FromRounds(2), ModifierFunction.NoEffect, ModifierFunction.NoEffect, m_Scheduler);

        effect.Apply(caster, target);
    }
}
