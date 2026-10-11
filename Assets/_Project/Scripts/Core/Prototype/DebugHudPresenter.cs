using System;
using System.Collections.Generic;
using System.Globalization;
using Specimen.Core.Common;
using Specimen.Core.Time;
using Specimen.Core.Vitals;

namespace Specimen.Core.Prototype
{
    /// <summary>
    /// Texto del HUD de depuración (PG-04): vida, recurso de clase, Aguante, estado vital y fase con su
    /// tiempo restante. Puro C#: la vista solo pinta <see cref="Text"/>.
    /// </summary>
    public sealed class DebugHudPresenter
    {
        public const string SpectreLabel = "ESPECTRO";
        public const string ExpiredLabel = "EXPIRADA";

        private readonly CharacterVitals _vitals;
        private readonly PhaseTimer _timer;
        private readonly string _phaseName;

        public DebugHudPresenter(CharacterVitals vitals, PhaseTimer timer, string phaseName)
        {
            _vitals = Guard.NotNull(vitals, nameof(vitals));
            _timer = Guard.NotNull(timer, nameof(timer));
            _phaseName = Guard.NotNull(phaseName, nameof(phaseName));
        }

        public IReadOnlyList<string> Lines()
        {
            var health = Pool("Vida", _vitals.Health);
            return new[]
            {
                _vitals.IsAlive ? health : health + " " + SpectreLabel,
                Pool("Recurso", _vitals.ClassResource),
                Pool("Aguante", _vitals.Stamina),
                "Fase " + _phaseName + " " + (_timer.IsExpired ? ExpiredLabel : HudTime.Format(_timer.Remaining))
            };
        }

        public string Text => string.Join("\n", Lines());

        private static string Pool(string label, ResourcePool pool) =>
            string.Format(CultureInfo.InvariantCulture, "{0} {1}/{2}", label, Round(pool.Current), Round(pool.Max));

        private static int Round(float value) => (int)Math.Round(value, MidpointRounding.AwayFromZero);
    }
}
