using System.Runtime.CompilerServices;

using MessagePack;

namespace UkiDukiRPG.Core.Domain.Battle.Events;

// @formatter:off
[MessagePackObject]
public record CombatantHurtEvent(
    [property: Key(0)] int   CombatantId,
    [property: Key(1)] float DamageTaken
) : IBattleEvent
// @formatter:on
{
    public BattleEventType Type => BattleEventType.CombatantHurt;
}

public partial class BattleEvent
{
    public static class CombatantHurt
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CombatantHurtEvent Create(int combatantId, float damageTaken) => new(combatantId, damageTaken);
    }
}
