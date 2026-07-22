using UkiDukiRPG.Core.Domain.Battle.Events;
using UkiDukiRPG.Core.Domain.Time;

namespace UkiDukiRPG.Core.Domain.Battle;

public enum BattleType
{
    None = 0,

    EvE,
    PvE,
    PvP,

    Count
}

public enum CombatantType
{
    None = 0,

    Challenger,
    Encounter,

    Count
}

//TODO: Organize
public class BattleState(Combatant combatantLeft, Combatant combatantRight, TimeInterval duration, bool isOver)
{
    public Combatant    CombatantLeft  { get; } = combatantLeft;
    public Combatant    CombatantRight { get; } = combatantRight;
    public TimeInterval Duration       { get; } = duration;
    public bool         IsOver         { get; } = isOver;
}

//TODO: Organize
public class BattleActionResult(BattleState state, List<IBattleEvent> events)
{
    public BattleState        State  { get; } = state;
    public List<IBattleEvent> Events { get; } = events;
}
