namespace Specimen.Core.Crafting.Content
{
    /// <summary>Sastre y curtidor · Taller de sastre (§17.6). La capa ignífuga es de sinergia.</summary>
    public static partial class DefaultRecipeCatalog
    {
        private static void AddSastre(RecipeLadder r) => r
            .T1("cuerda", "cuerda*2", "fibra_vegetal*3")
            .T1("correa", "correa_cuero*2", "piel_cruda*2")
            .T1("tiras", "tiras_cuero*4", "piel_cruda")
            .T1("venda", "venda*2", "tela*2")
            .T1("capucha", "capucha_piel", "piel_cruda*2 cuerda")
            .Add(2, "cuero_curtido", "cuero", "piel_cruda*2 sal", part: "tina_curtido")
            .Add(2, "jubon_acolchado", "jubon_acolchado", "lino*3 lana*2 cuerda", part: "bastidor_telar")
            .Add(2, "forro_acolchado", "forro_acolchado", "lana*2 lino", part: "bastidor_telar")
            .Add(2, "bolsa_cintura", "bolsa_cintura", "cuero*2 correa_cuero", unlock: "pergamino.bolsa_cintura")
            .Add(2, "capa_viaje", "capa_viaje", "lana*3 correa_cuero", part: "bastidor_telar")
            .Add(2, "capa", "capa-acolchada", "tela*4 cuero", part: "bastidor_telar")
            .Add(2, "mochila", "mochila", "cuero*3 tela*2", unlock: "pergamino.mochila")
            .Add(3, "cuero_endurecido", "cuero_endurecido", "cuero*2 resina", "tina_curtido", "plano.cuero_endurecido")
            .Add(3, "brigantina", "brigantina", "cuero_endurecido*3 remaches*6 pano_encerado",
                "tijeras_maestro", "plano.brigantina")
            .Add(3, "correaje_ligero", "correaje_ligero", "correa_cuero*4 remaches*2",
                unlock: "pergamino.correaje_ligero")
            .Add(3, "capa_planeo", "capa_planeo", "pano_encerado*3 #Madera*2@2 cuerda*2", unlock: "tratado.cueros")
            .Add(4, "gambeson_seda", "gambeson_seda", "seda*4 forro_acolchado*2 lana*2",
                "tijeras_maestro", "libro.maestro_sastre");
    }
}
