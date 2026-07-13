using UkiDukiRPG.Core.Domain.Characters;
using UkiDukiRPG.Core.Domain.Time;

namespace UkiDukiRPG.Core.Domain.Effects;

[Obsolete("Does not include implementation. Its the remainder that attribute effects can be unified.")]
public class AttributeDecreaseEffect(string name, TimeInterval duration, IScheduler scheduler) : DebuffEffect(nameof(AttributeDecreaseEffect), duration, scheduler)
{
    public override void Apply(Character caster, Character target) { }

    public override void Clear(Character hero) { }
}
