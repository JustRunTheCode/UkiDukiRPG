namespace UkiDukiRPG.Core.Domain.Stats;

public class DamageStat(float value = 0) : Stat(StatType.Damage, value)
{
    public static DamageStat operator +(DamageStat left, DamageStat right) => new(left.Value + right.Value);
}
