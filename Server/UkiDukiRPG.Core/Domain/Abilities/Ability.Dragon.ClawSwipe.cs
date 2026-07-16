using UkiDukiRPG.Core.Domain.Effects;
using UkiDukiRPG.Core.Domain.Characters;
using UkiDukiRPG.Core.Domain.Time;
using UkiDukiRPG.Core.Domain.Utilities;

namespace UkiDukiRPG.Core.Domain.Abilities;

//NOTE: Effect 1: PhysicalDamageEffect (Target: Defender, Value: Moderate)
public class ClawSwipeAbility(IScheduler scheduler) : Ability(nameof(ClawSwipeAbility), AbilityType.ClawSwipe)
{
    private const float c_BaseDamage = 15.0f;

    private readonly IScheduler m_Scheduler = scheduler;

    public override void Use(Combatant caster, Combatant target)
    {
        var effect = new PhysicalDamageEffect(c_BaseDamage, ModifierFunction.AttackAmplification, ModifierFunction.DefenseReduction, m_Scheduler);

        effect.Apply(caster, target);
    }
}
