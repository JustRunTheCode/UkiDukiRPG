using System.Runtime.CompilerServices;

using UkiDukiRPG.Core.Domain.Abilities;
using UkiDukiRPG.Core.Domain.Characters;

namespace UkiDukiRPG.Core.Domain.Battle.Actions;

public record UseAbilityAction(
    AbilityType AbilityType
) : IBattleAction
{
    public BattleActionType Type => BattleActionType.UseAbility;
    
    public void Process(Combatant caster, Combatant target)
    {
        // TODO: Invoke Battle Engine
        // Ability.Lookup[(int)AbilityType].Use(caster, target, battleEngine);
        
        throw new NotImplementedException();
    }
}

public partial class BattleAction
{
    public static class UseAbility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UseAbilityAction Create(AbilityType abilityType) => new(abilityType);
    }
}
