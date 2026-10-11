namespace Specimen.Core.Iteration
{
    /// <summary>
    /// Constructor fluido con los valores por defecto del GDD (§3.1): Acto de 5 pisos,
    /// Calma 3 min, Incursión 7 min, avisos a 60 s y 15 s, asedio de 5 oleadas, 4 jugadores.
    /// </summary>
    public sealed class IterationConfigBuilder
    {
        private int _floors = 5;
        private float _calm = 180f;
        private float _incursion = 420f;
        private int _waves = 5;
        private int _players = IterationConfig.MaxPlayersLimit;
        private float[] _warnings = { 60f, 15f };

        public static IterationConfig Default => new IterationConfigBuilder().Build();

        public IterationConfigBuilder WithFloors(int count) { _floors = count; return this; }
        public IterationConfigBuilder WithCalm(float seconds) { _calm = seconds; return this; }
        public IterationConfigBuilder WithIncursion(float seconds) { _incursion = seconds; return this; }
        public IterationConfigBuilder WithSiegeWaves(int waves) { _waves = waves; return this; }
        public IterationConfigBuilder WithMaxPlayers(int players) { _players = players; return this; }
        public IterationConfigBuilder WithDoorWarnings(params float[] seconds) { _warnings = seconds; return this; }

        public IterationConfig Build() =>
            new IterationConfig(_floors, _calm, _incursion, _waves, _players, _warnings);
    }
}
