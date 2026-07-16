using UkiDukiRPG.Core.Domain.Characters;

namespace UkiDukiRPG.Core.Domain.Utilities;

//NOTE: Incomplete
public static class ModifierFunction
{
    public static Func<Combatant, float> NoEffect => _ => 1.0f;

    public static Func<Combatant, float> DefenseReduction => hero => (1.0f - hero.Attributes.Defense.EffectMultiplier());

    public static Func<Combatant, float> AttackAmplification => hero => hero.Attributes.Attack.EffectMultiplier();

    public static Func<Combatant, float> MagicAmplification => hero => hero.Attributes.Magic.EffectMultiplier();
}
