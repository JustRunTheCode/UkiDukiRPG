using System.Runtime.CompilerServices;

using MessagePack;

using UkiDukiRPG.Core.Domain.Effects;
using UkiDukiRPG.Core.Domain.Time;

namespace UkiDukiRPG.Core.Domain.Battle.Events;

// @formatter:off
[MessagePackObject]
public record StatusEffectAppliedEvent(
    [property: Key(0)] Guid             CombatantId,
    [property: Key(1)] StatusEffectType StatusEffect,
    [property: Key(2)] TimeInterval     EffectDuration
) : IBattleEvent
// @formatter:on
{
    public BattleEventType Type => BattleEventType.StatusEffectApplied;
}

public partial class BattleEvent
{
    public static class StatusEffectApplied
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static StatusEffectAppliedEvent Create(Guid combatantId, StatusEffectType statusEffect, TimeInterval effectDuration) => new(combatantId, statusEffect, effectDuration);
    }
}
