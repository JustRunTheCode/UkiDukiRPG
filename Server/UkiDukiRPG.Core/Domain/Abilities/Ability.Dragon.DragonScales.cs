using UkiDukiRPG.Core.Domain.Effects;
using UkiDukiRPG.Core.Domain.Characters;
using UkiDukiRPG.Core.Domain.Time;
using UkiDukiRPG.Core.Domain.Utilities;

namespace UkiDukiRPG.Core.Domain.Abilities;

//NOTE: Effect 1: DefenseIncreaseEffect (Target: Caster, Duration: 2 Turns)
public class DragonScalesAbility(IScheduler scheduler) : Ability(nameof(DragonScalesAbility), AbilityType.DragonScales)
{
    private const float c_BaseIncrease   = 0.0f;
    private const float c_IncreaseFactor = 0.50f;

    private readonly IScheduler m_Scheduler = scheduler;

    public override void Use(Combatant caster, Combatant target)
    {
        var effect = new DefenseIncreaseEffect(c_BaseIncrease, c_IncreaseFactor, TimeInterval.FromRounds(2), ModifierFunction.NoEffect, ModifierFunction.NoEffect, m_Scheduler);

        effect.Apply(caster, caster);
    }
}
