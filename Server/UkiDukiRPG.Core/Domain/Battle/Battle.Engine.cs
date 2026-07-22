using UkiDukiRPG.Core.Domain.Battle.Actions;
using UkiDukiRPG.Core.Domain.Battle.Events;
using UkiDukiRPG.Core.Domain.Time;

namespace UkiDukiRPG.Core.Domain.Battle;

public interface IBattleEngine
{
    public ITimeSystem   TimeSystem { get; }
    public bool          HasStarted { get; }
    public bool          HasEnded   { get; }
    public CombatantType Winner     { get; }

    public BattleActionResult ProcessAction(IBattleAction action);

    public void AddEvent(IBattleEvent @event);
}
