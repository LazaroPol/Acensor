using System.Collections.Generic;
using Specimen.Core.Classes;

namespace Specimen.Core.Abilities
{
    /// <summary>Habilidad de firma de cada kit (GDD §4). Costes y enfriamientos provisionales, a ajustar en balance.</summary>
    public static class DefaultClassAbilities
    {
        private static readonly Dictionary<ClassKitId, AbilityDefinition> Signatures =
            new Dictionary<ClassKitId, AbilityDefinition>
            {
                { ClassKitId.Bastion, new AbilityDefinition("bastion.muro", "Muro de escudo", 30f, 0f, 12f) },
                { ClassKitId.Vigia, new AbilityDefinition("vigia.esquiva", "Esquiva", 15f, 20f, 4f) },
                { ClassKitId.Oraculo, new AbilityDefinition("oraculo.sanar", "Sanación", 40f, 0f, 8f) },
                { ClassKitId.DruidaCeniza, new AbilityDefinition("druida.brotes", "Brotes de ceniza", 35f, 0f, 10f) },
                { ClassKitId.Duelista, new AbilityDefinition("duelista.tajo", "Tajo veloz", 25f, 10f, 5f) },
                { ClassKitId.Arcanista, new AbilityDefinition("arcanista.orbe", "Orbe arcano", 30f, 0f, 6f) }
            };

        public static AbilityDefinition SignatureOf(ClassKitId kit) => Signatures[kit];
    }
}
