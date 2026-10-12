using Specimen.Core.Crafting.Play;
using Specimen.Core.Crafting.Save;
using UnityEngine;

namespace Specimen.Runtime.Sandbox
{
    /// <summary>
    /// Oficios del Campo de pruebas (GDD §17.7): leer conocimientos de la bolsa, mods de objetos, botín de
    /// oficio y guardado automático (carga al entrar; guarda al salir y al cerrar una estación).
    /// </summary>
    public sealed partial class SandboxContext
    {
        private readonly CraftingSave _save = new CraftingSave(new PlayerPrefsCraftingSaveStore());
        private KnowledgeShelf _shelf;
        private GearModBench _mods;
        private CraftingLoot _craftLoot;
        private bool _craftingLoaded;

        public KnowledgeShelf Shelf => _shelf ??= new KnowledgeShelf(CraftingData.Knowledge, Crafting);
        public GearModBench Mods => _mods ??= new GearModBench(CraftingData.Mods, CraftingData.Quality, Crafting);
        public CraftingLoot CraftLoot => _craftLoot ??= new CraftingLoot(CraftingData);

        /// <summary>Clic derecho sobre un conocimiento de la bolsa; false si el objeto no es un conocimiento.</summary>
        public bool ReadKnowledge(string itemId)
        {
            var result = Shelf.Read(itemId);
            if (result == null) return false;
            Feed.Add(KnowledgeShelf.Message(result.Value));
            return true;
        }

        /// <summary>Carga el progreso de oficios; un guardado dañado se respalda (.bak) y se empieza de cero.</summary>
        public void LoadCrafting()
        {
            try
            {
                new CraftingRestorer(CraftingData, Mods).Apply(_save.Load(), Crafting, Stations);
                Feed.Add("Progreso de oficios cargado.");
            }
            catch (CraftingSaveFormatException e)
            {
                Debug.LogWarning($"Guardado de oficios dañado: {e.Message}");
                Feed.Add("El guardado de oficios estaba dañado: se guardó una copia .bak.");
            }
            _craftingLoaded = true;
        }

        /// <summary>Guarda niveles, recetas, estaciones, piezas, cola e inventario (solo tras haber cargado).</summary>
        public void SaveCrafting()
        {
            if (!_craftingLoaded) return;
            _save.Save(CraftingSnapshot.Capture(Crafting, Stations.Sessions, Mods));
        }
    }
}
