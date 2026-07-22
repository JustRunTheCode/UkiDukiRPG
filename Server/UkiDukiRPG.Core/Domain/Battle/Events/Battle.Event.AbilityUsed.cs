using System.Runtime.CompilerServices;

using MessagePack;

using UkiDukiRPG.Core.Domain.Abilities;

namespace UkiDukiRPG.Core.Domain.Battle.Events;

// @formatter:off
[MessagePackObject]
public record AbilityUsedEvent(
    [property: Key(0)] int         CasterId,
    [property: Key(1)] int         TargetId,
    [property: Key(2)] AbilityType AbilityType
) : IBattleEvent
// @formatter:on
{
    public BattleEventType Type => BattleEventType.AbilityUsed;
}

public partial class BattleEvent
{
    public static class AbilityUsed
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AbilityUsedEvent Create(int casterId, int targetId, AbilityType abilityType) => new(casterId, targetId, abilityType);
    }
}
