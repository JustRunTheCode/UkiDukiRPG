using System.Diagnostics.CodeAnalysis;

namespace UkiDukiRPG.Core.Domain.Time;

public enum TimeUnit
{
    Tick,
    Turn,
    Round,
}

[SuppressMessage("ReSharper", "ConvertToAutoPropertyWhenPossible")]
public readonly struct TimeInterval(int ticks = 0)
{
    public const int TickTimeMultiplier  = 1;
    public const int TurnTimeMultiplier  = TickTimeMultiplier;
    public const int RoundTimeMultiplier = TurnTimeMultiplier * 2;

    private readonly int m_Ticks = ticks;

    public int Tick => m_Ticks;
    
    public int Turn => m_Ticks;

    public int Round => (m_Ticks + 1) / 2;
    
    public static TimeInterval FromTurns(int turns) => new(TurnTimeMultiplier * turns);

    public static TimeInterval FromRounds(int rounds) => new(RoundTimeMultiplier * rounds);

    public static TimeInterval FromTicks(int ticks) => new(TickTimeMultiplier * ticks);

    public static TimeInterval From(int interval, TimeUnit timeUnit)
    {
        return timeUnit switch
               {
                   TimeUnit.Tick  => FromTicks(interval),
                   TimeUnit.Turn  => FromTurns(interval),
                   TimeUnit.Round => FromRounds(interval),
                   _              => FromTicks(0)
               };
    }
}
