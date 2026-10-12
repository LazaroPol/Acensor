using UnityEngine;

namespace Specimen.Runtime.UI
{
    /// <summary>Paleta y tipografía de la interfaz (estilo MMO clásico: fondo oscuro, marcos dorados, texto pergamino).</summary>
    public static class UiTheme
    {
        public static readonly Color Background = new Color(0.07f, 0.06f, 0.05f, 0.94f);
        public static readonly Color Panel = new Color(0.13f, 0.11f, 0.09f, 0.96f);
        public static readonly Color Slot = new Color(0.05f, 0.05f, 0.05f, 0.9f);
        public static readonly Color Gold = new Color(0.86f, 0.68f, 0.32f);
        public static readonly Color GoldDim = new Color(0.5f, 0.4f, 0.22f);
        public static readonly Color Text = new Color(0.96f, 0.91f, 0.8f);
        public static readonly Color TextDim = new Color(0.62f, 0.58f, 0.5f);
        public static readonly Color Good = new Color(0.35f, 0.9f, 0.35f);
        public static readonly Color Bad = new Color(0.95f, 0.3f, 0.25f);
        public static readonly Color Health = new Color(0.78f, 0.15f, 0.12f);
        public static readonly Color Resource = new Color(0.2f, 0.42f, 0.95f);
        public static readonly Color Stamina = new Color(0.9f, 0.75f, 0.2f);
        public static readonly Color Xp = new Color(0.62f, 0.25f, 0.85f);

        private static Font _font;

        public static Font Font => _font != null ? _font : _font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");

        public static Color Rarity(Specimen.Core.Items.Rarity rarity)
        {
            switch (rarity)
            {
                case Specimen.Core.Items.Rarity.Uncommon: return new Color(0.12f, 1f, 0f);
                case Specimen.Core.Items.Rarity.Rare: return new Color(0f, 0.44f, 0.87f);
                case Specimen.Core.Items.Rarity.Epic: return new Color(0.64f, 0.21f, 0.93f);
                case Specimen.Core.Items.Rarity.Legendary: return new Color(1f, 0.5f, 0f);
                default: return new Color(0.85f, 0.85f, 0.85f);
            }
        }
    }
}
