using System.Runtime.CompilerServices;

using MessagePack;

namespace UkiDukiRPG.Core.Domain.Battle.Events;

// @formatter:off
[MessagePackObject]
public record CombatantDefeatedEvent(
    [property: Key(0)] int DefeatedId
) : IBattleEvent
// @formatter:on
{
    public BattleEventType Type => BattleEventType.CombatantDefeated;
}

public partial class BattleEvent
{
    public static class CombatantDefeated
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CombatantDefeatedEvent Create(int defeatedId) => new(defeatedId);
    }
}
