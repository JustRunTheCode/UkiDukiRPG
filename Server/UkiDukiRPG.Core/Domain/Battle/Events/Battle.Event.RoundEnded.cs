using System.Runtime.CompilerServices;

using MessagePack;

using UkiDukiRPG.Core.Domain.Time;

namespace UkiDukiRPG.Core.Domain.Battle.Events;

// @formatter:off
[MessagePackObject]
public record RoundEndedEvent(
    [property: Key(0)] TimeInterval Duration
) : IBattleEvent
// @formatter:on
{
    public BattleEventType Type => BattleEventType.RoundEnded;
}

public partial class BattleEvent
{
    public static class RoundEnded
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RoundEndedEvent Create(TimeInterval duration) => new(duration);
    }
}
