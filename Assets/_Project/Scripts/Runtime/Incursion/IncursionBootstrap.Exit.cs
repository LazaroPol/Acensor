using Specimen.Core.Composition;
using Specimen.Core.Iteration;
using Specimen.Core.Iteration.Presence;
using Specimen.Core.Loot;
using Specimen.Runtime.Bootstrap;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Specimen.Runtime.Incursion
{
    /// <summary>Salida del piso: regreso al Ascensor a tiempo, o atrapado, Hueco de Emergencia y fracaso.</summary>
    public sealed partial class IncursionBootstrap
    {
        private int Me => GameComposition.LocalPlayerId;
        private bool Trapped => _run.Location(Me) == PlayerLocation.Trapped;
        private bool Ended => _run.Phase == IterationPhase.Ended;

        private void Update()
        {
            if (_run == null) return;
            _run.Tick(Time.deltaTime);
            if (Ended) return;
            if (!_vitals.IsAlive) Respawn();
            if (_run.Phase == IterationPhase.Incursion) { if (Pressed() && Near(_elevator)) BoardElevator(); return; }
            if (Trapped) { if (Pressed() && Near(_shaft)) _run.Abandon(); return; }
            _flow.GoTo(SceneId.Ascensor);
        }

        private void BoardElevator()
        {
            _run.ReturnToElevator(Me);
            _run.VoteEarlyClose(Me, true);
            GameBootstrap.Services.Resolve<RunHaul>().Add(_loot.Collected);
            _flow.GoTo(SceneId.Ascensor);
        }

        private static bool Pressed() => Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame;

        private void OnGUI()
        {
            if (_run == null) return;
            if (Ended) { DefeatScreen(); return; }
            GUI.Label(new Rect(10f, Screen.height - 54f, 600f, 24f), $"Botín recogido: {_loot.Collected}");
            GUI.Label(new Rect(10f, Screen.height - 30f, 700f, 24f), Hint());
        }

        private string Hint()
        {
            if (Trapped) return "El Ascensor se fue sin ti. Busca el Hueco de Emergencia (marco marrón) y pulsa E para saltar";
            return Near(_elevator) ? "[E] Subir al Ascensor y cerrar el piso" : "Limpia el piso, coge el botín y vuelve al Ascensor antes de que cierre";
        }

        private void DefeatScreen()
        {
            GUI.Label(new Rect(Screen.width / 2f - 150f, Screen.height / 2f - 40f, 300f, 28f), "Has saltado por el Hueco. Fracaso.");
            if (!GUI.Button(new Rect(Screen.width / 2f - 100f, Screen.height / 2f, 200f, 36f), "Volver a la Armería")) return;
            GameBootstrap.NewGame();
            SceneManager.LoadScene(nameof(SceneId.Armeria));
        }
    }
}
