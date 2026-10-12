using System.Collections.Generic;
using I = Specimen.Core.Items.Catalog.ItemIds;

namespace Specimen.Core.Items.Catalog
{
    /// <summary>Qué hace un consumible al usarlo: vida y recurso de clase que devuelve, y descripción para el tooltip.</summary>
    public sealed class ConsumableEffect
    {
        private static readonly Dictionary<string, ConsumableEffect> Effects = new Dictionary<string, ConsumableEffect>
        {
            [I.Venda] = new ConsumableEffect(25f, 0f, "Restaura 25 de vida."),
            [I.PocionVida] = new ConsumableEffect(70f, 0f, "Restaura 70 de vida."),
            [I.PocionMana] = new ConsumableEffect(0f, 60f, "Restaura 60 de recurso de clase."),
            [I.Estofado] = new ConsumableEffect(45f, 20f, "Restaura 45 de vida y 20 de recurso."),
            [I.Brocheta] = new ConsumableEffect(30f, 10f, "Restaura 30 de vida y 10 de recurso.")
        };

        public ConsumableEffect(float health, float resource, string description)
        {
            Health = health;
            Resource = resource;
            Description = description;
        }

        public float Health { get; }
        public float Resource { get; }
        public string Description { get; }

        public static bool TryGet(string itemId, out ConsumableEffect effect)
            => Effects.TryGetValue(itemId ?? string.Empty, out effect);
    }
}
