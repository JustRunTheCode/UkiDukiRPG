using System.Runtime.CompilerServices;

using MessagePack;

namespace UkiDukiRPG.Core.Domain.Battle.Events;

// @formatter:off
[MessagePackObject]
public record BattleEndedEvent(
    [property: Key(0)] Guid ChampionId,
    [property: Key(1)] Guid DefeatedId
) : IBattleEvent
// @formatter:on
{
    public BattleEventType Type => BattleEventType.BattleEnded;
}

public partial class BattleEvent
{
    public static class BattleEnded
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BattleEndedEvent Create(Guid championId, Guid defeatedId) => new(championId, defeatedId);
    }
}
