namespace UkiDukiRPG.Core.Domain.Leveling;

public class Experience(int maxLevel, ILevelRequirements levelRequirements)
{
    public int MaxLevel { get; } = maxLevel;

    public int Level            { get; private set; } = 0;
    public int UnassignedLevels { get; private set; } = 0;

    public int Xp                 { get; private set; } = 0;
    public int TotalXp            { get; private set; } = 0;
    public int XpRequiredForLevel { get; private set; } = 0;

    private readonly ILevelRequirements m_LevelRequirements = levelRequirements;

    public void AddExperience(int xp)
    {
        if (Level == MaxLevel)
            return;

        TotalXp += xp;

        while (Level < MaxLevel && m_LevelRequirements.TotalXpRequiredForLevel(Level + 1) <= TotalXp)
        {
            ++Level;
            ++UnassignedLevels;
        }

        if (Level == MaxLevel)
        {
            Xp                 = 0;
            XpRequiredForLevel = 0;
        }
        else
        {
            Xp                 = TotalXp - m_LevelRequirements.TotalXpRequiredForLevel(Level);
            XpRequiredForLevel = m_LevelRequirements.XpRequiredForLevel(Level);
        }
    }

    public void AssignLevel() => --UnassignedLevels;
}
