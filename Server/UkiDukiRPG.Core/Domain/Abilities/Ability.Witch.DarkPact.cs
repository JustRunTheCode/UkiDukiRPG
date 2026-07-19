using UkiDukiRPG.Core.Domain.Effects;
using UkiDukiRPG.Core.Domain.Characters;
using UkiDukiRPG.Core.Domain.Time;
using UkiDukiRPG.Core.Domain.Utilities;

namespace UkiDukiRPG.Core.Domain.Abilities;

//NOTE: Effect 1: DrainHealthEffect (Target: Caster, Value: Light)
//      Effect 2: MagicIncreaseEffect (Target: Caster, Duration: 2 Turns)
public class DarkPactAbility() : Ability(nameof(DarkPactAbility), AbilityType.DarkPact)
{
    private const float c_BaseDamage     = 5.0f;
    private const float c_BaseIncrease   = 0.0f;
    private const float c_IncreaseFactor = 0.60f;

    public override void Use(Combatant caster, Combatant target, IScheduler scheduler)
    {
        var effect1 = new DrainHealthEffect(c_BaseDamage, ModifierFunction.NoEffect, ModifierFunction.NoEffect, scheduler);
        var effect2 = new MagicIncreaseEffect(c_BaseIncrease, c_IncreaseFactor, TimeInterval.FromRounds(2), ModifierFunction.NoEffect, ModifierFunction.NoEffect, scheduler);

        effect1.Apply(caster, caster);
        effect2.Apply(caster, caster);
    }
}
