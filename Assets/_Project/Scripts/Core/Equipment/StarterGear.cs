using System.Collections.Generic;
using Specimen.Core.Classes;
using G = Specimen.Core.Items.Catalog.GearIds;

namespace Specimen.Core.Equipment
{
    /// <summary>Equipo con el que empieza cada kit (arma con su velocidad propia, pieza de pecho y pantalones).</summary>
    public static class StarterGear
    {
        public static IReadOnlyList<string> For(ClassKitId kit)
        {
            switch (kit)
            {
                case ClassKitId.Bastion: return new[] { G.MazaRecluta, G.CotaAnillas, G.PantalonesTela };
                case ClassKitId.Vigia: return new[] { G.LanzaVigia, G.JubonCuero, G.PantalonesTela };
                case ClassKitId.Oraculo: return new[] { G.BastonAprendiz, G.TunicaLino, G.PantalonesTela };
                case ClassKitId.DruidaCeniza: return new[] { G.CayadoCeniza, G.JubonCuero, G.PantalonesTela };
                case ClassKitId.Duelista: return new[] { G.DagaGastada, G.JubonCuero, G.PantalonesTela };
                default: return new[] { G.VaritaChispas, G.TunicaLino, G.PantalonesTela };
            }
        }
    }
}
