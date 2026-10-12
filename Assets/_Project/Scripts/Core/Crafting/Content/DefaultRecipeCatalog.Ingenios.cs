namespace Specimen.Core.Crafting.Content
{
    /// <summary>Maestro de ingenios · Taller de ingenios y Cantero · Banco de cantero (§17.6).</summary>
    public static partial class DefaultRecipeCatalog
    {
        private static void AddIngeniero(RecipeLadder r) => r
            .T1("engranaje", "engranaje_madera*2", "madera*2")
            .T1("trampa_lazo", "trampa_lazo", "cuerda*2 rama_seca")
            .T1("virotes", "virotes*10", "#Madera astilla_hueso*2")
            .Add(2, "ballesta", "ballesta_ligera", "engranaje_madera*2 madera_fresno*2 cuerda*2", part: "torno_pedal")
            .Add(2, "trampa", "trampa", "chatarra*3 madera", unlock: "pergamino.trampa_cepo")
            .Add(2, "polea", "polea_carga", "engranaje_madera*2 cuerda*3 clavos*2", part: "torno_pedal")
            .Add(3, "torreta", "torreta-portatil", "chatarra*4 nucleo", "compas_maestro", "plano.torreta_virotes")
            .Add(3, "mecanismo", "mecanismo_apertura", "engranaje_madera*3 lingote_cobre*2 remaches*2",
                unlock: "tratado.ingenios")
            .Add(4, "balista", "balista_portatil", "mecanismo_apertura*2 lingote_acero*3 madera_tejo*2",
                "torno_pedal", "libro.ingenios");

        private static void AddEscultor(RecipeLadder r) => r
            .T1("filo_silex", "filo_silex", "silex*2 #Madera")
            .T1("piedra_atada", "piedra_atada", "piedra cuerda rama_seca")
            .T1("piedra_labrada", "piedra_labrada", "piedra*2")
            .T1("mortero_cal", "mortero_cal*2", "piedra*2 arcilla")
            .T1("senuelo", "senuelo_piedra", "piedra_labrada")
            .Add(2, "bloque_granito", "bloque_granito", "piedra*4", part: "mazo_cantero")
            .Add(2, "totem", "totem-piedra", "piedra*4", unlock: "pergamino.totem_menor")
            .Add(2, "piedra_afilar", "piedra_afilar*2", "silex arena_fina", part: "mazo_cantero")
            .Add(3, "totem_aura", "totem_aura", "marmol*2 cristal", "cincel_acero", "plano.totem_aura")
            .Add(3, "idolo", "idolo", "piedra*2 reliquia", "cincel_acero", "tratado.estatuaria")
            .Add(4, "gema_tallada", "gema_tallada", "obsidiana polvo_estrella", "cincel_acero", "libro.lapidario");
    }
}
