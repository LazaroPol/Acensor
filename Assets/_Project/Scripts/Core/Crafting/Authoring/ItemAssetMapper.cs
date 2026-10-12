using Specimen.Core.Common;
using Specimen.Core.Items;

namespace Specimen.Core.Crafting.Authoring
{
    /// <summary>Convierte <see cref="ItemData"/> ⇄ <see cref="ItemDefinition"/>.</summary>
    public static class ItemAssetMapper
    {
        public static ItemDefinition ToDefinition(ItemData data)
        {
            Guard.NotNull(data, nameof(data));
            return new ItemDefinition(data.id, data.name ?? string.Empty, data.category, data.rarity,
                data.baseDurability, data.maxStack, data.tier, data.weight, data.tags, data.iconKey);
        }

        public static ItemData FromDefinition(ItemDefinition item)
        {
            Guard.NotNull(item, nameof(item));
            return new ItemData
            {
                id = item.Id, name = item.Name, category = item.Category, rarity = item.Rarity,
                baseDurability = item.BaseDurability, maxStack = item.MaxStack, tier = item.Tier,
                weight = item.Weight, tags = item.Tags, iconKey = item.IconKey == item.Id ? null : item.IconKey
            };
        }
    }
}
