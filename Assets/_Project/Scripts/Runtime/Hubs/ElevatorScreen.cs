using Specimen.Core.Composition;
using Specimen.Core.Hubs;
using UnityEngine;

namespace Specimen.Runtime.Hubs
{
    /// <summary>Pantalla del Ascensor (IMGUI): ranuras de módulos, «listo» y acceso a Armería y Laboratorio.</summary>
    public sealed class ElevatorScreen : MonoBehaviour
    {
        private ElevatorPresenter _elevator;

        public void Init(ElevatorPresenter elevator, SceneFlow _)
        {
            _elevator = elevator;
        }

        private void OnGUI()
        {
            if (_elevator == null) return;
            GUI.Label(new Rect(20f, 80f, 600f, 28f), "ASCENSOR-REFUGIO · acércate a las puertas y pulsa E");
            float y = 108f;
            foreach (var label in _elevator.SlotLabels())
            {
                GUI.Label(new Rect(20f, y, 360f, 24f), label);
                y += 24f;
            }
        }
    }
}
