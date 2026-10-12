using Specimen.Core.Lab;
using UnityEngine;

namespace Specimen.Runtime.Lab
{
    /// <summary>Pantalla del Laboratorio Genético (IMGUI): una columna por rama, nodos clicables y panel de selección.</summary>
    public sealed class LabScreen : MonoBehaviour
    {
        private const float ColumnWidth = 190f;
        private const float NodeHeight = 34f;

        private LabPresenter _lab;

        public void Init(LabPresenter lab) => _lab = lab;

        private void OnGUI()
        {
            if (_lab == null) return;
            GUI.Label(new Rect(20f, 10f, 400f, 28f), $"LABORATORIO · Puntos de ADN: {_lab.FreePoints}");
            float x = 20f;
            foreach (var branch in _lab.Branches())
            {
                Column(x, branch);
                x += ColumnWidth + 10f;
            }
            Footer();
        }

        private void Column(float x, LabBranchView branch)
        {
            GUI.Label(new Rect(x, 44f, ColumnWidth, 24f), $"{branch.Branch} ({branch.Invested})");
            float y = 72f;
            foreach (var node in branch.Nodes)
            {
                var previous = GUI.color;
                GUI.color = Tint(node.State);
                string mark = node.IsSelected ? "▶ " : string.Empty;
                if (GUI.Button(new Rect(x, y, ColumnWidth, NodeHeight), $"{mark}T{node.Tier} {node.Id} ({node.Cost})"))
                    _lab.Select(node.Id);
                GUI.color = previous;
                y += NodeHeight + 4f;
            }
        }

        private void Footer()
        {
            float y = Screen.height - 70f;
            if (_lab.SelectedId != null)
            {
                int missing = _lab.PointsMissingForSelected();
                string gap = missing > 0 ? $" · faltan {missing} pts en la rama" : string.Empty;
                GUI.Label(new Rect(20f, y, 600f, 24f), $"Seleccionada: {_lab.SelectedId}{gap}");
                if (GUI.Button(new Rect(640f, y, 160f, 28f), "Desbloquear")) _lab.UnlockSelected();
            }
            GUI.Label(new Rect(20f, y + 28f, 800f, 24f), _lab.Message);
        }

        private static Color Tint(NodeState state)
        {
            switch (state)
            {
                case NodeState.Unlocked: return new Color(0.5f, 1f, 0.5f);
                case NodeState.Available: return Color.white;
                case NodeState.TooExpensive: return new Color(1f, 0.85f, 0.4f);
                default: return new Color(0.5f, 0.5f, 0.5f);
            }
        }
    }
}
