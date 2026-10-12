using Specimen.Core.Crafting.Definitions;
using Specimen.Core.Crafting.Queue;
using Specimen.Core.Crafting.Stations;
using Specimen.Core.Professions;

namespace Specimen.Core.Crafting.Session
{
    /// <summary>Textos de la ventana de estación (GDD §17 «Nombre visible»): oficios, estaciones y motivos.</summary>
    public static class StationLabels
    {
        public static string Profession(ProfessionId id)
        {
            switch (id)
            {
                case ProfessionId.Sastre: return "Sastre y curtidor";
                case ProfessionId.Quimico: return "Alquimista";
                case ProfessionId.Ingeniero: return "Maestro de ingenios";
                case ProfessionId.Manitas: return "Carpintero";
                case ProfessionId.Escultor: return "Cantero";
                default: return id.ToString();
            }
        }

        public static string Station(StationType type)
        {
            switch (type)
            {
                case StationType.Fragua: return "Fragua";
                case StationType.TallerSastre: return "Taller de sastre";
                case StationType.HogarCocina: return "Hogar de cocina";
                case StationType.MesaAlquimia: return "Mesa de alquimia";
                case StationType.TallerIngenios: return "Taller de ingenios";
                case StationType.BancoCarpintero: return "Banco de carpintero";
                default: return "Banco de cantero";
            }
        }

        /// <summary>Motivo de una receta en gris («Requiere Herrero 10», «Faltan materiales»); vacío si se puede.</summary>
        public static string Block(RecipeOption option)
        {
            switch (option.Block)
            {
                case RecipeBlockReason.ProfessionLevel:
                    return $"Requiere {Profession(option.Recipe.Profession)} {option.Recipe.MinProfessionLevel}";
                case RecipeBlockReason.MissingMaterials: return "Faltan materiales";
                default: return string.Empty;
            }
        }

        public static string State(CraftQueueState state)
        {
            switch (state)
            {
                case CraftQueueState.Working: return "Trabajando";
                case CraftQueueState.NoFuel: return "En pausa: sin combustible";
                case CraftQueueState.OutputFull: return "En pausa: salida llena";
                default: return "Parada";
            }
        }

        public static string Result(StationCraftResult result)
        {
            switch (result)
            {
                case StationCraftResult.Queued: return "Encolado";
                case StationCraftResult.ProfessionLevel: return "Tu nivel de oficio es insuficiente";
                case StationCraftResult.MissingMaterials: return "Faltan materiales";
                case StationCraftResult.QueueFull: return "La cola está llena";
                default: return "Esa receta no está disponible aquí";
            }
        }
    }
}
