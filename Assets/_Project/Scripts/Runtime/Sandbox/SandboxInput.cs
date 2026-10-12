using System;
using UnityEngine.InputSystem;

namespace Specimen.Runtime.Sandbox
{
    /// <summary>
    /// Entradas propias del Campo de pruebas (mover, esprintar y esquivar las lleva PlayerController):
    /// ataque, firma, interactuar, cambio de kit (1-6 / Tab), reiniciar, dañar/curar y ayuda.
    /// </summary>
    public sealed class SandboxInput : IDisposable
    {
        public const int ClassKeys = 6;

        public SandboxInput()
        {
            Attack = Button("Attack", "<Mouse>/leftButton", "<Keyboard>/j", "<Gamepad>/buttonWest");
            Ability = Button("Ability", "<Keyboard>/q", "<Gamepad>/buttonNorth");
            Interact = Button("Interact", "<Keyboard>/e", "<Gamepad>/buttonSouth");
            NextClass = Button("NextClass", "<Keyboard>/tab", "<Gamepad>/rightShoulder");
            Restart = Button("Restart", "<Keyboard>/r", "<Gamepad>/select");
            Hurt = Button("Hurt", "<Keyboard>/f1");
            Heal = Button("Heal", "<Keyboard>/f2");
            Help = Button("Help", "<Keyboard>/h");
            Classes = new InputAction[ClassKeys];
            for (int i = 0; i < ClassKeys; i++) Classes[i] = Button($"Class{i + 1}", $"<Keyboard>/{i + 1}");
        }

        public InputAction Attack { get; }
        public InputAction Ability { get; }
        public InputAction Interact { get; }
        public InputAction NextClass { get; }
        public InputAction Restart { get; }
        public InputAction Hurt { get; }
        public InputAction Heal { get; }
        public InputAction Help { get; }
        public InputAction[] Classes { get; }

        /// <summary>Índice 0-5 de la tecla de kit pulsada este frame, o -1.</summary>
        public int ClassPressed()
        {
            for (int i = 0; i < ClassKeys; i++)
                if (Classes[i].WasPressedThisFrame()) return i;
            return -1;
        }

        private static InputAction Button(string name, params string[] bindings)
        {
            var action = new InputAction(name, InputActionType.Button);
            foreach (string binding in bindings) action.AddBinding(binding);
            action.Enable();
            return action;
        }

        public void Dispose()
        {
            foreach (var action in new[] { Attack, Ability, Interact, NextClass, Restart, Hurt, Heal, Help })
                action.Dispose();
            foreach (var action in Classes) action.Dispose();
        }
    }
}
