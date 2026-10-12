using Specimen.Core.Armory;
using Specimen.Core.Composition;
using UnityEngine;

namespace Specimen.Runtime.Player
{
    /// <summary>Etiqueta IMGUI arriba a la derecha con el kit que lleva el jugador local (o «sin kit»).</summary>
    public sealed class KitBadge : MonoBehaviour
    {
        private IServiceResolver _services;

        public void Init(IServiceResolver services) => _services = services;

        private void OnGUI()
        {
            if (_services == null) return;
            string text = _services.Resolve<ClassKitArmory>().TryGetKit(GameComposition.LocalPlayerId, out var kit)
                ? $"Kit: {kit.DisplayName} ({kit.Role})"
                : "Kit: sin elegir";
            GUI.Label(new Rect(Screen.width - 330f, 10f, 320f, 24f), text);
        }
    }
}
