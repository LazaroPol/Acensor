using Specimen.Core.Composition;
using Specimen.Core.Hubs;
using UnityEngine;

namespace Specimen.Runtime.Hubs
{
    /// <summary>Pantalla del Ascensor (IMGUI): ranuras de módulos, «listo» y acceso a Armería y Laboratorio.</summary>
    public sealed class ElevatorScreen : MonoBehaviour
    {
        private ElevatorPresenter _elevator;
        private SceneFlow _flow;

        public void Init(ElevatorPresenter elevator, SceneFlow flow)
        {
            _elevator = elevator;
            _flow = flow;
        }

        private void OnGUI()
        {
            if (_elevator == null) return;
            GUI.Label(new Rect(20f, 10f, 600f, 28f), "ASCENSOR-REFUGIO");
            float y = 44f;
            foreach (var label in _elevator.SlotLabels())
            {
                GUI.Label(new Rect(20f, y, 360f, 24f), label);
                y += 26f;
            }
            GUI.enabled = _elevator.IsCalm;
            string text = _elevator.IsReady ? "Listo ✔ (cancelar)" : "Estoy listo para salir";
            if (GUI.Button(new Rect(20f, y + 10f, 360f, 38f), text)) _elevator.ToggleReady();
            GUI.enabled = true;
            if (_elevator.CanExit && GUI.Button(new Rect(20f, y + 100f, 360f, 34f), "Salir al piso"))
                _flow.GoTo(SceneId.Incursion);
            if (GUI.Button(new Rect(20f, y + 56f, 360f, 34f), "Laboratorio Genético")) _flow.GoTo(SceneId.Laboratorio);
        }
    }
}
