using System;
using UnityEngine.InputSystem;

namespace Specimen.Runtime.Player
{
    /// <summary>Acciones del jugador: mover, esprintar, esquivar (Espacio/B) y zoom de cámara (rueda del ratón).</summary>
    public sealed class PlayerInput : IDisposable
    {
        public PlayerInput()
        {
            Move = new InputAction("Move", InputActionType.Value);
            Move.AddCompositeBinding("2DVector")
                .With("Up", "<Keyboard>/w").With("Down", "<Keyboard>/s")
                .With("Left", "<Keyboard>/a").With("Right", "<Keyboard>/d");
            Move.AddBinding("<Gamepad>/leftStick");
            Sprint = Button("Sprint", "<Keyboard>/leftShift", "<Gamepad>/leftStickPress");
            Dodge = Button("Dodge", "<Keyboard>/space", "<Gamepad>/buttonEast");
            Zoom = new InputAction("Zoom", InputActionType.Value);
            Zoom.AddBinding("<Mouse>/scroll/y");
            Move.Enable();
            Zoom.Enable();
        }

        public InputAction Move { get; }
        public InputAction Sprint { get; }
        public InputAction Dodge { get; }
        public InputAction Zoom { get; }

        private static InputAction Button(string name, string key, string pad)
        {
            var action = new InputAction(name, InputActionType.Button);
            action.AddBinding(key);
            action.AddBinding(pad);
            action.Enable();
            return action;
        }

        public void Dispose()
        {
            Move.Dispose();
            Sprint.Dispose();
            Dodge.Dispose();
            Zoom.Dispose();
        }
    }
}
