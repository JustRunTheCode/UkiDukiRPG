using UkiDukiRPG.Core.Domain.Characters;

namespace UkiDukiRPG.Core.Domain.Abilities;

public abstract class Ability(string name)
{
    public string Name { get; } = name;

    public abstract void Use(Character caster, Character target);
}
