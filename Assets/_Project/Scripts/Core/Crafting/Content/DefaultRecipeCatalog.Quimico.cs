namespace Specimen.Core.Crafting.Content
{
    /// <summary>Alquimista · Mesa de alquimia (§17.6).</summary>
    public static partial class DefaultRecipeCatalog
    {
        private static void AddQuimico(RecipeLadder r) => r
            .T1("unguento", "unguento_hierbas", "hierba_comun*2 manteca")
            .T1("polvo_humo", "polvo_humo*2", "hierba-ceniza carbon_vegetal")
            .T1("antidoto", "antidoto", "raiz_amarga*2 hierba_comun")
            .Add(2, "vida", "pocion-vida", "hierba-ceniza*3", part: "alambique")
            .Add(2, "aguante", "pocion_aguante", "hierba_comun*2 miel", part: "alambique")
            .Add(2, "aceite_afilar", "aceite_afilar*2", "resina #Mineral", part: "mortero_piedra")
            .Add(2, "resina", "resina*2", "#Madera*2", part: "alambique")
            .Add(2, "vision_nocturna", "tonico_vision", "musgo_cueva*2 flor_sauco", unlock: "pergamino.vision_nocturna")
            .Add(2, "mana", "pocion-mana", "hierba-ceniza*2 cristal", part: "alambique")
            .Add(2, "bomba", "bomba-ceniza", "chatarra*2 hierba-ceniza", part: "retorta_vidrio")
            .Add(3, "acido", "acido_disolvente", "azufre*2 salitre", "retorta_vidrio", "plano.acido_disolvente")
            .Add(3, "bomba_azufre", "bomba_azufre", "azufre*2 salitre*2 vidrio_soplado",
                "retorta_vidrio", "tratado.alquimia")
            .Add(3, "resina_ignea", "resina_ignea", "resina*2 azufre", unlock: "pergamino.resina_ignea")
            .Add(3, "elixir_vigor", "elixir_vigor", "belladona miel*2 hierba_comun*2", "alambique", "tratado.alquimia")
            .Add(4, "panacea", "panacea", "mandragora*2 belladona vidrio_cuarzo", "alambique", "libro.codice_alquimista")
            .Add(4, "humo_cegador", "humo_cegador", "polvo_humo*3 salitre*2 vidrio_soplado",
                "retorta_vidrio", "plano.humo_cegador");
    }
}
