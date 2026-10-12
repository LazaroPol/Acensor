using System;
using Specimen.Core.Combat;
using Specimen.Core.Progression;
using UnityEngine;

namespace Specimen.Runtime.Sandbox
{
    /// <summary>Sucesos de combate y progresión que pinta la interfaz (texto flotante, errores, marco de objetivo).</summary>
    public sealed class CombatEvents
    {
        public event Action<SandboxEnemy, DamageResult, HitSource> DamageDealt;
        public event Action<Vector3, DamageResult> DamageTaken;
        public event Action<Vector3, float> Healed;
        public event Action<int, XpSource> XpGained;
        public event Action<string> Error;

        public void Dealt(SandboxEnemy enemy, DamageResult result, HitSource source) => DamageDealt?.Invoke(enemy, result, source);

        public void Taken(Vector3 position, DamageResult result) => DamageTaken?.Invoke(position, result);

        public void Heal(Vector3 position, float amount) => Healed?.Invoke(position, amount);

        public void Xp(int amount, XpSource source) => XpGained?.Invoke(amount, source);

        /// <summary>Aviso rojo en pantalla, como «No tienes objetivo» o «Fuera de alcance» en WoW.</summary>
        public void Fail(string message) => Error?.Invoke(message);
    }
}
