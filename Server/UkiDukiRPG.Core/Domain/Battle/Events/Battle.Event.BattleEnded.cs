using System.Runtime.CompilerServices;

using MessagePack;

namespace UkiDukiRPG.Core.Domain.Battle.Events;

// @formatter:off
[MessagePackObject]
public record BattleEndedEvent(
    [property: Key(0)] CombatantType Winner
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
        public static BattleEndedEvent Create(CombatantType winner) => new(winner);
    }
}
