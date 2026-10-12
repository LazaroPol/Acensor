using System.Text;
using Specimen.Core.Items;
using Specimen.Core.Items.Catalog;
using Specimen.Runtime.UI;
using UnityEngine;

namespace Specimen.Runtime.Sandbox.Ui
{
    /// <summary>Presentación de objetos en la interfaz: «icono» (iniciales sobre color de categoría) y tooltip.</summary>
    public static class ItemView
    {
        public static string Glyph(string name)
        {
            var words = name.Split(' ');
            if (words.Length > 1 && words[1].Length > 2) return $"{char.ToUpper(words[0][0])}{char.ToUpper(words[1][0])}";
            return name.Length >= 2 ? name.Substring(0, 2) : name;
        }

        public static Color Tint(ItemCategory category)
        {
            switch (category)
            {
                case ItemCategory.Weapon: return new Color(0.55f, 0.2f, 0.17f);
                case ItemCategory.Armor: return new Color(0.25f, 0.33f, 0.5f);
                case ItemCategory.Tool: return new Color(0.45f, 0.33f, 0.2f);
                case ItemCategory.Consumable: return new Color(0.2f, 0.45f, 0.25f);
                default: return new Color(0.35f, 0.32f, 0.28f);
            }
        }

        public static string CategoryName(ItemCategory category)
        {
            switch (category)
            {
                case ItemCategory.Weapon: return "Arma";
                case ItemCategory.Armor: return "Armadura";
                case ItemCategory.Tool: return "Herramienta";
                case ItemCategory.Consumable: return "Consumible";
                default: return "Material";
            }
        }

        public static string RarityName(Rarity rarity)
        {
            switch (rarity)
            {
                case Rarity.Uncommon: return "Poco común";
                case Rarity.Rare: return "Raro";
                case Rarity.Epic: return "Épico";
                case Rarity.Legendary: return "Legendario";
                default: return "Común";
            }
        }

        public static string Colored(ItemDefinition item) =>
            $"<color=#{ColorUtility.ToHtmlStringRGB(UiTheme.Rarity(item.Rarity))}>{item.Name}</color>";

        public static void Show(UiSlot slot, ItemDefinition item, int count, bool dim = false) =>
            slot.Set(Glyph(item.Name), Tint(item.Category), UiTheme.Rarity(item.Rarity), count, dim);

        public static string Tooltip(ItemDefinition item, ItemInstance instance = null, int count = 1)
        {
            var text = new StringBuilder();
            text.Append($"<size=16><b>{Colored(item)}</b></size>\n");
            text.Append($"<color=#B0A890>{CategoryName(item.Category)} · {RarityName(item.Rarity)}</color>");
            if (count > 1) text.Append($"\nCantidad: {count}");
            if (instance != null && item.IsDegradable)
                text.Append($"\nDurabilidad {instance.Durability.Current}/{instance.Durability.Max}");
            if (ConsumableEffect.TryGet(item.Id, out var effect))
                text.Append($"\n<color=#5AE65A>Usar: {effect.Description}</color>\n<color=#B0A890>Clic derecho para usar.</color>");
            return text.ToString();
        }
    }
}
