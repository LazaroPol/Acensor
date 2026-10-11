using System;
using UnityEngine.InputSystem;

namespace Specimen.Runtime.Prototype
{
    /// <summary>Acciones del Input System creadas por código: mover (WASD/flechas/stick) y esprintar (Shift/L3).</summary>
    public sealed class PrototypeInput : IDisposable
    {
        public PrototypeInput()
        {
            Move = new InputAction("Move", InputActionType.Value);
            Move.AddCompositeBinding("2DVector")
                .With("Up", "<Keyboard>/w").With("Down", "<Keyboard>/s")
                .With("Left", "<Keyboard>/a").With("Right", "<Keyboard>/d");
            Move.AddCompositeBinding("2DVector")
                .With("Up", "<Keyboard>/upArrow").With("Down", "<Keyboard>/downArrow")
                .With("Left", "<Keyboard>/leftArrow").With("Right", "<Keyboard>/rightArrow");
            Move.AddBinding("<Gamepad>/leftStick");
            Sprint = new InputAction("Sprint", InputActionType.Button);
            Sprint.AddBinding("<Keyboard>/leftShift");
            Sprint.AddBinding("<Gamepad>/leftStickPress");
            Move.Enable();
            Sprint.Enable();
        }

        public InputAction Move { get; }
        public InputAction Sprint { get; }

        public void Dispose()
        {
            Move.Dispose();
            Sprint.Dispose();
        }
    }
}
