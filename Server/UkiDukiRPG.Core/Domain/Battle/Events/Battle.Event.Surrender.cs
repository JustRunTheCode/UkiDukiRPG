using System.Runtime.CompilerServices;

using MessagePack;

namespace UkiDukiRPG.Core.Domain.Battle.Events;

// @formatter:off
[MessagePackObject]
public record SurrenderEvent(
    [property: Key(0)] int InitiatedBy
) : IBattleEvent
// @formatter:on
{
    public BattleEventType Type => BattleEventType.Surrender;
}

public partial class BattleEvent
{
    public static class Surrender
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SurrenderEvent Create(int initiatedBy) => new(initiatedBy);
    }
}
