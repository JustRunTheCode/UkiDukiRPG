using UkiDukiRPG.Core.Domain.Effects;
using UkiDukiRPG.Core.Domain.Characters;
using UkiDukiRPG.Core.Domain.Time;
using UkiDukiRPG.Core.Domain.Utilities;

namespace UkiDukiRPG.Core.Domain.Abilities;

//NOTE: Effect 1: AttackDecreaseEffect (Target: Defender, Duration: 2 Turns)
public class CurseAbility() : Ability(nameof(CurseAbility), AbilityType.Curse)
{
    private const float c_BaseDecrease   = 0.0f;
    private const float c_DecreaseFactor = 0.25f;

    public override void Use(Combatant caster, Combatant target, ITimeSystem timeSystem)
    {
        var effect = new AttackDecreaseEffect(c_BaseDecrease, c_DecreaseFactor, TimeInterval.FromRounds(2), ModifierFunction.NoEffect, ModifierFunction.NoEffect, timeSystem);

        effect.Apply(caster, target);
    }
}
