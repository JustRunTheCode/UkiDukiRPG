namespace UkiDukiRPG.Core.Domain.Time;

public enum TimeUnit
{
    Tick,
    Turn,
    Round,
}

public readonly struct TimeInterval(int ticks = 0)
{
    public const int TickTimeMultiplier  = 1;
    public const int TurnTimeMultiplier  = TickTimeMultiplier;
    public const int RoundTimeMultiplier = TurnTimeMultiplier * 2;

    public int Ticks { get; } = ticks;

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
