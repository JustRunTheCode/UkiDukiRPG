using System.Runtime.CompilerServices;

namespace UkiDukiRPG.Core.Domain.Battle.Actions;

// @formatter:off
public record EndTurnAction(
    int CombatantId
) : IBattleAction
// @formatter:on
{
    public BattleActionType Type => BattleActionType.EndTurn;
}

public partial class BattleAction
{
    public static class EndTurn
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IBattleAction Create(int combatantId) => new EndTurnAction(combatantId);
    }
}