using UkiDukiRPG.Core.Domain.Abilities;
using UkiDukiRPG.Core.Domain.Attributes;
using UkiDukiRPG.Core.Domain.Leveling;
using UkiDukiRPG.Core.Domain.Stats;

namespace UkiDukiRPG.Core.Domain.Characters;

public abstract class Character(AttributeSet baseAttributes, StatBlock baseStats)
{
    public readonly AttributeSet BaseAttributes      = baseAttributes;
    public readonly AttributeSet UpgradedAttributes  = new();
    public readonly AttributeSet EffectiveAttributes = baseAttributes;

    public readonly StatBlock BaseStats      = baseStats;
    public readonly StatBlock EffectiveStats = baseStats + baseAttributes;

    public readonly Experience Experience = new(268, new LevelRequirements());
    
    //TODO: Keep Track of Abilities
    public readonly Ability[] Abilities = new Ability[4];
}
