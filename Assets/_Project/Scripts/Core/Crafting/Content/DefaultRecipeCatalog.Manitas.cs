namespace Specimen.Core.Crafting.Content
{
    /// <summary>Carpintero · Banco de carpintero (§17.6). Fabrica también piezas de mejora de estación.</summary>
    public static partial class DefaultRecipeCatalog
    {
        private static void AddManitas(RecipeLadder r) => r
            .T1("palo", "palo_madera", "madera*2")
            .T1("mango", "mango_madera*2", "madera")
            .T1("lena", "lena*3", "rama_seca*3")
            .T1("carbon", "carbon_vegetal", "madera*3")
            .T1("punta_hueso", "punta_hueso*2", "hueso")
            .T1("escudo_tablas", "escudo_tablas", "tablon_roble*2 clavos*4")
            .Add(2, "arco_corto", "arco_corto", "madera_fresno*2 cuerda", part: "tornillo_banco")
            .Add(2, "mango_fresno", "mango_fresno", "madera_fresno", part: "tornillo_banco")
            .Add(2, "escudo_reforzado", "escudo_reforzado", "tablon_roble*3 lingote-hierro remaches*4",
                part: "tornillo_banco")
            .Add(2, "ganzua", "ganzua*2", "chatarra", "juego_limas", "pergamino.ganzuas")
            .Add(2, "reparacion", "kit-reparacion", "chatarra*2 madera*2", part: "juego_limas")
            .Add(3, "arco_tejo", "arco_tejo", "madera_tejo*2 cuerda*2 resina", "tornillo_banco", "plano.arco_tejo")
            .Add(3, "lanza_fresno", "lanza_fresno", "mango_fresno lingote_acero", unlock: "pergamino.lanza_fresno")
            .Add(3, "pieza_bastidor", "pieza.bastidor_telar", "tablon_roble*2 cuerda*4 clavos*4",
                unlock: "plano.mejoras_estacion")
            .Add(3, "pieza_parrilla", "pieza.parrilla", "lingote-hierro*2 clavos*2", unlock: "plano.mejoras_estacion")
            .Add(3, "pieza_mazo", "pieza.mazo_cantero", "tablon_roble lingote-hierro*2", unlock: "plano.mejoras_estacion")
            .Add(4, "arco_largo", "arco_largo_corazon", "madera_corazon_negro*2 seda resina*2",
                "tornillo_banco", "libro.arquero");
    }
}
