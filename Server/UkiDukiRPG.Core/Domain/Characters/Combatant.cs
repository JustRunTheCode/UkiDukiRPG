using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

using JustRunTheCode.Optimization.Attributes;

using UkiDukiRPG.Core.Domain.Abilities;
using UkiDukiRPG.Core.Domain.Attributes;
using UkiDukiRPG.Core.Domain.Effects;
using UkiDukiRPG.Core.Domain.Stats;
using UkiDukiRPG.Core.Domain.Time;

namespace UkiDukiRPG.Core.Domain.Characters;

public partial class Combatant(Character character)
{
    public AttributeSet Attributes { get; } = character.EffectiveAttributes;
    public CombatStats  Stats      { get; } = character.EffectiveStats;

    private readonly AbilityMap      m_EquippedAbilityMap    = character.EquippedAbilitiesMap;
    private          StatusEffectMap m_ActiveStatusEffectMap = StatusEffect.EmptyMap;

    [SuppressMessage("ReSharper", "RedundantBoolCompare")]
    public void UseAbility(AbilityType abilityType, Combatant target)
    {
        if (m_EquippedAbilityMap[(int)abilityType] is false)
            return;

        // @formatter:off
        //NOTE: TypeSystem is a placeholder
        Ability.Lookup[(int)abilityType].Use(this, target, new TimeSystem());
        // @formatter:on
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AddStatusEffect(StatusEffectType statusEffectType) => m_ActiveStatusEffectMap[(int)statusEffectType] += 1;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void RemoveStatusEffect(StatusEffectType statusEffectType) => m_ActiveStatusEffectMap[(int)statusEffectType] -= 1;

    public StatusEffectMap ActiveStatusEffectMap => m_ActiveStatusEffectMap;

    [LookupTable<AttributeType>(AttributeType.Count, [AttributeType.None, AttributeType.Count])]
    public void AscendAttribute(AttributeType attributeType, int value)
    {
        AscendAttributeLookup(attributeType, value);
    }

    [LookupTable<AttributeType>(AttributeType.Count, [AttributeType.None, AttributeType.Count])]
    public void DescendAttribute(AttributeType attributeType, int value)
    {
        DescendAttributeLookup(attributeType, value);
    }

    [LookupTable<CombatStatType>(CombatStatType.Count, [CombatStatType.None, CombatStatType.Count])]
    public void IncreaseStat(CombatStatType statType, float value)
    {
        IncreaseStatLookup(statType, value);
    }

    [LookupTable<CombatStatType>(CombatStatType.Count, [CombatStatType.None, CombatStatType.Count])]
    public void DecreaseStat(CombatStatType statType, float value)
    {
        DecreaseStatLookup(statType, value);
    }

    #region Stats & Attributes | Ascend & Descend Implementations

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [LookupTableReference<AttributeType>(nameof(AscendAttribute), AttributeType.Health)]
    private static void AscendAttributeHealth(Combatant combatant, int value)
    {
        //TODO: Make a Utility function, same increase as Health Attribute is going to have | If function is non-linear, include StartLevel argument 
        IncreaseStatMaxHealth(combatant, value * 16.75f);

        combatant.Attributes.Health.Ascend(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [LookupTableReference<AttributeType>(nameof(AscendAttribute), AttributeType.Attack)]
    private static void AscendAttributeAttack(Combatant combatant, int value)
    {
        //TODO: Make a Utility function, same increase as Attack Attribute is going to have | If function is non-linear, include StartLevel argument 
        IncreaseStatDamage(combatant, value * 0.067f);

        combatant.Attributes.Attack.Ascend(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [LookupTableReference<AttributeType>(nameof(AscendAttribute), AttributeType.Defense)]
    private static void AscendAttributeDefense(Combatant combatant, int value)
    {
        //TODO: Make a Utility function, same increase as Defense Attribute is going to have | If function is non-linear, include StartLevel argument 
        IncreaseStatDefense(combatant, value * 0.01f);

        combatant.Attributes.Defense.Ascend(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [LookupTableReference<AttributeType>(nameof(AscendAttribute), AttributeType.Magic)]
    private static void AscendAttributeMagic(Combatant combatant, int value)
    {
        //TODO: Make a Utility function, same increase as Magic Attribute is going to have | If function is non-linear, include StartLevel argument 
        IncreaseStatMana(combatant, value * 0.067f);

        combatant.Attributes.Magic.Ascend(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [LookupTableReference<AttributeType>(nameof(DescendAttribute), AttributeType.Health)]
    private static void DescendAttributeHealth(Combatant combatant, int value)
    {
        //TODO: Make a Utility function, same increase as Health Attribute is going to have | If function is non-linear, include StartLevel argument 
        DecreaseStatMaxHealth(combatant, value * 16.75f);

        combatant.Attributes.Health.Descend(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [LookupTableReference<AttributeType>(nameof(DescendAttribute), AttributeType.Attack)]
    private static void DescendAttributeAttack(Combatant combatant, int value)
    {
        //TODO: Make a Utility function, same increase as Attack Attribute is going to have | If function is non-linear, include StartLevel argument 
        DecreaseStatDamage(combatant, value * 0.067f);

        combatant.Attributes.Attack.Descend(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [LookupTableReference<AttributeType>(nameof(DescendAttribute), AttributeType.Defense)]
    private static void DescendAttributeDefense(Combatant combatant, int value)
    {
        //TODO: Make a Utility function, same increase as Defense Attribute is going to have | If function is non-linear, include StartLevel argument 
        DecreaseStatDefense(combatant, value * 0.01f);

        combatant.Attributes.Defense.Descend(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [LookupTableReference<AttributeType>(nameof(DescendAttribute), AttributeType.Magic)]
    private static void DescendAttributeMagic(Combatant combatant, int value)
    {
        //TODO: Make a Utility function, same increase as Magic Attribute is going to have | If function is non-linear, include StartLevel argument 
        DecreaseStatMana(combatant, value * 0.067f);

        combatant.Attributes.Magic.Descend(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [LookupTableReference<CombatStatType>(nameof(IncreaseStat), CombatStatType.MaxHealth)]
    private static void IncreaseStatMaxHealth(Combatant combatant, float value) => combatant.Stats.MaxHealth.Value += value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [LookupTableReference<CombatStatType>(nameof(IncreaseStat), CombatStatType.MaxMana)]
    private static void IncreaseStatMaxMana(Combatant combatant, float value) => combatant.Stats.MaxMana.Value += value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [LookupTableReference<CombatStatType>(nameof(IncreaseStat), CombatStatType.Health)]
    private static void IncreaseStatHealth(Combatant combatant, float value) => combatant.Stats.Health.Value += value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [LookupTableReference<CombatStatType>(nameof(IncreaseStat), CombatStatType.Damage)]
    private static void IncreaseStatDamage(Combatant combatant, float value) => combatant.Stats.Damage.Value += value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [LookupTableReference<CombatStatType>(nameof(IncreaseStat), CombatStatType.Defense)]
    private static void IncreaseStatDefense(Combatant combatant, float value) => combatant.Stats.Defense.Value += value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [LookupTableReference<CombatStatType>(nameof(IncreaseStat), CombatStatType.Mana)]
    private static void IncreaseStatMana(Combatant combatant, float value) => combatant.Stats.Mana.Value += value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [LookupTableReference<CombatStatType>(nameof(DecreaseStat), CombatStatType.MaxHealth)]
    private static void DecreaseStatMaxHealth(Combatant combatant, float value) => combatant.Stats.MaxHealth.Value -= value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [LookupTableReference<CombatStatType>(nameof(DecreaseStat), CombatStatType.MaxMana)]
    private static void DecreaseStatMaxMana(Combatant combatant, float value) => combatant.Stats.MaxMana.Value -= value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [LookupTableReference<CombatStatType>(nameof(DecreaseStat), CombatStatType.Health)]
    private static void DecreaseStatHealth(Combatant combatant, float value) => combatant.Stats.Health.Value -= value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [LookupTableReference<CombatStatType>(nameof(DecreaseStat), CombatStatType.Damage)]
    private static void DecreaseStatDamage(Combatant combatant, float value) => combatant.Stats.Damage.Value -= value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [LookupTableReference<CombatStatType>(nameof(DecreaseStat), CombatStatType.Defense)]
    private static void DecreaseStatDefense(Combatant combatant, float value) => combatant.Stats.Defense.Value -= value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [LookupTableReference<CombatStatType>(nameof(DecreaseStat), CombatStatType.Mana)]
    private static void DecreaseStatMana(Combatant combatant, float value) => combatant.Stats.Mana.Value -= value;

    #endregion
}
