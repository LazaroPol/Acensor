using Specimen.Core.Classes;
using Specimen.Core.Visuals;

namespace Specimen.Editor.Arte
{
    /// <summary>
    /// Paso «CatalogoVisual»: el greybox del Prototipo pasa a usar un personaje y un enemigo reales
    /// (Bastión y Diablillo de brasa); suelo, muros, puertas y loot los asigna el paso Entorno.
    /// </summary>
    public sealed class VisualCatalogStep : IArtStep
    {
        public const string PrototypeEnemy = "diablillo";

        public string Name => "CatalogoVisual";

        public void Run(ArtContext context)
        {
            VisualCatalogWriter.Assign(VisualId.Jugador, context.Catalog.ClassPrefab(ClassKitId.Bastion));
            VisualCatalogWriter.Assign(VisualId.Enemigo, context.Catalog.EnemyPrefab(PrototypeEnemy));
        }
    }
}
