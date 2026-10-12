using System;
using Specimen.Core.Crafting.Session;
using Specimen.Core.Crafting.Stations;
using Specimen.Core.Professions;
using Specimen.Runtime.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Specimen.Runtime.Sandbox.Ui
{
    /// <summary>
    /// Ventana de estación (GDD §17.7, E en un banco del poblado): recetas disponibles y en gris con motivo,
    /// cola con barras de progreso, ranuras de entrada, salida y combustible, piezas y mejora de estación.
    /// </summary>
    public sealed partial class StationWindow : UiWindow
    {
        private Text _header;
        private Text _message;
        private Button _upgrade;
        private Text _upgradeLabel;
        private BenchStation _bench;

        public SandboxContext Context { get; set; }

        /// <summary>Abre la ventana de profesión (aprender u olvidar el oficio de esta estación).</summary>
        public Action<ProfessionId> OpenTrade { get; set; }

        public override string Title => "Estación de oficio";
        protected override Vector2 Size => new Vector2(780f, 580f);

        private StationSession Session => _bench != null ? _bench.Session : null;

        public void Show(BenchStation bench)
        {
            if (_bench != bench) _builtFor = null;
            _bench = bench;
            _message.text = string.Empty;
            Open();
        }

        protected override void Build(UiRoot root)
        {
            _header = UiFactory.Label(Body, string.Empty, Vector2.zero, new Vector2(500f, 46f), 15);
            _upgrade = UiFactory.Button(Body, "Mejorar", new Vector2(510f, 4f), new Vector2(130f, 30f), Upgrade);
            _upgradeLabel = _upgrade.GetComponentInChildren<Text>();
            UiFactory.Button(Body, "Oficio", new Vector2(646f, 4f), new Vector2(110f, 30f),
                () => { if (_bench != null) OpenTrade?.Invoke(_bench.Profession); });
            UiFactory.Panel(Body, "Separador", new Vector2(0f, 50f), new Vector2(756f, 2f), UiTheme.GoldDim, false);
            BuildRecipes(new Vector2(0f, 60f));
            BuildQueue(new Vector2(276f, 60f));
            BuildSlots(new Vector2(520f, 60f));
            _message = UiFactory.Label(Body, string.Empty, new Vector2(276f, 470f), new Vector2(480f, 44f), 14,
                TextAnchor.UpperLeft, UiTheme.Gold);
        }

        public override void Refresh()
        {
            if (Session == null || Context == null) return;
            var station = Session.Station;
            var skill = Session.Profile.Skill(_bench.Profession);
            string trade = StationLabels.Profession(_bench.Profession);
            _header.text = $"<size=20><b>{StationLabels.Station(station.Type)}</b></size>  nivel {station.Level}\n" +
                           $"<color=#B0A890>{trade} {skill.Level} · {(skill.IsMaxed ? "máximo" : $"{skill.Xp}/{skill.Curve.XpToNext(skill.Level)} XP")} · " +
                           $"cola {Session.Queue.Jobs.Count}/{station.QueueSize}</color>";
            RefreshUpgrade();
            RefreshRecipes();
            RefreshQueue();
            RefreshSlots();
        }

        private void RefreshUpgrade()
        {
            var next = Session.NextUpgrade;
            _upgradeLabel.text = next == null ? "Nivel máximo" : $"Mejorar a nv{next.TargetLevel}";
            _upgrade.interactable = next != null && Session.CanUpgrade() == UpgradeResult.Upgraded;
        }

        private void Upgrade()
        {
            if (Session == null) return;
            var next = Session.NextUpgrade;
            var result = Session.Upgrade();
            Say(result == UpgradeResult.Upgraded
                ? $"Estación mejorada a nivel {Session.Station.Level}."
                : next == null ? "La estación ya está al máximo." : $"Para mejorar hace falta: {Ingredients(next.Cost)}");
        }

        /// <summary>Guardado automático al cerrar la ventana de estación (GDD §17.7).</summary>
        private void OnDisable() => Context?.SaveCrafting();

        private void Say(string text)
        {
            _message.text = text;
            Context.Feed.Add(text);
            Refresh();
        }
    }
}
