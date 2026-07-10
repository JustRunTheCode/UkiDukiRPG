namespace UkiDukiRPG.Core.Domain.Stats;

public class DefenseStat(float value = 0) : Stat(StatType.Defense, value)
{
    public static DefenseStat operator+(DefenseStat left, DefenseStat right) => new(left.Value + right.Value);
    
}
