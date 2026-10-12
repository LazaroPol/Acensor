using System.Collections.Generic;
using Specimen.Core.Items.Catalog;
using Specimen.Core.Professions;
using I = Specimen.Core.Items.Catalog.ItemIds;

namespace Specimen.Core.Crafting
{
    /// <summary>Recetas de cada profesión con los materiales del poblado (recolección) y del bestiario (botín).</summary>
    public static class DefaultRecipes
    {
        public static RecipeBook CreateBook() => new RecipeBook(Create());

        public static IReadOnlyList<Recipe> Create() => new[]
        {
            R("herrero.lingote", ProfessionId.Herrero, I.Lingote, 1, (I.MineralHierro, 2)),
            R("herrero.espada", ProfessionId.Herrero, I.EspadaHierro, 1, (I.Lingote, 3), (I.Madera, 1)),
            R("herrero.placa", ProfessionId.Herrero, I.PlacaReforzada, 1, (I.Lingote, 2), (I.Cuero, 1)),
            R("sastre.venda", ProfessionId.Sastre, I.Venda, 2, (I.Tela, 2)),
            R("sastre.capa", ProfessionId.Sastre, I.CapaAcolchada, 1, (I.Tela, 4), (I.Cuero, 1)),
            R("sastre.mochila", ProfessionId.Sastre, I.Mochila, 1, (I.Cuero, 3), (I.Tela, 2)),
            R("cocinero.estofado", ProfessionId.Cocinero, I.Estofado, 1, (I.Carne, 2), (I.Hierba, 1)),
            R("cocinero.brocheta", ProfessionId.Cocinero, I.Brocheta, 2, (I.Carne, 1), (I.Madera, 1)),
            R("quimico.vida", ProfessionId.Quimico, I.PocionVida, 1, (I.Hierba, 3)),
            R("quimico.mana", ProfessionId.Quimico, I.PocionMana, 1, (I.Hierba, 2), (I.Cristal, 1)),
            R("quimico.bomba", ProfessionId.Quimico, I.BombaCeniza, 1, (I.Chatarra, 2), (I.Hierba, 1)),
            R("ingeniero.trampa", ProfessionId.Ingeniero, I.Trampa, 1, (I.Chatarra, 3), (I.Madera, 1)),
            R("ingeniero.torreta", ProfessionId.Ingeniero, I.TorretaPortatil, 1, (I.Chatarra, 4), (I.Nucleo, 1)),
            R("manitas.ganzua", ProfessionId.Manitas, I.Ganzua, 2, (I.Chatarra, 1)),
            R("manitas.reparacion", ProfessionId.Manitas, I.KitReparacion, 1, (I.Chatarra, 2), (I.Madera, 2)),
            R("escultor.totem", ProfessionId.Escultor, I.TotemPiedra, 1, (I.Piedra, 4)),
            R("escultor.idolo", ProfessionId.Escultor, I.Idolo, 1, (I.Piedra, 2), (I.Reliquia, 1))
        };

        private static Recipe R(string id, ProfessionId profession, string output, int quantity,
            params (string item, int qty)[] ingredients)
        {
            var list = new List<RecipeIngredient>();
            foreach (var (item, qty) in ingredients) list.Add(new RecipeIngredient(ItemCatalog.Get(item), qty));
            return new Recipe(id, profession, list, ItemCatalog.Get(output), quantity);
        }
    }
}
