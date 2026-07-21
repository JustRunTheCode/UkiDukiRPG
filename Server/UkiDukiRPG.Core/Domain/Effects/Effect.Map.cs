using System.Runtime.CompilerServices;

namespace UkiDukiRPG.Core.Domain.Effects;

[InlineArray((int)StatusEffectType.Count)]
public struct StatusEffectMap
{
    private int m_Element0;
}

public partial class StatusEffect
{
    public static StatusEffectMap EmptyMap => default;

    public static StatusEffectMap CreateMap(params ReadOnlySpan<StatusEffectType> abilityTypes)
    {
        var map = EmptyMap;

        foreach (var abilityType in abilityTypes)
            map[(int)abilityType] = 1;

        return map;
    }
}
