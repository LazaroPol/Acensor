using Specimen.Runtime.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Specimen.Runtime.Sandbox
{
    /// <summary>
    /// Distingue clic de arrastre con cada botón del ratón (como en WoW): arrastrar mueve la cámara y un clic sin
    /// arrastre selecciona/ataca. Los gestos que empiezan sobre la interfaz se ignoran.
    /// </summary>
    [DefaultExecutionOrder(-20)]
    public sealed class MouseGestures : MonoBehaviour
    {
        private const float DragThreshold = 6f;

        private readonly Button _left = new Button();
        private readonly Button _right = new Button();

        public bool LeftClicked => _left.Clicked;
        public bool RightClicked => _right.Clicked;
        public bool LeftDragging => _left.Dragging;
        public bool RightDragging => _right.Dragging;
        public bool Dragging => LeftDragging || RightDragging;
        public Vector2 Delta { get; private set; }

        /// <summary>Posición del ratón al soltar el último clic (para elegir enemigo bajo el cursor).</summary>
        public Vector2 ClickPosition { get; private set; }

        private void Update()
        {
            var mouse = Mouse.current;
            if (mouse == null) return;
            Delta = mouse.delta.ReadValue();
            var position = mouse.position.ReadValue();
            _left.Step(mouse.leftButton, position, Delta);
            _right.Step(mouse.rightButton, position, Delta);
            if (_left.Clicked || _right.Clicked) ClickPosition = position;
            bool hide = Dragging;
            Cursor.lockState = hide ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !hide;
        }

        private void OnDisable()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        private sealed class Button
        {
            private bool _held;
            private bool _blocked;
            private float _travel;

            public bool Clicked { get; private set; }
            public bool Dragging { get; private set; }

            public void Step(ButtonControl control, Vector2 position, Vector2 delta)
            {
                Clicked = false;
                if (control.wasPressedThisFrame)
                {
                    _held = true;
                    _blocked = UiRoot.PointerOverUi();
                    _travel = 0f;
                }
                if (_held && !_blocked) _travel += delta.magnitude;
                Dragging = _held && !_blocked && _travel > DragThreshold;
                if (!control.wasReleasedThisFrame) return;
                Clicked = _held && !_blocked && _travel <= DragThreshold;
                _held = false;
                Dragging = false;
            }
        }
    }
}
