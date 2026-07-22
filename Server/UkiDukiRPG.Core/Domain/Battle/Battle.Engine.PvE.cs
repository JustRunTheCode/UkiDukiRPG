using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using JustRunTheCode.Optimization.Attributes;

using UkiDukiRPG.Core.Domain.Battle.Actions;
using UkiDukiRPG.Core.Domain.Battle.Events;
using UkiDukiRPG.Core.Domain.Characters;
using UkiDukiRPG.Core.Domain.Time;

namespace UkiDukiRPG.Core.Domain.Battle;

internal partial class PvEBattleEngine(Character challenger, Character encounter) : IBattleEngine
{
    public ITimeSystem   TimeSystem { get; }              = new TimeSystem();
    public bool          HasStarted { get; private set; } = false;
    public bool          HasEnded   { get; private set; } = false;
    public CombatantType Winner     { get; private set; } = CombatantType.None;

    private readonly Combatant          m_Challenger = Combatant.Create((int)CombatantType.Challenger, challenger);
    private readonly Combatant          m_Encounter  = Combatant.Create((int)CombatantType.Encounter,  encounter);
    private readonly Combatant[]        m_Combatants = new Combatant[(int)CombatantType.Count];
    private readonly List<IBattleEvent> m_BattleLog  = [];

    private Combatant m_Caster            = null!;
    private Combatant m_Opponent          = null!;
    private int       m_LastReportedEvent = 0;
    private bool      m_IsAbilityUsed     = false;

    public BattleActionResult ProcessAction(IBattleAction action)
    {
        ProcessAction(action.Type, action);

        return new BattleActionResult(CreateBattleState(), UnreportedEvents());
    }

    private BattleState CreateBattleState() => new(m_Challenger, m_Encounter, TimeSystem.CurrentTime, HasEnded);

    private List<IBattleEvent> UnreportedEvents()
    {
        var unreportedEvents = CollectionsMarshal.AsSpan(m_BattleLog)[m_LastReportedEvent..];

        m_LastReportedEvent = m_BattleLog.Count;

        return [..unreportedEvents];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [LookupTable<BattleActionType>(BattleActionType.Count, [BattleActionType.None, BattleActionType.Count])]
    private void ProcessAction(BattleActionType type, IBattleAction action) => ProcessActionLookup(type, action);

    [LookupTableReference<BattleActionType>(nameof(ProcessAction), BattleActionType.StartBattle)]
    private static void ProcessStartBattleAction(PvEBattleEngine battle, IBattleAction battleAction)
    {
        if (battle.HasStarted)
            return; //NOTE: Should not be allowed

        battle.AddEvent(BattleEvent.BattleStarted.Create(battle.m_Challenger.Id, battle.m_Encounter.Id));

        battle.TimeSystem.AdvanceTick();
        
        battle.m_Caster   = battle.m_Challenger;
        battle.m_Opponent = battle.m_Encounter;

        battle.m_Combatants[(int)CombatantType.Challenger] = battle.m_Challenger;
        battle.m_Combatants[(int)CombatantType.Encounter]  = battle.m_Encounter;

        battle.HasStarted = true;
    }

    [LookupTableReference<BattleActionType>(nameof(ProcessAction), BattleActionType.UseAbility)]
    private static void ProcessUseAbilityAction(PvEBattleEngine battle, IBattleAction battleAction)
    {
        if (!battle.HasStarted || battle.HasEnded || battle.m_IsAbilityUsed)
            return; //NOTE: Should not be allowed

        var action = Unsafe.As<UseAbilityAction>(battleAction);

        if (action.CasterId != battle.m_Caster.Id)
            return; //NOTE: Should not be allowed
        
        // @formatter:off
        battle.m_Caster.UseAbility(action.AbilityType, battle.m_Combatants[action.TargetId], battle); 
        // @formatter:on

        if (battle.m_Caster.IsDead)
            battle.Winner = (CombatantType)battle.m_Opponent.Id;
        else if (battle.m_Opponent.IsDead)
            battle.Winner = (CombatantType)battle.m_Caster.Id;

        battle.HasEnded = battle.Winner != CombatantType.None;

        if (battle.HasEnded)
        {
            battle.AddEvent(BattleEvent.CombatantDefeated.Create(((int)battle.Winner & 0x1) + 1));
            battle.AddEvent(BattleEvent.BattleEnded.Create(battle.Winner));
        }
        
        battle.m_IsAbilityUsed = true;
    }

    [LookupTableReference<BattleActionType>(nameof(ProcessAction), BattleActionType.EndTurn)]
    private static void ProcessEndTurnAction(PvEBattleEngine battle, IBattleAction battleAction)
    {
        if (!battle.HasStarted || battle.HasEnded)
            return; //NOTE: Should not be allowed

        var action = Unsafe.As<EndTurnAction>(battleAction);
        
        if (battle.m_Caster.Id != action.CombatantId)
            return; //NOTE: Should not be allowed
        
        battle.AddEvent(BattleEvent.TurnEnded.Create(battle.m_Caster.Id, battle.TimeSystem.CurrentTime));
        
        if (battle.TimeSystem.CurrentTick % 2 == 0) 
            battle.AddEvent(BattleEvent.TurnEnded.Create(battle.m_Caster.Id, battle.TimeSystem.CurrentTime));

        battle.TimeSystem.AdvanceTick();

        (battle.m_Caster, battle.m_Opponent) = (battle.m_Opponent, battle.m_Caster);

        battle.m_IsAbilityUsed = false;
    }

    [LookupTableReference<BattleActionType>(nameof(ProcessAction), BattleActionType.Surrender)]
    private static void ProcessSurrenderAction(PvEBattleEngine battle, IBattleAction battleAction)
    {
        if (!battle.HasStarted || battle.HasEnded)
            return; //NOTE: Should not be allowed

        var action = Unsafe.As<SurrenderAction>(battleAction);

        if (action.InitiatedBy != battle.m_Challenger.Id || action.CombatantType != CombatantType.Challenger)
            return; //NOTE: Should not be allowed

        battle.AddEvent(BattleEvent.BattleEnded.Create(CombatantType.Encounter));

        battle.Winner   = CombatantType.Encounter;
        battle.HasEnded = true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AddEvent(IBattleEvent @event) => m_BattleLog.Add(@event);
}

public partial class BattleEngine
{
    public static class PvE
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IBattleEngine Create(Character challenger, Character encounter) => new PvEBattleEngine(challenger, encounter);
    }
}
