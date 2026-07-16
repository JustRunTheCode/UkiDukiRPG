using System.Runtime.CompilerServices;

using UkiDukiRPG.Core.Domain.Attributes;
using UkiDukiRPG.Core.Domain.Stats;

namespace UkiDukiRPG.Core.Domain.Characters;

public class Combatant(Character character)
{
    public AttributeSet Attributes { get; } = character.EffectiveAttributes;
    public CombatStats  Stats      { get; } = character.EffectiveStats;

    //TODO: Keep Track of Active Status Effects
    //TODO: Add Abilities
    
    public void AscendAttribute(AttributeType attributeType, int value)
    {
        unsafe
        {
            s_AscendAttributeLookupTable[(int)attributeType](this, value);
        }
    }

    public void DescendAttribute(AttributeType attributeType, int value)
    {
        unsafe
        {
            s_DescendAttributeLookupTable[(int)attributeType](this, value);
        }
    }

    public void IncreaseStat(CombatStatType statType, float value)
    {
        unsafe
        {
            s_IncreaseCombatStatLookupTable[(int)statType](this, value);
        }
    }

    public void DecreaseStat(CombatStatType statType, float value)
    {
        unsafe
        {
            s_DecreaseCombatStatLookupTable[(int)statType](this, value);
        }
    }

    private static readonly unsafe delegate* managed<Combatant, int, void>[] s_AscendAttributeLookupTable;
    private static readonly unsafe delegate* managed<Combatant, int, void>[] s_DescendAttributeLookupTable;

    private static readonly unsafe delegate* managed<Combatant, float, void>[] s_IncreaseCombatStatLookupTable;
    private static readonly unsafe delegate* managed<Combatant, float, void>[] s_DecreaseCombatStatLookupTable;

    static Combatant()
    {
        unsafe
        {
            s_AscendAttributeLookupTable    = new delegate*<Combatant, int, void>[(int)AttributeType.Count];
            s_DescendAttributeLookupTable   = new delegate*<Combatant, int, void>[(int)AttributeType.Count];
            s_IncreaseCombatStatLookupTable = new delegate*<Combatant, float, void>[(int)CombatStatType.Count];
            s_DecreaseCombatStatLookupTable = new delegate*<Combatant, float, void>[(int)CombatStatType.Count];

            s_AscendAttributeLookupTable[(int)AttributeType.Health]  = &AscendAttributeHealth;
            s_AscendAttributeLookupTable[(int)AttributeType.Attack]  = &AscendAttributeAttack;
            s_AscendAttributeLookupTable[(int)AttributeType.Defense] = &AscendAttributeDefense;
            s_AscendAttributeLookupTable[(int)AttributeType.Magic]   = &AscendAttributeMagic;

            s_DescendAttributeLookupTable[(int)AttributeType.Health]  = &DescendAttributeHealth;
            s_DescendAttributeLookupTable[(int)AttributeType.Attack]  = &DescendAttributeAttack;
            s_DescendAttributeLookupTable[(int)AttributeType.Defense] = &DescendAttributeDefense;
            s_DescendAttributeLookupTable[(int)AttributeType.Magic]   = &DescendAttributeMagic;

            s_IncreaseCombatStatLookupTable[(int)CombatStatType.MaxHealth] = &IncreaseStatMaxHealth;
            s_IncreaseCombatStatLookupTable[(int)CombatStatType.MaxMana]   = &IncreaseStatMaxMana;
            s_IncreaseCombatStatLookupTable[(int)CombatStatType.Health]    = &IncreaseStatHealth;
            s_IncreaseCombatStatLookupTable[(int)CombatStatType.Damage]    = &IncreaseStatDamage;
            s_IncreaseCombatStatLookupTable[(int)CombatStatType.Defense]   = &IncreaseStatDefense;
            s_IncreaseCombatStatLookupTable[(int)CombatStatType.Mana]      = &IncreaseStatMana;

            s_DecreaseCombatStatLookupTable[(int)CombatStatType.MaxHealth] = &DecreaseStatMaxHealth;
            s_DecreaseCombatStatLookupTable[(int)CombatStatType.MaxMana]   = &DecreaseStatMaxMana;
            s_DecreaseCombatStatLookupTable[(int)CombatStatType.Health]    = &DecreaseStatHealth;
            s_DecreaseCombatStatLookupTable[(int)CombatStatType.Damage]    = &DecreaseStatDamage;
            s_DecreaseCombatStatLookupTable[(int)CombatStatType.Defense]   = &DecreaseStatDefense;
            s_DecreaseCombatStatLookupTable[(int)CombatStatType.Mana]      = &DecreaseStatMana;

            for (var index = 1; index < s_AscendAttributeLookupTable.Length; index++)
                if (s_AscendAttributeLookupTable[index] == null || s_DescendAttributeLookupTable[index] == null)
                    throw new TypeInitializationException(typeof(Combatant).FullName, new NotImplementedException($"Attribute '{(AttributeType)index}' has no function pointer method assigned"));

            for (var index = 1; index < s_IncreaseCombatStatLookupTable.Length; index++)
                if (s_IncreaseCombatStatLookupTable[index] == null || s_DecreaseCombatStatLookupTable[index] == null)
                    throw new TypeInitializationException(typeof(Combatant).FullName, new NotImplementedException($"Stat '{(CombatStatType)index}' has no function pointer method assigned"));
        }
    }

    #region Stats & Attributes | Ascend & Descend Implementations

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void AscendAttributeHealth(Combatant combatant, int value)
    {
        //TODO: Make a Utility function, same increase as Health Attribute is going to have | If function is non-linear, include StartLevel argument 
        IncreaseStatMaxHealth(combatant, value * 16.75f);

        combatant.Attributes.Health.Ascend(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void AscendAttributeAttack(Combatant combatant, int value)
    {
        //TODO: Make a Utility function, same increase as Attack Attribute is going to have | If function is non-linear, include StartLevel argument 
        IncreaseStatDamage(combatant, value * 0.067f);

        combatant.Attributes.Attack.Ascend(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void AscendAttributeDefense(Combatant combatant, int value)
    {
        //TODO: Make a Utility function, same increase as Defense Attribute is going to have | If function is non-linear, include StartLevel argument 
        IncreaseStatDefense(combatant, value * 0.01f);

        combatant.Attributes.Defense.Ascend(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void AscendAttributeMagic(Combatant combatant, int value)
    {
        //TODO: Make a Utility function, same increase as Magic Attribute is going to have | If function is non-linear, include StartLevel argument 
        IncreaseStatMana(combatant, value * 0.067f);

        combatant.Attributes.Magic.Ascend(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void DescendAttributeHealth(Combatant combatant, int value)
    {
        //TODO: Make a Utility function, same increase as Health Attribute is going to have | If function is non-linear, include StartLevel argument 
        DecreaseStatMaxHealth(combatant, value * 16.75f);

        combatant.Attributes.Health.Descend(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void DescendAttributeAttack(Combatant combatant, int value)
    {
        //TODO: Make a Utility function, same increase as Attack Attribute is going to have | If function is non-linear, include StartLevel argument 
        DecreaseStatDamage(combatant, value * 0.067f);

        combatant.Attributes.Attack.Descend(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void DescendAttributeDefense(Combatant combatant, int value)
    {
        //TODO: Make a Utility function, same increase as Defense Attribute is going to have | If function is non-linear, include StartLevel argument 
        DecreaseStatDefense(combatant, value * 0.01f);

        combatant.Attributes.Defense.Descend(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void DescendAttributeMagic(Combatant combatant, int value)
    {
        //TODO: Make a Utility function, same increase as Magic Attribute is going to have | If function is non-linear, include StartLevel argument 
        DecreaseStatMana(combatant, value * 0.067f);

        combatant.Attributes.Magic.Descend(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void IncreaseStatMaxHealth(Combatant combatant, float value) => combatant.Stats.MaxHealth.Value += value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void IncreaseStatMaxMana(Combatant combatant, float value) => combatant.Stats.MaxMana.Value += value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void IncreaseStatHealth(Combatant combatant, float value) => combatant.Stats.Health.Value += value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void IncreaseStatDamage(Combatant combatant, float value) => combatant.Stats.Damage.Value += value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void IncreaseStatDefense(Combatant combatant, float value) => combatant.Stats.Defense.Value += value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void IncreaseStatMana(Combatant combatant, float value) => combatant.Stats.Mana.Value += value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void DecreaseStatMaxHealth(Combatant combatant, float value) => combatant.Stats.MaxHealth.Value -= value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void DecreaseStatMaxMana(Combatant combatant, float value) => combatant.Stats.MaxMana.Value -= value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void DecreaseStatHealth(Combatant combatant, float value) => combatant.Stats.Health.Value -= value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void DecreaseStatDamage(Combatant combatant, float value) => combatant.Stats.Damage.Value -= value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void DecreaseStatDefense(Combatant combatant, float value) => combatant.Stats.Defense.Value -= value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void DecreaseStatMana(Combatant combatant, float value) => combatant.Stats.Mana.Value -= value;

    #endregion
}
