using System.Runtime.CompilerServices;

using UkiDukiRPG.Core.Domain.Abilities;

namespace UkiDukiRPG.Core.Domain.Battle.Actions;

// @formatter:off
public record UseAbilityAction(
    int         CasterId,
    int         TargetId,
    AbilityType AbilityType
) : IBattleAction
// @formatter:on
{
    public BattleActionType Type => BattleActionType.UseAbility;
}

public partial class BattleAction
{
    public static class UseAbility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IBattleAction Create(int casterId, int targetId, AbilityType abilityType) => new UseAbilityAction(casterId, targetId, abilityType);
    }
}
