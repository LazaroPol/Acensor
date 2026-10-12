using System;
using Specimen.Core.Professions;

namespace Specimen.Core.Crafting.Definitions
{
    /// <summary>Las 7 estaciones de trabajo (GDD §17). Valores explícitos y estables: se serializan.</summary>
    public enum StationType
    {
        Fragua = 1,
        TallerSastre = 2,
        HogarCocina = 3,
        MesaAlquimia = 4,
        TallerIngenios = 5,
        BancoCarpintero = 6,
        BancoCantero = 7
    }

    /// <summary>Estación principal de cada oficio (tabla de GDD §17).</summary>
    public static class StationTypes
    {
        public static StationType MainOf(ProfessionId profession)
        {
            switch (profession)
            {
                case ProfessionId.Herrero: return StationType.Fragua;
                case ProfessionId.Sastre: return StationType.TallerSastre;
                case ProfessionId.Cocinero: return StationType.HogarCocina;
                case ProfessionId.Quimico: return StationType.MesaAlquimia;
                case ProfessionId.Ingeniero: return StationType.TallerIngenios;
                case ProfessionId.Manitas: return StationType.BancoCarpintero;
                case ProfessionId.Escultor: return StationType.BancoCantero;
                default: throw new ArgumentOutOfRangeException(nameof(profession), profession, null);
            }
        }

        public static bool IsDefined(StationType station) => Enum.IsDefined(typeof(StationType), station);
    }
}
