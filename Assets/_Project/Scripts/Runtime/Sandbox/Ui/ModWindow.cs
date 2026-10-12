using System;
using System.Collections.Generic;
using Specimen.Core.Crafting.Mods;
using Specimen.Core.Crafting.Play;
using Specimen.Core.Items;
using Specimen.Runtime.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Specimen.Runtime.Sandbox.Ui
{
    /// <summary>
    /// Ventana de mods de un objeto (GDD §17.4, §17.7; clic izquierdo en la bolsa): huecos usados, stats
    /// base → efectivo en verde/rojo, mods instalados (clic para quitar) y mods de la bolsa (clic para poner).
    /// </summary>
    public sealed class ModWindow : UiWindow
    {
        private const int InstalledRows = 4;
        private const int BagRows = 6;
        private static readonly GearStat[] Stats = (GearStat[])Enum.GetValues(typeof(GearStat));

        private readonly List<Text> _stats = new List<Text>();
        private readonly List<(Button button, Text label)> _installed = new List<(Button, Text)>();
        private readonly List<(Button button, Text label)> _bag = new List<(Button, Text)>();
        private Text _header;
        private Text _message;
        private ItemInstance _item;

        public SandboxContext Context { get; set; }

        public override string Title => "Mods del objeto";
        protected override Vector2 Size => new Vector2(420f, 600f);

        public void Show(ItemInstance item)
        {
            _item = item;
            _message.text = string.Empty;
            Open();
        }

        protected override void Build(UiRoot root)
        {
            _header = UiFactory.Label(Body, string.Empty, Vector2.zero, new Vector2(396f, 44f), 15);
            for (int i = 0; i < Stats.Length; i++)
                _stats.Add(UiFactory.Label(Body, string.Empty, new Vector2(0f, 50f + i * 22f), new Vector2(396f, 20f), 13));
            Section("Instalados (clic para quitar)", 210f, InstalledRows, _installed, Remove);
            Section("En la bolsa (clic para poner)", 360f, BagRows, _bag, Install);
            _message = UiFactory.Label(Body, string.Empty, new Vector2(0f, 540f), new Vector2(396f, 22f), 13,
                TextAnchor.UpperLeft, UiTheme.Gold);
        }

        public override void Refresh()
        {
            if (_item == null || Context == null) return;
            var modded = Context.Mods.For(_item);
            string grade = modded.Grade != null ? modded.Grade.Name : "sin calidad";
            _header.text = $"<size=18><b>{ItemView.Colored(_item.Definition)}</b></size>\n" +
                           $"<color=#B0A890>{grade} · huecos {modded.Mods.Count}/{modded.SlotCount}</color>";
            var view = Context.Mods.Stats(_item);
            for (int i = 0; i < Stats.Length; i++)
            {
                _stats[i].text = GearModLabels.Line(view, Stats[i]);
                int trend = GearModLabels.Trend(view, Stats[i]);
                _stats[i].color = trend > 0 ? UiTheme.Good : trend < 0 ? UiTheme.Bad : UiTheme.Text;
            }
            Fill(_installed, modded.Mods, "Quitar");
            Fill(_bag, Context.Mods.Installable(_item), "Poner");
        }

        private void Section(string title, float y, int rows, List<(Button, Text)> target, Action<int> onClick)
        {
            UiFactory.Label(Body, $"<b>{title}</b>", new Vector2(0f, y), new Vector2(396f, 20f), 14,
                TextAnchor.UpperLeft, UiTheme.Gold);
            for (int i = 0; i < rows; i++)
            {
                int index = i;
                var button = UiFactory.Button(Body, string.Empty, new Vector2(0f, y + 24f + i * 30f),
                    new Vector2(396f, 26f), () => onClick(index), 13);
                target.Add((button, button.GetComponentInChildren<Text>()));
            }
        }

        private static void Fill(List<(Button button, Text label)> rows, IReadOnlyList<ModDefinition> mods, string verb)
        {
            for (int i = 0; i < rows.Count; i++)
            {
                bool used = i < mods.Count;
                rows[i].button.gameObject.SetActive(used);
                if (used) rows[i].label.text = $"{verb}: {mods[i].Name} [{mods[i].SlotType}]";
            }
        }

        private void Remove(int index)
        {
            var mods = Context.Mods.For(_item).Mods;
            if (index < mods.Count) Say(GearModLabels.Result(Context.Mods.Uninstall(_item, mods[index].Id)));
        }

        private void Install(int index)
        {
            var mods = Context.Mods.Installable(_item);
            if (index < mods.Count) Say(GearModLabels.Result(Context.Mods.Install(_item, mods[index].Id)));
        }

        private void Say(string text)
        {
            _message.text = text;
            Context.Feed.Add(text);
            Refresh();
        }
    }
}
