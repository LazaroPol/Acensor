using System.Collections.Generic;
using Specimen.Core.Crafting.Queue;
using Specimen.Core.Crafting.Session;
using Specimen.Core.Crafting.Stations;
using Specimen.Runtime.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Specimen.Runtime.Sandbox.Ui
{
    /// <summary>Columna central: estado de la cola y una barra de progreso por trabajo (cancelable si no ha empezado).</summary>
    public sealed partial class StationWindow
    {
        private const int MaxQueueRows = Workstation.BaseQueueSize * 3;

        private readonly List<(Image fill, Text label, Button cancel)> _bars = new List<(Image, Text, Button)>();
        private Text _state;

        private void BuildQueue(Vector2 origin)
        {
            UiFactory.Label(Body, "<b>Cola</b>", origin, new Vector2(230f, 20f), 14, TextAnchor.UpperLeft, UiTheme.Gold);
            _state = UiFactory.Label(Body, string.Empty, origin + new Vector2(0f, 22f), new Vector2(230f, 20f), 13);
            for (int i = 0; i < MaxQueueRows; i++)
            {
                int index = i;
                var position = origin + new Vector2(0f, 48f + i * 34f);
                var fill = UiFactory.Bar(Body, position, new Vector2(200f, 26f), UiTheme.Stamina, out var label);
                var cancel = UiFactory.Button(Body, "X", position + new Vector2(206f, 1f), new Vector2(24f, 24f),
                    () => Cancel(index), 12);
                _bars.Add((fill, label, cancel));
            }
        }

        private void RefreshQueue()
        {
            var queue = Session.Queue;
            var color = queue.State == CraftQueueState.Working ? "5AE65A" : queue.State == CraftQueueState.Idle ? "B0A890" : "F04A3A";
            _state.text = $"<color=#{color}>{StationLabels.State(queue.State)}</color>";
            for (int i = 0; i < _bars.Count; i++)
            {
                var (fill, label, cancel) = _bars[i];
                bool used = i < queue.Jobs.Count;
                fill.transform.parent.gameObject.SetActive(used);
                cancel.gameObject.SetActive(used && !queue.Jobs[i].IsStarted);
                if (!used) continue;
                var job = queue.Jobs[i];
                fill.fillAmount = job.Progress;
                fill.color = queue.State == CraftQueueState.NoFuel && i == 0 ? UiTheme.Bad : UiTheme.Stamina;
                label.text = $"{job.Recipe.MainOutput.Item.Name}  {job.Duration - job.Remaining:0.0}/{job.Duration:0}s";
            }
        }

        private void Cancel(int index)
        {
            var jobs = Session?.Queue.Jobs;
            if (jobs == null || index >= jobs.Count) return;
            Say(Session.Queue.Cancel(jobs[index]) ? "Trabajo cancelado: materiales devueltos." : "Ese trabajo ya ha empezado.");
        }
    }
}
