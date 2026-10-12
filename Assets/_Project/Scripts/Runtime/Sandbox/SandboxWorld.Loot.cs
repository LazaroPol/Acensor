using Specimen.Core.Loot;
using Specimen.Core.Progression;
using Specimen.Core.Sandbox;
using Specimen.Core.Visuals;
using Specimen.Runtime.Visuals;
using UnityEngine;

namespace Specimen.Runtime.Sandbox
{
    /// <summary>Botín del Campo de pruebas: al morir una criatura se tira su tabla y aparece en el suelo.</summary>
    public sealed partial class SandboxWorld
    {
        private void OnEnemyDeath(SandboxEnemy enemy, Transform player)
        {
            _context.Feed.Add($"{enemy.DisplayName} derrotado. Reaparece en {enemy.RespawnRemaining:0} s.");
            _context.GrantXp(XpRewards.ForKill(enemy.Entry, _context.Hero.Level, enemy.Level), enemy.DisplayName,
                XpSource.Combate);
            var drops = SandboxLoot.DropsFor(enemy.Entry, _context.Random);
            for (int i = 0; i < drops.Count; i++) SpawnLoot(drops[i], enemy.transform.position + Offset(i), player);
        }

        private void SpawnLoot(LootDrop drop, Vector3 position, Transform player)
        {
            var resolution = _context.Visuals.Resolve(VisualId.Loot);
            var go = resolution.HasAsset
                ? UnityEngine.Object.Instantiate(resolution.Asset, position, Quaternion.identity)
                : GreyboxFactory.Create(VisualId.Loot, resolution.Greybox, null);
            go.transform.position = position;
            foreach (var collider in go.GetComponentsInChildren<Collider>()) UnityEngine.Object.Destroy(collider);
            go.AddComponent<LootPickup>().Init(drop, _context, player);
        }

        private static Vector3 Offset(int i) => new Vector3(Mathf.Cos(i * 2.1f), 0.2f, Mathf.Sin(i * 2.1f)) * 0.8f;
    }
}
