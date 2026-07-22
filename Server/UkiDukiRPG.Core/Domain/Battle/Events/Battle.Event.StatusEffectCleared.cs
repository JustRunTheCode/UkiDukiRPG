using System.Runtime.CompilerServices;

using MessagePack;

using UkiDukiRPG.Core.Domain.Effects;

namespace UkiDukiRPG.Core.Domain.Battle.Events;

// @formatter:off
[MessagePackObject]
public record StatusEffectClearedEvent(
    [property: Key(0)] int              CombatantId,
    [property: Key(1)] StatusEffectType StatusEffect
) : IBattleEvent
// @formatter:on
{
    public BattleEventType Type => BattleEventType.StatusEffectCleared;
}

public partial class BattleEvent
{
    public static class StatusEffectCleared
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static StatusEffectClearedEvent Create(int combatantId, StatusEffectType statusEffect) => new(combatantId, statusEffect);
    }
}
