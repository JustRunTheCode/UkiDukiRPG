using System.Runtime.CompilerServices;

using MessagePack;

namespace UkiDukiRPG.Core.Domain.Battle.Events;

// @formatter:off
[MessagePackObject]
public record CombatantHealEvent(
    [property: Key(0)] Guid  CombatantId,
    [property: Key(1)] float HealthRestored
) : IBattleEvent
// @formatter:on
{
    public BattleEventType Type => BattleEventType.CombatantHeal;
}

public partial class BattleEvent
{
    public static class CombatantHeal
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CombatantHealEvent Create(Guid combatantId, float healthRestored) => new(combatantId, healthRestored);
    }
}
