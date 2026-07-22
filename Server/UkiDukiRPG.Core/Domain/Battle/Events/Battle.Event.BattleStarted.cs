using System.Runtime.CompilerServices;

using MessagePack;

namespace UkiDukiRPG.Core.Domain.Battle.Events;

// @formatter:off
[MessagePackObject]
public record BattleStartedEvent(
    [property: Key(0)] int ChallengerId,
    [property: Key(1)] int EncounterId
) : IBattleEvent
// @formatter:on
{
    public BattleEventType Type => BattleEventType.BattleStarted;
}

public partial class BattleEvent
{
    public static class BattleStarted
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BattleStartedEvent Create(int challengerId, int encounterId) => new(challengerId, encounterId);
    }
}
