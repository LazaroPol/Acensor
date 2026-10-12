using System.Collections.Generic;

namespace Specimen.Runtime.Sandbox
{
    /// <summary>Enemigos del Campo de pruebas (vivos y caídos) para que ataques y HUD los recorran sin buscar en la escena.</summary>
    public sealed class EnemyRegistry
    {
        private readonly List<SandboxEnemy> _all = new List<SandboxEnemy>();

        public IReadOnlyList<SandboxEnemy> All => _all;

        public void Add(SandboxEnemy enemy) => _all.Add(enemy);

        public IEnumerable<SandboxEnemy> Alive()
        {
            foreach (var enemy in _all)
                if (enemy != null && !enemy.IsDead) yield return enemy;
        }

        public int AliveCount()
        {
            int count = 0;
            foreach (var _ in Alive()) count++;
            return count;
        }
    }
}
