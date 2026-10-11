using Specimen.Core.Prototype;
using Specimen.Core.Time;
using Specimen.Core.Vitals;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Specimen.Runtime.Prototype
{
    /// <summary>
    /// HUD de depuración (IMGUI): pinta el texto de <see cref="DebugHudPresenter"/>, avanza el temporizador
    /// de fase y atiende las teclas de depuración (F1 dañar, F2 curar, F3 avanzar tiempo). Sin lógica propia.
    /// </summary>
    public sealed class DebugHud : MonoBehaviour
    {
        private const string Help = "F1 dañar · F2 curar · F3 avanzar tiempo";

        [SerializeField] private string phaseName = "Calma";
        [SerializeField] private float phaseSeconds = 300f;

        private PhaseTimer _timer;
        private DebugHudPresenter _presenter;
        private DebugCommands _commands;
        private GUIStyle _style;

        public void Init(CharacterVitals vitals)
        {
            _timer = new PhaseTimer(phaseSeconds);
            _presenter = new DebugHudPresenter(vitals, _timer, phaseName);
            _commands = new DebugCommands(vitals, _timer);
            _timer.Start();
        }

        private void Update()
        {
            if (_timer == null) return;
            _timer.Tick(Time.deltaTime);
            var keyboard = Keyboard.current;
            if (keyboard == null) return;
            if (keyboard.f1Key.wasPressedThisFrame) _commands.Hurt();
            if (keyboard.f2Key.wasPressedThisFrame) _commands.Cure();
            if (keyboard.f3Key.wasPressedThisFrame) _commands.AdvanceTime();
        }

        private void OnGUI()
        {
            if (_presenter == null) return;
            _style ??= new GUIStyle(GUI.skin.box) { alignment = TextAnchor.UpperLeft, fontSize = 16 };
            GUI.Box(new Rect(10f, 10f, 320f, 120f), _presenter.Text + "\n" + Help, _style);
        }
    }
}
