using Specimen.Core.Items;
using Specimen.Core.Loot;
using UnityEngine;

namespace Specimen.Runtime.Sandbox
{
    /// <summary>Botín en el suelo: gira y se recoge al pasar cerca (va al inventario del contexto).</summary>
    public sealed class LootPickup : MonoBehaviour
    {
        private const float PickupRange = 1.3f;

        private LootDrop _drop;
        private SandboxContext _context;
        private Transform _player;

        public void Init(LootDrop drop, SandboxContext context, Transform player)
        {
            _drop = drop;
            _context = context;
            _player = player;
        }

        private void Update()
        {
            transform.Rotate(0f, 90f * Time.deltaTime, 0f);
            if (_player == null || _drop.Item == null) return;
            var offset = _player.position - transform.position;
            offset.y = 0f;
            if (offset.magnitude > PickupRange) return;
            int added = _context.Inventory.Add(ItemInstance.CreateNew(_drop.Item), _drop.Quantity);
            _context.Feed.Add(added > 0 ? $"+ {_drop.Item.Name} x{added} [{_drop.Item.Rarity}]" : "Inventario lleno.");
            Destroy(gameObject);
        }
    }
}
