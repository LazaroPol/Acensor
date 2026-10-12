using System;
using Specimen.Core.Sandbox;

namespace Specimen.Runtime.Sandbox
{
    /// <summary>Reaparición del bestiario: tras morir, la criatura vuelve a su sitio con la vida llena.</summary>
    public sealed partial class SandboxEnemy
    {
        private RespawnTimer _respawn;

        /// <summary>Se dispara cuando la criatura reaparece (el registro de combate lo anuncia).</summary>
        public event Action<SandboxEnemy> Respawned;

        /// <summary>Segundos que faltan para reaparecer (0 si está viva).</summary>
        public float RespawnRemaining => _respawn != null && _respawn.IsRunning ? _respawn.Remaining : 0f;

        private void TickRespawn(float deltaSeconds)
        {
            if (!_respawn.Tick(deltaSeconds)) return;
            ResetToHome();
            Respawned?.Invoke(this);
        }
    }
}
