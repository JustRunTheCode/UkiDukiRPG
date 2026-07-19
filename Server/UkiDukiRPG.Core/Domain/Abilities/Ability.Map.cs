using System.Runtime.CompilerServices;

namespace UkiDukiRPG.Core.Domain.Abilities;

[InlineArray((int)AbilityType.Count)]
public struct AbilityMap
{
    private bool m_Element0;
}

public partial class Ability
{
    public static AbilityMap EmptyMap => default;

    public static AbilityMap CreateMap(params ReadOnlySpan<AbilityType> abilityTypes)
    {
        var map = EmptyMap;

        foreach (var abilityType in abilityTypes)
            map[(int)abilityType] = true;

        return map;
    }

    public static Ability[] Lookup = new Ability[(int)AbilityType.Count];

    static Ability()
    {
        Lookup[(int)AbilityType.ClawSwipe]    = new ClawSwipeAbility();
        Lookup[(int)AbilityType.DragonScales] = new DragonScalesAbility();
        Lookup[(int)AbilityType.FlameBreath]  = new FlameBreathAbility();
        Lookup[(int)AbilityType.Intimidate]   = new IntimidateAbility();
        
        Lookup[(int)AbilityType.Bite]         = new BiteAbility();
        Lookup[(int)AbilityType.Pounce]       = new PounceAbility();
        Lookup[(int)AbilityType.Skitter]      = new SkitterAbility();
        Lookup[(int)AbilityType.WebThrow]     = new WebThrowAbility();
        
        Lookup[(int)AbilityType.ArcaneSurge]  = new ArcaneSurgeAbility();
        Lookup[(int)AbilityType.Firebolt]     = new FireboltAbility();
        Lookup[(int)AbilityType.HexShield]    = new HexShieldAbility();
        Lookup[(int)AbilityType.ManaDrain]    = new ManaDrainAbility();
        
        Lookup[(int)AbilityType.DirtyKick]    = new DirtyKickAbility();
        Lookup[(int)AbilityType.Frenzy]       = new FrenzyAbility();
        Lookup[(int)AbilityType.Headbutt]     = new HeadbuttAbility();
        Lookup[(int)AbilityType.RustyBlade]   = new RustyBladeAbility();
        
        Lookup[(int)AbilityType.BattleCry]    = new BattleCryAbility();
        Lookup[(int)AbilityType.SecondWind]   = new SecondWindAbility();
        Lookup[(int)AbilityType.ShieldUp]     = new ShieldUpAbility();
        Lookup[(int)AbilityType.Slash]        = new SlashAbility();
        
        Lookup[(int)AbilityType.Curse]        = new CurseAbility();
        Lookup[(int)AbilityType.DarkPact]     = new DarkPactAbility();
        Lookup[(int)AbilityType.DrainLife]    = new DrainLifeAbility();
        Lookup[(int)AbilityType.ShadowBolt]   = new ShadowBoltAbility();
    }
}
