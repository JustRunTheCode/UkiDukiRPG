namespace UkiDukiRPG.Core.Domain.Stats;

public class HealthStat(float value = 0) : Stat(StatType.Health, value)
{
    public static HealthStat operator +(HealthStat left, HealthStat right) => new(left.Value + right.Value);
}
