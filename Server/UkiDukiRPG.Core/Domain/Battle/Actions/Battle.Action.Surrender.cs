using System.Runtime.CompilerServices;

namespace UkiDukiRPG.Core.Domain.Battle.Actions;

// @formatter:off
public record SurrenderAction(
    int           InitiatedBy,
    CombatantType CombatantType
) : IBattleAction
// @formatter:on
{
    public BattleActionType Type => BattleActionType.Surrender;
}

public partial class BattleAction
{
    public static class Surrender
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IBattleAction Create(int initiatedBy, CombatantType combatantType) => new SurrenderAction(initiatedBy, combatantType);
    }
}