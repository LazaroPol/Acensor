using Specimen.Core.Bestiary;
using Specimen.Core.Combat.Enemies;
using Specimen.Core.Sandbox;
using UnityEngine;

namespace Specimen.Runtime.Sandbox
{
    /// <summary>Criaturas del Campo de pruebas, repartidas por los campamentos del poblado.</summary>
    public sealed partial class SandboxWorld
    {
        /// <summary>Bestiario en los campamentos del poblado: cada criatura común con dos compañeros; el jefe, solo.</summary>
        public void SpawnBestiary(SandboxPlayer player)
        {
            foreach (var camp in _context.Village.Camps)
            {
                var entry = DefaultBestiary.Find(camp.Id);
                int count = entry.Id.StartsWith("guardian") ? 1 : 2;
                for (int i = 0; i < count; i++)
                {
                    float angle = i * 2.4f;
                    var offset = i == 0 ? Vector3.zero : new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle)) * 2.5f;
                    SpawnEnemy(entry, new Vector3(camp.X, 0f, camp.Z) + offset, camp.Yaw, player);
                }
            }
        }

        private void SpawnEnemy(BestiaryEntry entry, Vector3 position, float yaw, SandboxPlayer player)
        {
            var prefab = _context.Art != null ? _context.Art.EnemyPrefab(entry.Id) : null;
            var rotation = Quaternion.Euler(0f, yaw, 0f);
            var go = prefab != null ? UnityEngine.Object.Instantiate(prefab, position, rotation) : Greybox(entry, position, rotation);
            go.name = entry.DisplayName;
            var enemy = go.AddComponent<SandboxEnemy>();
            var core = _factory.Create(entry.Archetype, new EnemyDepth(1, SandboxLoot.RingOf(entry)));
            enemy.Init(entry, core, player, dead => OnEnemyDeath(dead, player.transform),
                _context.Art != null ? _context.Art.monstruo : null);
            enemy.Respawned += e => _context.Feed.Add($"{e.DisplayName} ha reaparecido.");
            _context.Enemies.Add(enemy);
        }

        private static GameObject Greybox(BestiaryEntry entry, Vector3 position, Quaternion rotation)
        {
            var root = new GameObject();
            root.transform.SetPositionAndRotation(position, rotation);
            VisualFactory.Character(null, root.transform, new Color(0.85f, 0.2f, 0.2f), 1.5f * entry.Scale);
            return root;
        }
    }
}
