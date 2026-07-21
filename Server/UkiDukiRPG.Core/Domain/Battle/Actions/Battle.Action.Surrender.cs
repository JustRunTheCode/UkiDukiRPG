using System.Runtime.CompilerServices;

using UkiDukiRPG.Core.Domain.Characters;

namespace UkiDukiRPG.Core.Domain.Battle.Actions;

public record SurrenderAction(
    Guid InitiatedBy
) : IBattleAction
{
    public BattleActionType Type => BattleActionType.Surrender;

    public void Process(Combatant caster, Combatant target)
    {
        // TODO: Invoke Battle Engine
        // Ability.Lookup[(int)AbilityType].Use(caster, target, battleEngine);
        
        throw new NotImplementedException();
    }
}

public partial class BattleAction
{
    public static class Surrender
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SurrenderAction Create(Guid initiatedBy) => new(initiatedBy);
    }
}