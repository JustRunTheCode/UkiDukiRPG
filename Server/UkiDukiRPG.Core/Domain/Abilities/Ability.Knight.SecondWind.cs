using UkiDukiRPG.Core.Domain.Battle;
using UkiDukiRPG.Core.Domain.Battle.Events;
using UkiDukiRPG.Core.Domain.Effects;
using UkiDukiRPG.Core.Domain.Utilities;

namespace UkiDukiRPG.Core.Domain.Abilities;

//NOTE: Effect 1: RestoreHealthEffect (Target: Caster, Value: Moderate)
public class SecondWindAbility() : Ability(nameof(SecondWindAbility), AbilityType.SecondWind)
{
    private const float c_BaseHeal = 15.0f;

    public override void Use(Combatant caster, Combatant target, IBattleEngine battle)
    {
        battle.AddEvent(BattleEvent.AbilityUsed.Create(caster.Id, target.Id, Type));

        var effect = new RestoreHealthEffect(c_BaseHeal, ModifierFunction.MagicAmplification, ModifierFunction.NoEffect);

        effect.Apply(caster, target, battle);
    }
}
