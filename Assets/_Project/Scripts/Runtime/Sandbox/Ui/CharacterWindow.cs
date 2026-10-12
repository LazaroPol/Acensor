using System;
using System.Collections.Generic;
using System.Text;
using Specimen.Core.Classes;
using Specimen.Core.Stats;
using Specimen.Core.Talents;
using Specimen.Runtime.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Specimen.Runtime.Sandbox.Ui
{
    /// <summary>Personaje (C): kit, nivel y experiencia, atributos, profesiones y selector de kit de clase.</summary>
    public sealed class CharacterWindow : UiWindow
    {
        private Text _header;
        private Text _stats;
        private Text _extra;

        public SandboxContext Context { get; set; }
        public SandboxPlayer Player { get; set; }
        public IReadOnlyList<ClassKit> Kits { get; set; }
        public Action<ClassKit> Equip { get; set; }

        public override string Title => "Personaje";
        protected override Vector2 Size => new Vector2(440f, 560f);

        protected override void Build(UiRoot root)
        {
            _header = UiFactory.Label(Body, string.Empty, Vector2.zero, new Vector2(416f, 60f), 16);
            UiFactory.Panel(Body, "Separador", new Vector2(0f, 64f), new Vector2(416f, 2f), UiTheme.GoldDim, false);
            _stats = UiFactory.Label(Body, string.Empty, new Vector2(0f, 74f), new Vector2(416f, 200f), 15);
            _extra = UiFactory.Label(Body, string.Empty, new Vector2(0f, 270f), new Vector2(416f, 90f), 14);
            UiFactory.Label(Body, "<b>Kit de clase</b> (Tab cambia al siguiente)", new Vector2(0f, 368f),
                new Vector2(416f, 20f), 14, TextAnchor.UpperLeft, UiTheme.Gold);
            for (int i = 0; i < Kits.Count; i++)
            {
                var kit = Kits[i];
                var position = new Vector2(i % 2 * 210f, 394f + i / 2 * 36f);
                UiFactory.Button(Body, kit.DisplayName, position, new Vector2(204f, 30f), () => Equip(kit), 14);
            }
        }

        public override void Refresh()
        {
            if (Player.Kit == null) return;
            var progress = Context.Hero.Progress;
            string xp = progress.IsMaxLevel ? "nivel máximo" : $"{progress.Xp}/{progress.XpToNext} PX";
            _header.text = $"<size=20><b>{Player.Kit.DisplayName}</b></size>  <color=#B0A890>{Role(Player.Kit.Role)}</color>\n" +
                           $"Nivel <b>{progress.Level}</b> · {xp}";
            _stats.text = Stats(Player.Stats);
            var professions = Context.Hero.Professions.All;
            _extra.text = $"<b>Profesiones</b> ({professions.Count}/2): " +
                          (professions.Count > 0 ? string.Join(", ", professions) : "ninguna — aprende una en un banco de la Armería") +
                          $"\n<b>Poder de hechizos</b> +{Player.Bonuses.SpellPower * 100f:0} % · " +
                          $"<b>Enfriamientos</b> −{Player.Bonuses.CooldownReduction * 100f:0} %" +
                          $"\n<b>Puntos de talento</b> sin gastar: {Context.Hero.AvailablePoints(Player.Kit.Id)}";
        }

        private static string Stats(StatSheet sheet)
        {
            var text = new StringBuilder();
            foreach (StatType type in Enum.GetValues(typeof(StatType)))
            {
                if (!sheet.Has(type)) continue;
                float value = sheet.ValueOf(type);
                string shown = type == StatType.CritChance ? $"{value * 100f:0.#} %" : $"{value:0.#}";
                string name = TalentText.StatName(type);
                text.Append($"{char.ToUpper(name[0])}{name.Substring(1)}: <color=#FFFFFF><b>{shown}</b></color>\n");
            }
            return text.ToString();
        }

        private static string Role(ClassRole role) => role switch
        {
            ClassRole.Tank => "Tanque",
            ClassRole.Support => "Apoyo",
            _ => "Daño"
        };
    }
}
