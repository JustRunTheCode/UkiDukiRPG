using MessagePack;

namespace UkiDukiRPG.Core.Domain.Battle.Events;

public enum BattleEventType
{
    None = 0,

    AbilityUsed,
    BattleEnded,
    BattleStarted,
    CombatantDefeated,
    CombatantHeal,
    CombatantHurt,
    RoundEnded,
    TurnEnded,
    StatusEffectApplied,
    StatusEffectCleared,

    Count
}

[Union((int)BattleEventType.AbilityUsed,         typeof(AbilityUsedEvent))]
[Union((int)BattleEventType.CombatantDefeated,   typeof(CombatantDefeatedEvent))]
[Union((int)BattleEventType.CombatantHeal,       typeof(CombatantHealEvent))]
[Union((int)BattleEventType.CombatantHurt,       typeof(CombatantHurtEvent))]
[Union((int)BattleEventType.RoundEnded,          typeof(RoundEndedEvent))]
[Union((int)BattleEventType.TurnEnded,           typeof(TurnEndedEvent))]
[Union((int)BattleEventType.StatusEffectApplied, typeof(StatusEffectAppliedEvent))]
[Union((int)BattleEventType.StatusEffectCleared, typeof(StatusEffectClearedEvent))]
public interface IBattleEvent
{
    public BattleEventType Type { get; }
}
