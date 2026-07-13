using UkiDukiRPG.Core.Domain.Characters;

namespace UkiDukiRPG.Core.Domain.Utilities;

//NOTE: Incomplete
public static class ModifierFunction
{
    public static Func<Character, float> NoEffect => _ => 1.0f;
    
    public static Func<Character, float> DefenseReduction => hero => (1.0f - hero.EffectiveAttributes.Defense.EffectMultiplier());
    
    public static Func<Character, float> AttackAmplification => hero => hero.EffectiveAttributes.Attack.EffectMultiplier();
    
    public static Func<Character, float> MagicAmplification => hero => hero.EffectiveAttributes.Magic.EffectMultiplier();
}
