using System.Runtime.CompilerServices;

namespace UkiDukiRPG.Core.Domain.Battle.Actions;

// @formatter:off
public record StartBattleAction(
    int CombatantId
) : IBattleAction
// @formatter:on
{
    public BattleActionType Type => BattleActionType.StartBattle;
}

public partial class BattleAction
{
    public static class StartBattle
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IBattleAction Create(int combatantId) => new StartBattleAction(combatantId);
    }
}