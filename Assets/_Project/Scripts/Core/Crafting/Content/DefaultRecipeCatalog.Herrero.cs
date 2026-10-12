namespace Specimen.Core.Crafting.Content
{
    /// <summary>Herrero · Fragua (§17.6).</summary>
    public static partial class DefaultRecipeCatalog
    {
        private static void AddHerrero(RecipeLadder r) => r
            .T1("lingote_cobre", "lingote_cobre", "mineral_cobre*2")
            .T1("clavos", "clavos*4", "lingote_cobre")
            .T1("esquirlas", "esquirlas_metal*3", "chatarra")
            .T1("cuchillo_cobre", "cuchillo_cobre", "lingote_cobre*2 mango_madera")
            .T1("hacha_mano", "hacha_mano", "lingote_cobre*2 #Madera")
            .T1("reparar_chatarra", "parche_metal", "chatarra*2 clavos")
            .Add(2, "lingote", "lingote-hierro", "mineral-hierro*2", part: "crisol")
            .Add(2, "remaches", "remaches*4", "lingote-hierro", unlock: "pergamino.remaches")
            .Add(2, "espada", "espada-hierro", "lingote-hierro*3 madera", part: "muela_afilar")
            .Add(2, "maza_hierro", "maza_hierro", "lingote-hierro*3 mango_madera", part: "fuelle_cuero")
            .Add(2, "casco_hierro", "casco_hierro", "lingote-hierro*2 correa_cuero remaches*2", part: "fuelle_cuero")
            .Add(2, "placa", "placa-reforzada", "lingote-hierro*2 cuero", part: "fuelle_cuero")
            .Add(2, "contrapeso_plomo", "contrapeso_plomo", "chatarra*3", unlock: "pergamino.contrapeso_plomo")
            .Add(2, "herradura_llave", "herradura_llave", "lingote-hierro", unlock: "pergamino.herradura_llave")
            .Add(3, "lingote_acero", "lingote_acero", "lingote-hierro*2 carbon_vegetal", "crisol", "plano.lingote_acero")
            .Add(3, "espada_acero", "espada_acero", "lingote_acero*3 mango_madera correa_cuero",
                unlock: "tratado.forja_acero")
            .Add(3, "coraza_placas", "coraza_placas", "lingote_acero*4 remaches*4 correa_cuero*2",
                "yunque_acero", "plano.coraza_placas")
            .Add(3, "afilado_fino", "kit_afilado", "lingote_acero aceite_afilar", part: "muela_afilar")
            .Add(3, "cerrojo_acero", "cerrojo_acero", "lingote_acero remaches*2", unlock: "pergamino.cerrojo_acero")
            .Add(4, "acero_crisol", "acero_crisol", "lingote_acero*2 polvo_estrella hulla", "crisol", "libro.forjador")
            .Add(4, "mandoble_crisol", "mandoble_crisol", "acero_crisol*4 madera_corazon_negro piel_bestia_mayor",
                "yunque_acero", "tratado.forja_maestra")
            .Add(4, "arnes_completo", "arnes_completo", "acero_crisol*5 cuero_endurecido*3 remaches*6",
                "yunque_acero", "plano.arnes_completo");
    }
}
