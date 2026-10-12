using System.Collections.Generic;

namespace Specimen.Core.Professions
{
    /// <summary>Banco de trabajo de cada profesión: una base y herramientas del pack KayKit RPG Tools Bits encima.</summary>
    public static class ProfessionFurniture
    {
        public const string BenchBase = "Prop_Crate";

        private static readonly Dictionary<ProfessionId, string[]> Tools = new Dictionary<ProfessionId, string[]>
        {
            [ProfessionId.Herrero] = new[] { "anvil", "hammer", "tongs", "grindstone" },
            [ProfessionId.Sastre] = new[] { "scissors", "rope_bundle_A", "rope_bundle_B" },
            [ProfessionId.Cocinero] = new[] { "bucket_metal", "knife", "lantern" },
            [ProfessionId.Quimico] = new[] { "magnifying_glass", "journal_open", "torch" },
            [ProfessionId.Ingeniero] = new[] { "blueprint", "drafting_compass", "wrench_A", "screwdriver_A_long_color" },
            [ProfessionId.Manitas] = new[] { "saw", "handdrill", "handplane", "nail" },
            [ProfessionId.Escultor] = new[] { "chisel", "mallet", "trowel", "file" }
        };

        public static IReadOnlyList<string> ToolsFor(ProfessionId profession) => Tools[profession];
    }
}
