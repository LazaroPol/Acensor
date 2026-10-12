using System.Collections.Generic;
using UnityEngine;

namespace Specimen.Runtime.Sandbox
{
    /// <summary>Registro de los últimos sucesos del combate para el HUD (daño, botín, kit, avisos).</summary>
    public sealed class CombatFeed
    {
        public const int Capacity = 7;
        private const float Lifetime = 8f;

        private readonly List<(string text, float time)> _lines = new List<(string, float)>();

        public void Add(string text)
        {
            _lines.Add((text, Time.time));
            if (_lines.Count > Capacity) _lines.RemoveAt(0);
            Debug.Log($"[Campo de pruebas] {text}");
        }

        public IEnumerable<string> Recent()
        {
            foreach (var (text, time) in _lines)
                if (Time.time - time < Lifetime) yield return text;
        }
    }
}
