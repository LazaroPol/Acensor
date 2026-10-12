namespace Specimen.Core.Crafting.Content
{
    /// <summary>Cocinero · Hogar de cocina (§17.6). El festín alquímico es de sinergia.</summary>
    public static partial class DefaultRecipeCatalog
    {
        private static void AddCocinero(RecipeLadder r) => r
            .T1("carne_asada", "carne_asada", "carne")
            .T1("pescado_brasa", "pescado_brasa", "pescado")
            .T1("pan", "pan_trigo*2", "harina*2")
            .T1("gachas", "gachas", "trigo*2")
            .T1("cebo", "cebo_bestias*2", "carne raiz_amarga")
            .T1("brocheta", "brocheta*2", "carne madera")
            .Add(2, "guiso_raices", "guiso_raices", "raiz_amarga*2 hierba_comun", part: "olla_hierro")
            .Add(2, "caldo_huesos", "caldo_huesos", "hueso*2 sal", part: "olla_hierro")
            .Add(2, "asado_especiado", "asado_especiado", "carne*2 sal", part: "parrilla")
            .Add(2, "conserva_miel", "conserva_miel", "miel*2 manzana*2", part: "cazo_cobre")
            .Add(2, "racion_marcha", "racion_marcha", "carne harina sal", unlock: "pergamino.racion_marcha")
            .Add(2, "estofado", "estofado", "carne*2 hierba-ceniza", part: "olla_hierro")
            .Add(3, "estofado_cazador", "estofado_cazador", "carne*3 especias_sur raiz_amarga",
                "olla_hierro", "tratado.guisos")
            .Add(3, "pastel_carne", "pastel_carne", "harina*2 carne*2 manteca", unlock: "plano.pastel_carne")
            .Add(3, "confitura_sauco", "confitura_sauco", "flor_sauco*3 miel", "cazo_cobre", "pergamino.confitura_sauco")
            .Add(4, "banquete", "banquete_senor", "trufa_negra especias_sur*2 carne*4",
                "olla_hierro", "libro.casa_noble");
    }
}
