namespace Specimen.Core.Progression
{
    /// <summary>Color de dificultad de una criatura según la diferencia de nivel, como el nombre del objetivo en WoW.</summary>
    public enum Difficulty
    {
        Gris,
        Verde,
        Amarillo,
        Naranja,
        Rojo
    }

    public static class LevelDifficulty
    {
        public static Difficulty Of(int playerLevel, int creatureLevel)
        {
            int gap = creatureLevel - playerLevel;
            if (gap >= 5) return Difficulty.Rojo;
            if (gap >= 3) return Difficulty.Naranja;
            if (gap >= -2) return Difficulty.Amarillo;
            return gap >= -5 ? Difficulty.Verde : Difficulty.Gris;
        }
    }
}
