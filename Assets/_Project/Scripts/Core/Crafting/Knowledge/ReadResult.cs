namespace Specimen.Core.Crafting.Knowledge
{
    /// <summary>Resultado de <see cref="KnowledgeReader.Read"/>: qué pasó y si se consume el objeto.</summary>
    public readonly struct ReadResult
    {
        public KnowledgeDefinition Knowledge { get; }
        public ReadOutcome Outcome { get; }

        public ReadResult(KnowledgeDefinition knowledge, ReadOutcome outcome)
        {
            Knowledge = knowledge;
            Outcome = outcome;
        }

        /// <summary>Solo se consume el objeto si se aprendió algo.</summary>
        public bool Consumed => Outcome == ReadOutcome.Learned;

        /// <summary>Nivel de oficio necesario (para el tooltip «Necesitas Herrero 10…»).</summary>
        public int RequiredLevel => Knowledge?.MinLevel ?? 0;

        public override string ToString() => $"{Outcome} {Knowledge?.Id}";
    }
}
