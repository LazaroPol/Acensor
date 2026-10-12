using System.Collections.Generic;

namespace Specimen.Core.Siege.Extraction
{
    /// <summary>Quién subió al Osprey y quién se quedó atrás.</summary>
    public sealed class ExtractionReport
    {
        public ExtractionReport(IReadOnlyList<int> boarded, IReadOnlyList<int> leftBehind)
        {
            Boarded = boarded;
            LeftBehind = leftBehind;
        }

        public IReadOnlyList<int> Boarded { get; }
        public IReadOnlyList<int> LeftBehind { get; }
        public bool EveryoneBoarded => LeftBehind.Count == 0;
    }
}
