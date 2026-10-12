using System.Collections.Generic;
using Specimen.Core.Crafting.Knowledge;
using H = Specimen.Core.Professions.ProfessionId;

namespace Specimen.Core.Crafting.Content
{
    /// <summary>Contenido de la batería de conocimientos (§17.6): qué enseña cada uno.</summary>
    public static partial class DefaultKnowledge
    {
        private static void AddScrolls(List<KnowledgeDefinition> l)
        {
            Scroll(l, H.Herrero, "remaches", "remaches");
            Scroll(l, H.Herrero, "contrapeso_plomo", "contrapeso_plomo");
            Scroll(l, H.Herrero, "herradura_llave", "herradura_llave");
            Scroll(l, H.Herrero, "cerrojo_acero", "cerrojo_acero");
            Scroll(l, H.Sastre, "bolsa_cintura", "bolsa_cintura");
            Scroll(l, H.Sastre, "mochila", "mochila");
            Scroll(l, H.Sastre, "correaje_ligero", "correaje_ligero");
            Scroll(l, H.Cocinero, "racion_marcha", "racion_marcha");
            Scroll(l, H.Cocinero, "confitura_sauco", "confitura_sauco");
            Scroll(l, H.Quimico, "vision_nocturna", "vision_nocturna");
            Scroll(l, H.Quimico, "resina_ignea", "resina_ignea");
            Scroll(l, H.Ingeniero, "trampa_cepo", "trampa");
            Scroll(l, H.Manitas, "ganzuas", "ganzua");
            Scroll(l, H.Manitas, "lanza_fresno", "lanza_fresno");
            Scroll(l, H.Escultor, "totem_menor", "totem");
        }

        private static void AddBlueprints(List<KnowledgeDefinition> l)
        {
            Plan(l, H.Herrero, "lingote_acero", "lingote_acero");
            Plan(l, H.Herrero, "coraza_placas", "coraza_placas");
            Plan(l, H.Herrero, "arnes_completo", "arnes_completo");
            Plan(l, H.Sastre, "cuero_endurecido", "cuero_endurecido");
            Plan(l, H.Sastre, "brigantina", "brigantina");
            Plan(l, H.Cocinero, "pastel_carne", "pastel_carne");
            Plan(l, H.Quimico, "acido_disolvente", "acido");
            Plan(l, H.Quimico, "humo_cegador", "humo_cegador");
            Plan(l, H.Ingeniero, "torreta_virotes", "torreta");
            Plan(l, H.Manitas, "arco_tejo", "arco_tejo");
            Plan(l, H.Manitas, "mejoras_estacion", "pieza_bastidor", "pieza_parrilla", "pieza_mazo");
            Plan(l, H.Escultor, "totem_aura", "totem_aura");
        }

        private static void AddTreatises(List<KnowledgeDefinition> l)
        {
            Treatise(l, H.Herrero, "forja_acero", "espada_acero");
            Treatise(l, H.Herrero, "forja_maestra", "mandoble_crisol");
            Treatise(l, H.Sastre, "cueros", "capa_planeo");
            Treatise(l, H.Cocinero, "guisos", "estofado_cazador");
            Treatise(l, H.Quimico, "alquimia", "bomba_azufre", "elixir_vigor");
            Treatise(l, H.Ingeniero, "ingenios", "mecanismo");
            Treatise(l, H.Escultor, "estatuaria", "idolo");
        }

        private static void AddBooks(List<KnowledgeDefinition> l)
        {
            Book(l, H.Herrero, "forjador");
            Book(l, H.Sastre, "maestro_sastre");
            Book(l, H.Cocinero, "casa_noble");
            Book(l, H.Quimico, "codice_alquimista");
            Book(l, H.Ingeniero, "ingenios");
            Book(l, H.Manitas, "arquero");
            Book(l, H.Escultor, "lapidario");
        }
    }
}
