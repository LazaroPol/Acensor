using System;
using System.Collections.Generic;
using Specimen.Core.Classes;
using Specimen.Runtime.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Specimen.Runtime.Sandbox.Ui
{
    /// <summary>
    /// Personaje (C), como la hoja de WoW: huecos de equipo a la izquierda (clic derecho para quitar), estadísticas
    /// de combate y defensa a la derecha (armadura → % de reducción, daño y velocidad del arma) y selector de kit.
    /// </summary>
    public sealed partial class CharacterWindow : UiWindow
    {
        private Text _header;
        private Text _stats;
        private Text _extra;

        public SandboxContext Context { get; set; }
        public SandboxPlayer Player { get; set; }
        public IReadOnlyList<ClassKit> Kits { get; set; }
        public Action<ClassKit> Equip { get; set; }

        public override string Title => "Personaje";
        protected override Vector2 Size => new Vector2(720f, 690f);

        protected override void Build(UiRoot root)
        {
            _header = UiFactory.Label(Body, string.Empty, Vector2.zero, new Vector2(696f, 50f), 16);
            UiFactory.Panel(Body, "Separador", new Vector2(0f, 54f), new Vector2(696f, 2f), UiTheme.GoldDim, false);
            BuildSlots(new Vector2(0f, 66f));
            _stats = UiFactory.Label(Body, string.Empty, new Vector2(330f, 64f), new Vector2(366f, 440f), 14);
            _extra = UiFactory.Label(Body, string.Empty, new Vector2(0f, 512f), new Vector2(696f, 40f), 14);
            UiFactory.Label(Body, "<b>Kit de clase</b> (K cambia al siguiente)", new Vector2(0f, 556f),
                new Vector2(696f, 20f), 14, TextAnchor.UpperLeft, UiTheme.Gold);
            for (int i = 0; i < Kits.Count; i++)
            {
                var kit = Kits[i];
                var position = new Vector2(i % 3 * 232f, 580f + i / 3 * 36f);
                UiFactory.Button(Body, kit.DisplayName, position, new Vector2(226f, 30f), () => Equip(kit), 14);
            }
        }

        public override void Refresh()
        {
            if (Player.Kit == null) return;
            var progress = Context.Hero.Progress;
            string xp = progress.IsMaxLevel ? "nivel máximo" : $"{progress.Xp}/{progress.XpToNext} PX";
            _header.text = $"<size=20><b>{Player.Kit.DisplayName}</b></size>  <color=#B0A890>{Role(Player.Kit.Role)}</color>\n" +
                           $"Nivel <b>{progress.Level}</b> · {xp}";
            _stats.text = CharacterStats.Describe(Player, progress.Level);
            var professions = Context.Hero.Professions.All;
            _extra.text = $"<b>Profesiones</b> ({professions.Count}/2): " +
                          (professions.Count > 0 ? string.Join(", ", professions) : "ninguna — aprende una en un banco de la Armería") +
                          $"\n<b>Puntos de talento</b> sin gastar: {Context.Hero.AvailablePoints(Player.Kit.Id)}";
            RefreshSlots();
        }

        private static string Role(ClassRole role) => role switch
        {
            ClassRole.Tank => "Tanque",
            ClassRole.Support => "Apoyo",
            _ => "Daño"
        };
    }
}
