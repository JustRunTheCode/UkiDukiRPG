namespace UkiDukiRPG.Core.Domain.Battle.Actions;

public enum BattleActionType
{
    None = 0,

    EndTurn,
    StartBattle,
    Surrender,
    UseAbility,

    Count
}

public interface IBattleAction
{
    public BattleActionType Type { get; }
}