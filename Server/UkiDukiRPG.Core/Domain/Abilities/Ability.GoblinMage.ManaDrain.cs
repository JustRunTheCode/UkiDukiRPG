using UkiDukiRPG.Core.Domain.Effects;
using UkiDukiRPG.Core.Domain.Characters;
using UkiDukiRPG.Core.Domain.Time;
using UkiDukiRPG.Core.Domain.Utilities;

namespace UkiDukiRPG.Core.Domain.Abilities;

//NOTE: Effect 1: MagicDamageEffect (Target: Defender, Value: Light)
//      Effect 2: MagicDecreaseEffect (Target: Defender, Duration: 2 Turns)
public class ManaDrainAbility() : Ability(nameof(ManaDrainAbility), AbilityType.ManaDrain)
{
    private const float c_BaseDamage     = 10.0f;
    private const float c_BaseDecrease   = 0.0f;
    private const float c_DecreaseFactor = 0.25f;

    public override void Use(Combatant caster, Combatant target, ITimeSystem timeSystem)
    {
        var effect1 = new MagicDecreaseEffect(c_BaseDecrease, c_DecreaseFactor, TimeInterval.FromRounds(2), ModifierFunction.NoEffect, ModifierFunction.NoEffect, timeSystem);
        var effect2 = new MagicDamageEffect(c_BaseDamage, ModifierFunction.MagicAmplification, ModifierFunction.NoEffect, timeSystem);

        effect1.Apply(caster, target);
        effect2.Apply(caster, target);
    }
}
