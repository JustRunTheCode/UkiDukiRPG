using System.Runtime.CompilerServices;

using MessagePack;

namespace UkiDukiRPG.Core.Domain.Battle.Events;

// @formatter:off
[MessagePackObject]
public record SurrenderEvent(
    [property: Key(0)] Guid InitiatedBy
) : IBattleEvent
// @formatter:on
{
    public BattleEventType Type => BattleEventType.TurnEnded;
}

public partial class BattleEvent
{
    public static class Surrender
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SurrenderEvent Create(Guid initiatedBy) => new(initiatedBy);
    }
}
