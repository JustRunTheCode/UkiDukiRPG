using System.Runtime.CompilerServices;

using UkiDukiRPG.Core.Domain.Abilities;
using UkiDukiRPG.Core.Domain.Attributes;
using UkiDukiRPG.Core.Domain.Leveling;
using UkiDukiRPG.Core.Domain.Stats;

namespace UkiDukiRPG.Core.Domain.Characters;

public abstract class Character(AttributeSet baseAttributes, StatBlock baseStats, ReadOnlySpan<AbilityType> equippedAbilities, ReadOnlySpan<AbilityType> learnedAbilities)
{
    public readonly AttributeSet BaseAttributes      = baseAttributes;
    public readonly AttributeSet UpgradedAttributes  = new();
    public readonly AttributeSet EffectiveAttributes = baseAttributes;

    public readonly StatBlock BaseStats      = baseStats;
    public readonly StatBlock EffectiveStats = baseStats + baseAttributes;

    public readonly Experience Experience = new(268, new LevelRequirements());

    private AbilityMap m_EquippedAbilitiesMap = Ability.CreateMap(equippedAbilities);
    private AbilityMap m_LearnedAbilitiesMap  = Ability.CreateMap(learnedAbilities);

    public AbilityMap EquippedAbilitiesMap => m_EquippedAbilitiesMap;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void LearnAbility(AbilityType ability) => m_LearnedAbilitiesMap[(int)ability] = true;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsAbilityLearned(AbilityType ability) => m_LearnedAbilitiesMap[(int)ability];

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsAbilityEquipped(AbilityType ability) => m_EquippedAbilitiesMap[(int)ability];

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void EquipAbility(AbilityType ability, AbilityType abilityToReplace)
    {
        if (!IsAbilityEquipped(abilityToReplace))
            return;

        m_EquippedAbilitiesMap[(int)abilityToReplace] = false;
        m_EquippedAbilitiesMap[(int)ability]          = true;
    }
}
