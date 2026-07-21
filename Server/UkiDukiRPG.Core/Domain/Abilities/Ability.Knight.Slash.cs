using UkiDukiRPG.Core.Domain.Effects;
using UkiDukiRPG.Core.Domain.Characters;
using UkiDukiRPG.Core.Domain.Time;
using UkiDukiRPG.Core.Domain.Utilities;

namespace UkiDukiRPG.Core.Domain.Abilities;

//NOTE: Effect 1: PhysicalDamageEffect (Target: Defender, Value: Moderate)
public class SlashAbility() : Ability(nameof(SlashAbility), AbilityType.Slash)
{
    private const float c_BaseDamage = 15.0f;

    public override void Use(Combatant caster, Combatant target, ITimeSystem timeSystem)
    {
        var effect = new PhysicalDamageEffect(c_BaseDamage, ModifierFunction.AttackAmplification, ModifierFunction.DefenseReduction, timeSystem);

        effect.Apply(caster, target);
    }
}
