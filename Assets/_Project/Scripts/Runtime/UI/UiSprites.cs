using UnityEngine;

namespace Specimen.Runtime.UI
{
    /// <summary>Sprites de marco generados por código (9-slice): no hacen falta texturas en el proyecto.</summary>
    public static class UiSprites
    {
        private static Sprite _frame, _slot, _fill;

        /// <summary>Marco con borde dorado doble y esquinas biseladas.</summary>
        public static Sprite Frame => _frame != null ? _frame : _frame = Build(32, 3, true);

        /// <summary>Hueco de inventario/acción con borde fino.</summary>
        public static Sprite Slot => _slot != null ? _slot : _slot = Build(16, 1, false);

        /// <summary>Blanco liso para barras y fondos tintados.</summary>
        public static Sprite Fill => _fill != null ? _fill : _fill = Build(4, 0, false);

        private static Sprite Build(int size, int border, bool bevel)
        {
            var texture = new Texture2D(size, size, TextureFormat.RGBA32, false) { filterMode = FilterMode.Point };
            for (int y = 0; y < size; y++)
                for (int x = 0; x < size; x++)
                    texture.SetPixel(x, y, Pixel(x, y, size, border, bevel));
            texture.Apply();
            float b = border + (bevel ? 3 : 1);
            return Sprite.Create(texture, new Rect(0, 0, size, size), new Vector2(0.5f, 0.5f), 100f, 0,
                SpriteMeshType.FullRect, new Vector4(b, b, b, b));
        }

        private static Color Pixel(int x, int y, int size, int border, bool bevel)
        {
            int edge = Mathf.Min(Mathf.Min(x, y), Mathf.Min(size - 1 - x, size - 1 - y));
            if (bevel && x + y < 2 || bevel && (size - 1 - x) + y < 2 || bevel && x + (size - 1 - y) < 2
                || bevel && (size - 1 - x) + (size - 1 - y) < 2)
                return Color.clear;
            if (border == 0) return Color.white;
            if (edge < border) return new Color(1f, 1f, 1f, 1f);
            if (bevel && edge == border + 1) return new Color(1f, 1f, 1f, 0.55f);
            return new Color(1f, 1f, 1f, 0f);
        }
    }
}
