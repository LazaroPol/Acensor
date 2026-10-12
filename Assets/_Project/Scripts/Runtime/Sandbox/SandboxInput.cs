using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace Specimen.Runtime.Sandbox
{
    /// <summary>
    /// Entradas propias del Campo de pruebas (mover, esprintar y esquivar las lleva PlayerController):
    /// ataque, firma, hechizos 1-5, interactuar, ventanas estilo MMO (B/I, C, P, N, M, Esc), cambio de kit (Tab)
    /// y atajos de prueba (reiniciar, dañar/curar, ayuda).
    /// </summary>
    public sealed class SandboxInput : IDisposable
    {
        public const int SpellKeys = 5;

        private readonly List<InputAction> _all = new List<InputAction>();

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
            Bag = Button("Bag", "<Keyboard>/b", "<Keyboard>/i");
            Character = Button("Character", "<Keyboard>/c");
            Spellbook = Button("Spellbook", "<Keyboard>/p");
            Talents = Button("Talents", "<Keyboard>/n");
            Map = Button("Map", "<Keyboard>/m");
            Close = Button("Close", "<Keyboard>/escape");
            Spells = new InputAction[SpellKeys];
            for (int i = 0; i < SpellKeys; i++) Spells[i] = Button($"Spell{i + 1}", $"<Keyboard>/{i + 1}");
        }

        public InputAction Attack { get; }
        public InputAction Ability { get; }
        public InputAction Interact { get; }
        public InputAction NextClass { get; }
        public InputAction Restart { get; }
        public InputAction Hurt { get; }
        public InputAction Heal { get; }
        public InputAction Help { get; }
        public InputAction Bag { get; }
        public InputAction Character { get; }
        public InputAction Spellbook { get; }
        public InputAction Talents { get; }
        public InputAction Map { get; }
        public InputAction Close { get; }
        public InputAction[] Spells { get; }

        /// <summary>Índice 0-4 de la tecla de hechizo pulsada este frame, o -1.</summary>
        public int SpellPressed()
        {
            for (int i = 0; i < SpellKeys; i++)
                if (Spells[i].WasPressedThisFrame()) return i;
            return -1;
        }

        private InputAction Button(string name, params string[] bindings)
        {
            var action = new InputAction(name, InputActionType.Button);
            foreach (string binding in bindings) action.AddBinding(binding);
            action.Enable();
            _all.Add(action);
            return action;
        }

        public void Dispose()
        {
            foreach (var action in _all) action.Dispose();
        }
    }
}
