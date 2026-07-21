using UkiDukiRPG.Core.Domain.Characters;

namespace UkiDukiRPG.Core.Domain.Battle.Actions;

public enum BattleActionType
{
    None = 0,
    
    Surrender,
    UseAbility,
    
    Count
}

public interface IBattleAction
{
    public BattleActionType Type { get; }
    
    public void Process(Combatant caster, Combatant target); //NOTE: Add Battle Engine as 3rd parameter
}