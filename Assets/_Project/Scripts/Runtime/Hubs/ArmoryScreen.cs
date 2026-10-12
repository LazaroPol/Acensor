using Specimen.Core.Hubs;
using UnityEngine;

namespace Specimen.Runtime.Hubs
{
    /// <summary>Pantalla de la Armería (IMGUI): un botón por kit y «Salir al Ascensor».</summary>
    public sealed class ArmoryScreen : MonoBehaviour
    {
        private ArmoryPresenter _armory;
        private System.Action _onLeave;

        public void Init(ArmoryPresenter armory, System.Action onLeave)
        {
            _armory = armory;
            _onLeave = onLeave;
        }

        private void OnGUI()
        {
            if (_armory == null) return;
            GUI.Label(new Rect(20f, 80f, 600f, 28f), "ARMERÍA · " + _armory.Message);
            float y = 108f;
            foreach (var kit in _armory.Entries())
            {
                string mark = kit.Availability == KitAvailability.Mine ? "▶ " : "  ";
                GUI.Label(new Rect(20f, y, 420f, 24f), $"{mark}{kit.Name} ({kit.RoleAndResource})");
                y += 24f;
            }
            if (_onLeave != null && _armory.CanLeave && GUI.Button(new Rect(20f, y + 6f, 220f, 30f), "Salir al Ascensor")
                && _armory.Leave()) _onLeave();
        }
    }
}
