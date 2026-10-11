namespace Specimen.Core.Echoes.Voting
{
    /// <summary>Resultado de un voto de Eco: opción ganadora y si hizo falta desempatar.</summary>
    public readonly struct EchoVoteResult
    {
        public EchoVoteResult(int winner, bool wasTieBreak)
        {
            Winner = winner;
            WasTieBreak = wasTieBreak;
        }

        public int Winner { get; }
        public bool WasTieBreak { get; }
    }
}
