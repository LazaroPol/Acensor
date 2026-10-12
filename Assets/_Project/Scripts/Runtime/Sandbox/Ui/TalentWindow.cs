using System.Collections.Generic;
using Specimen.Core.Talents;
using Specimen.Runtime.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Specimen.Runtime.Sandbox.Ui
{
    /// <summary>
    /// Talentos (N), al estilo de los árboles clásicos de WoW: tres ramas por kit, cinco filas; cada fila pide
    /// 5 puntos gastados en su rama. Un punto por nivel a partir del 2. Clic para gastar, «Reiniciar» para recuperar.
    /// </summary>
    public sealed partial class TalentWindow : UiWindow
    {
        private const float Column = 214f;
        private const float RowHeight = 74f;
        private const float Top = 58f;
        private const float SlotSize = 48f;

        private readonly List<TalentCell> _cells = new List<TalentCell>();
        private readonly List<Text> _headers = new List<Text>();
        private RectTransform _grid;
        private Text _points;
        private TalentTree _tree;

        public SandboxContext Context { get; set; }
        public SandboxPlayer Player { get; set; }

        public override string Title => "Talentos";
        protected override Vector2 Size => new Vector2(3 * Column + 24f, Top + TalentDefinition.Tiers * RowHeight + 100f);

        private TalentAllocation Allocation => Context.Hero.TalentsFor(Player.Kit.Id);

        protected override void Build(UiRoot root)
        {
            _points = UiFactory.Label(Body, string.Empty, new Vector2(0f, Size.y - TitleHeight - 50f),
                new Vector2(420f, 24f), 15, TextAnchor.MiddleLeft, UiTheme.Gold);
            UiFactory.Button(Body, "Reiniciar talentos", new Vector2(Size.x - 210f, Size.y - TitleHeight - 52f),
                new Vector2(180f, 28f), ResetTalents);
        }

        public override void Refresh()
        {
            if (Player.Kit == null) return;
            if (_tree != Allocation.Tree) Rebuild(Allocation.Tree);
            for (int b = 0; b < _tree.Branches.Count; b++)
            {
                var branch = _tree.Branches[b];
                _headers[b].text = $"<b>{branch.Name}</b> ({Allocation.PointsIn(b)})\n<color=#B0A890><i>{branch.Motto}</i></color>";
            }
            foreach (var cell in _cells) cell.Refresh(Allocation, Context.Hero.CanSpend(Player.Kit.Id, cell.Talent.Id));
            int available = Context.Hero.AvailablePoints(Player.Kit.Id);
            _points.text = $"Puntos disponibles: {available}   ·   Gastados: {Allocation.TotalSpent}";
        }

        private void Rebuild(TalentTree tree)
        {
            if (_grid != null) Destroy(_grid.gameObject);
            _cells.Clear();
            _headers.Clear();
            _tree = tree;
            _grid = UiFactory.Node(Body, "Ramas", Vector2.zero, new Vector2(3 * Column, Top + TalentDefinition.Tiers * RowHeight));
            for (int b = 0; b < tree.Branches.Count; b++) BuildBranch(tree.Branches[b], b);
        }

        private void BuildBranch(TalentBranch branch, int index)
        {
            float x = index * Column;
            var column = UiFactory.Panel(_grid, branch.Name, new Vector2(x, 0f),
                new Vector2(Column - 8f, Top + TalentDefinition.Tiers * RowHeight), new Color(0.1f, 0.09f, 0.08f, 0.9f));
            _headers.Add(UiFactory.Label(column, branch.Name, new Vector2(8f, 6f), new Vector2(Column - 24f, 48f), 14,
                TextAnchor.UpperCenter));
            for (int tier = 0; tier < TalentDefinition.Tiers; tier++)
            {
                var talents = new List<TalentDefinition>(branch.InTier(tier));
                for (int i = 0; i < talents.Count; i++)
                {
                    float cx = (Column - 8f) / (talents.Count + 1) * (i + 1) - SlotSize / 2f;
                    var position = new Vector2(cx, Top + tier * RowHeight + 4f);
                    _cells.Add(new TalentCell(column, position, SlotSize, talents[i], index, Spend));
                }
            }
        }

        private void Spend(TalentDefinition talent)
        {
            if (!Context.Hero.TrySpend(Player.Kit.Id, talent.Id)) return;
            Player.RefreshProgression(false);
            Context.Feed.Add($"Talento: {talent.Name} (rango {Allocation.RankOf(talent.Id)}/{talent.MaxRank})");
            Refresh();
        }

        private void ResetTalents()
        {
            Allocation.Reset();
            Player.RefreshProgression(false);
            Context.Feed.Add("Talentos reiniciados.");
            Refresh();
        }
    }
}
