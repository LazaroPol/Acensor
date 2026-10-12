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
            GUI.Label(new Rect(20f, 10f, 600f, 28f), "ARMERÍA · " + _armory.Message);
            float y = 48f;
            foreach (var kit in _armory.Entries())
            {
                GUI.enabled = kit.Availability != KitAvailability.Unavailable;
                string mark = kit.Availability == KitAvailability.Mine ? "▶ " : string.Empty;
                if (GUI.Button(new Rect(20f, y, 360f, 34f), $"{mark}{kit.Name} ({kit.RoleAndResource})"))
                    _armory.Choose(kit.Id);
                y += 38f;
            }
            GUI.enabled = _armory.CanLeave;
            if (GUI.Button(new Rect(20f, y + 10f, 360f, 38f), "Salir al Ascensor") && _armory.Leave()) _onLeave?.Invoke();
            GUI.enabled = true;
        }
    }
}
