using System.Collections.Generic;
using Specimen.Core.Common;

namespace Specimen.Core.World
{
    /// <summary>
    /// Despiece de una casa del poblado con las piezas modulares del pack medieval (módulos de 2 m):
    /// suelo de madera, muros con puerta centrada en la fachada (+Z), ventanas alternas y tejado de tejas.
    /// </summary>
    public static class HouseBlueprint
    {
        public const string Floor = "Floor_WoodDark";

        public static string RoofFor(int width, int depth) => $"Roof_RoundTiles_{width}x{depth}";

        public static string WallModel(HouseStyle style, string variant) =>
            (style == HouseStyle.Yeso ? "Wall_Plaster_" : "Wall_UnevenBrick_") + variant;

        public static IReadOnlyList<HousePiece> For(HouseSpot house)
        {
            Guard.NotNull(house, nameof(house));
            var pieces = new List<HousePiece>();
            float halfW = house.Width / 2f, halfD = house.Depth / 2f;
            int cols = (int)(house.Width / HouseSpot.Module), rows = (int)(house.Depth / HouseSpot.Module);
            for (int i = 0; i < cols; i++)
                for (int j = 0; j < rows; j++)
                    pieces.Add(new HousePiece(HousePieceKind.Suelo, Floor, -halfW + 1f + 2f * i, -halfD + 1f + 2f * j, 0f));
            for (int i = 0; i < cols; i++)
            {
                float x = -halfW + 1f + 2f * i;
                pieces.Add(Wall(house.Style, i == cols / 2 ? "Door_Round" : Window(i + 1), x, halfD, 0f));
                pieces.Add(Wall(house.Style, Window(i), x, -halfD, 180f));
            }
            for (int j = 0; j < rows; j++)
            {
                float z = -halfD + 1f + 2f * j;
                pieces.Add(Wall(house.Style, Window(j), -halfW, z, 270f));
                pieces.Add(Wall(house.Style, Window(j + 1), halfW, z, 90f));
            }
            pieces.Add(new HousePiece(HousePieceKind.Tejado, RoofFor(house.Width, house.Depth), 0f, 0f, 0f));
            return pieces;
        }

        private static string Window(int index) => index % 2 == 1 ? "Window_Wide_Round" : "Straight";

        private static HousePiece Wall(HouseStyle style, string variant, float x, float z, float yaw) =>
            new HousePiece(HousePieceKind.Muro, WallModel(style, variant), x, z, yaw);
    }
}
