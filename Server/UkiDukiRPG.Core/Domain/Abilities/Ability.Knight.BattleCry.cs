using UkiDukiRPG.Core.Domain.Effects;
using UkiDukiRPG.Core.Domain.Characters;
using UkiDukiRPG.Core.Domain.Time;
using UkiDukiRPG.Core.Domain.Utilities;

namespace UkiDukiRPG.Core.Domain.Abilities;

//NOTE: Effect 1: AttackIncreaseEffect (Target: Caster, Duration: 2 Round)
public class BattleCryAbility() : Ability(nameof(BattleCryAbility), AbilityType.BattleCry)
{
    private const float c_BaseIncrease   = 0.0f;
    private const float c_IncreaseFactor = 0.50f;

    public override void Use(Combatant caster, Combatant target, ITimeSystem timeSystem)
    {
        var effect = new AttackIncreaseEffect(c_BaseIncrease, c_IncreaseFactor, TimeInterval.FromRounds(2), ModifierFunction.NoEffect, ModifierFunction.NoEffect, timeSystem);

        effect.Apply(caster, caster);
    }
}
