using System;

namespace Specimen.Core.Professions
{
    /// <summary>Receta disponible solo si el grupo tiene ambas profesiones (GDD §7).</summary>
    public sealed class ProfessionSynergy
    {
        public ProfessionSynergy(ProfessionId first, ProfessionId second, string recipe)
        {
            if (first == second) throw new ArgumentException("Una sinergia requiere dos profesiones distintas.");
            if (string.IsNullOrWhiteSpace(recipe)) throw new ArgumentException("Receta vacía.", nameof(recipe));
            First = first;
            Second = second;
            Recipe = recipe;
        }

        public ProfessionId First { get; }
        public ProfessionId Second { get; }
        public string Recipe { get; }

        public bool Involves(ProfessionId id) => First == id || Second == id;
    }
}
