using System.Runtime.CompilerServices;

using MessagePack;

using UkiDukiRPG.Core.Domain.Time;

namespace UkiDukiRPG.Core.Domain.Battle.Events;

// @formatter:off
[MessagePackObject]
public record TurnEndedEvent(
    [property: Key(0)] Guid         CombatantId,
    [property: Key(1)] TimeInterval Duration
) : IBattleEvent
// @formatter:on
{
    public BattleEventType Type => BattleEventType.TurnEnded;
}

public partial class BattleEvent
{
    public static class TurnEnded
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TurnEndedEvent Create(Guid combatantId, TimeInterval duration) => new(combatantId, duration);
    }
}
