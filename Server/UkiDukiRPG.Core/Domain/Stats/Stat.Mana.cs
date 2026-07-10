namespace UkiDukiRPG.Core.Domain.Stats;

public class ManaStat(float value = 0) : Stat(StatType.Mana, value)
{
    public static ManaStat operator+(ManaStat left, ManaStat right) => new(left.Value + right.Value);
}
