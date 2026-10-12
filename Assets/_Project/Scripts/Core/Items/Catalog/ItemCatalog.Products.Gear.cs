using System.Collections.Generic;
using C = Specimen.Core.Items.ItemCategory;
using R = Specimen.Core.Items.Rarity;

namespace Specimen.Core.Items.Catalog
{
    /// <summary>Equipo y herramientas del recetario de oficios (§17.6), sinergias incluidas.</summary>
    public static partial class ItemCatalog
    {
        private static void AddProductGear(List<ItemDefinition> l)
        {
            Gear(l, "cuchillo_cobre", "Cuchillo de cobre", C.Weapon, R.Common, 40);
            Gear(l, "hacha_mano", "Hacha de mano", C.Weapon, R.Common, 45);
            Gear(l, "maza_hierro", "Maza de hierro", C.Weapon, R.Uncommon, 60);
            Gear(l, "espada_acero", "Espada de acero", C.Weapon, R.Rare, 80);
            Gear(l, "mandoble_crisol", "Mandoble de acero de crisol", C.Weapon, R.Epic, 120);
            Gear(l, "ballesta_ligera", "Ballesta ligera", C.Weapon, R.Uncommon, 60);
            Gear(l, "balista_portatil", "Balista portátil", C.Weapon, R.Epic, 110);
            Gear(l, "palo_madera", "Palo de madera", C.Weapon, R.Common, 30);
            Gear(l, "arco_corto", "Arco corto", C.Weapon, R.Uncommon, 55);
            Gear(l, "arco_tejo", "Arco de tejo", C.Weapon, R.Rare, 75);
            Gear(l, "lanza_fresno", "Lanza de fresno", C.Weapon, R.Rare, 70);
            Gear(l, "arco_largo_corazon", "Arco largo de corazón negro", C.Weapon, R.Epic, 110);
            Gear(l, "filo_silex", "Filo de sílex", C.Weapon, R.Common, 30);
            Gear(l, "piedra_atada", "Piedra atada", C.Weapon, R.Common, 35);
            Gear(l, "ballesta_repeticion", "Ballesta de repetición", C.Weapon, R.Rare, 80);
            Gear(l, "casco_hierro", "Casco de hierro", C.Armor, R.Uncommon, 60);
            Gear(l, "coraza_placas", "Coraza de placas", C.Armor, R.Rare, 90);
            Gear(l, "arnes_completo", "Arnés completo", C.Armor, R.Epic, 120);
            Gear(l, "capucha_piel", "Capucha de piel", C.Armor, R.Common, 40);
            Gear(l, "jubon_acolchado", "Jubón acolchado", C.Armor, R.Uncommon, 60);
            Gear(l, "capa_viaje", "Capa de viaje", C.Armor, R.Uncommon, 50);
            Gear(l, "brigantina", "Brigantina", C.Armor, R.Rare, 85);
            Gear(l, "capa_planeo", "Capa de planeo", C.Armor, R.Rare, 60);
            Gear(l, "gambeson_seda", "Gambesón de seda", C.Armor, R.Epic, 110);
            Gear(l, "escudo_tablas", "Escudo de tablas", C.Armor, R.Common, 50);
            Gear(l, "escudo_reforzado", "Escudo reforzado", C.Armor, R.Uncommon, 70);
            Gear(l, "capa_ignifuga", "Capa ignífuga", C.Armor, R.Rare, 80);
            Gear(l, "armadura_mixta", "Armadura mixta", C.Armor, R.Rare, 90);
            AddProductTools(l);
        }

        private static void AddProductTools(List<ItemDefinition> l)
        {
            Gear(l, "herradura_llave", "Herradura-llave", C.Tool, R.Common, 0, 5);
            Gear(l, "kit_afilado", "Kit de afilado fino", C.Tool, R.Rare, 0, 5);
            Gear(l, "bolsa_cintura", "Bolsa de cintura", C.Tool, R.Uncommon, 0);
            Gear(l, "correaje_ligero", "Correaje ligero", C.Tool, R.Rare, 0);
            Gear(l, "trampa_lazo", "Trampa de lazo", C.Tool, R.Common, 0, 5);
            Gear(l, "virotes", "Virotes", C.Tool, R.Common, 0, 20);
            Gear(l, "polea_carga", "Polea de carga", C.Tool, R.Uncommon, 0);
            Gear(l, "senuelo_piedra", "Señuelo de piedra", C.Tool, R.Common, 0, 5);
            Gear(l, "piedra_afilar", "Piedra de afilar", C.Tool, R.Common, 0, 5);
            Gear(l, "totem_aura", "Tótem de aura", C.Tool, R.Rare, 0);
            Gear(l, "cebo_bestias", "Cebo para bestias", C.Tool, R.Common, 0, 10);
            Gear(l, "golem_piedra", "Gólem de piedra", C.Tool, R.Epic, 0);
            Gear(l, "estatua_explosiva", "Estatua explosiva", C.Tool, R.Rare, 0, 5);
            Gear(l, "llave_torretas", "Llave de torretas", C.Tool, R.Rare, 0, 5);
        }
    }
}
