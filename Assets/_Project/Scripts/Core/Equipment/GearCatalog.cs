using System.Collections.Generic;
using Specimen.Core.Stats;
using G = Specimen.Core.Items.Catalog.GearIds;
using I = Specimen.Core.Items.Catalog.ItemIds;
using S = Specimen.Core.Equipment.EquipSlot;
using W = Specimen.Core.Equipment.WeaponKind;

namespace Specimen.Core.Equipment
{
    /// <summary>
    /// Estadísticas del equipo, con velocidades típicas de WoW (daga 1,8 · espada 2,6 · hacha 2,7 · maza 2,8 ·
    /// bastón 3,0 · lanza 3,4 · mandoble 3,6 · varita 2,0) y armadura según el peso de la pieza.
    /// </summary>
    public static class GearCatalog
    {
        private static readonly Dictionary<string, GearPiece> Pieces = Build();

        public static IReadOnlyCollection<GearPiece> All => Pieces.Values;

        public static bool TryGet(string itemId, out GearPiece piece) => Pieces.TryGetValue(itemId ?? string.Empty, out piece);

        public static bool IsGear(string itemId) => Pieces.ContainsKey(itemId ?? string.Empty);

        private static Dictionary<string, GearPiece> Build()
        {
            var list = new List<GearPiece>
            {
                Weapon(G.MazaRecluta, W.Maza, 6, 12, 2.8f),
                Weapon(G.LanzaVigia, W.Lanza, 10, 17, 3.4f),
                Weapon(G.BastonAprendiz, W.Baston, 6, 11, 3.0f),
                Weapon(G.CayadoCeniza, W.Baston, 7, 12, 3.0f),
                Weapon(G.DagaGastada, W.Daga, 3, 7, 1.8f),
                Weapon(G.VaritaChispas, W.Varita, 6, 11, 2.0f),
                Weapon(G.DagaDiablillo, W.Daga, 6, 10, 1.7f, (StatType.CritChance, 0.02f)),
                Weapon(I.EspadaHierro, W.Espada, 12, 22, 2.6f, (StatType.Damage, 2f)),
                Weapon(G.HachaPuglin, W.Hacha, 14, 25, 2.7f, (StatType.MaxHealth, 10f)),
                Weapon(G.MandobleGuardian, W.Mandoble, 32, 49, 3.6f, (StatType.Damage, 5f), (StatType.CritChance, 0.02f)),
                Armor(G.CotaAnillas, S.Pecho, 95),
                Armor(G.JubonCuero, S.Pecho, 48),
                Armor(G.TunicaLino, S.Pecho, 22, (StatType.MaxClassResource, 10f)),
                Armor(G.PantalonesTela, S.Piernas, 14),
                Armor(G.GuantesChamuscados, S.Manos, 16, (StatType.Damage, 1f)),
                Armor(G.YelmoAbollado, S.Cabeza, 52, (StatType.MaxHealth, 8f)),
                Armor(G.BotasCuero, S.Pies, 30, (StatType.MoveSpeed, 0.3f)),
                Armor(I.PlacaReforzada, S.Pecho, 150, (StatType.MaxHealth, 20f)),
                Armor(I.CapaAcolchada, S.Espalda, 18, (StatType.MaxHealth, 5f)),
                Armor(G.CorazaGuardian, S.Pecho, 230, (StatType.MaxHealth, 40f), (StatType.Damage, 2f))
            };
            var result = new Dictionary<string, GearPiece>();
            foreach (var piece in list) result.Add(piece.ItemId, piece);
            return result;
        }

        private static GearPiece Weapon(string id, W kind, float min, float max, float speed,
            params (StatType stat, float value)[] bonuses)
            => new GearPiece(id, S.Arma, 0f, new WeaponSpec(kind, min, max, speed), Bonus(bonuses));

        private static GearPiece Armor(string id, S slot, float armor, params (StatType stat, float value)[] bonuses)
            => new GearPiece(id, slot, armor, null, Bonus(bonuses));

        private static Dictionary<StatType, float> Bonus((StatType stat, float value)[] bonuses)
        {
            var map = new Dictionary<StatType, float>();
            foreach (var (stat, value) in bonuses) map[stat] = value;
            return map;
        }
    }
}
