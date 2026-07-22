using UkiDukiRPG.Core.Domain.Battle;

namespace UkiDukiRPG.Core.Domain.Utilities.Extensions;

public static class CombatantExtensions
{
    extension(Combatant combatant)
    {
        public int HealthLevel  => combatant.Attributes.Health.Level;
        public int AttackLevel  => combatant.Attributes.Attack.Level;
        public int DefenseLevel => combatant.Attributes.Defense.Level;
        public int MagicLevel   => combatant.Attributes.Magic.Level;

        public float MaxHealth     => combatant.Stats.MaxHealth.Value;
        public float MaxMana       => combatant.Stats.MaxMana.Value;
        public float CurrentHealth => combatant.Stats.Health.Value;
        public float CurrentMana   => combatant.Stats.Mana.Value;
    }
}
