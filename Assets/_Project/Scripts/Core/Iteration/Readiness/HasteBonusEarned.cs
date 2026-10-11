namespace Specimen.Core.Iteration.Readiness
{
    /// <summary>Todo el grupo estuvo listo antes de tiempo: se gana Prisa para el loot de este piso.</summary>
    public readonly struct HasteBonusEarned
    {
        public readonly int Floor;
        public readonly float SavedSeconds;
        public readonly float Bonus;

        public HasteBonusEarned(int floor, float savedSeconds, float bonus)
        {
            Floor = floor;
            SavedSeconds = savedSeconds;
            Bonus = bonus;
        }
    }
}
