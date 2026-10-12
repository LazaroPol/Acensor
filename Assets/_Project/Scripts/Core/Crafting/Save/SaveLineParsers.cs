using System.Linq;
using Specimen.Core.Professions;
using static Specimen.Core.Crafting.Save.SaveFields;

namespace Specimen.Core.Crafting.Save
{
    /// <summary>Convierte el valor de cada clave en su entrada del guardado.</summary>
    internal static class SaveLineParsers
    {
        public static string SingleId(string v, string key) => NonEmpty(Split(v, 1, key)[0], key);

        public static SkillEntry Skill(string v)
        {
            var f = Split(v, 3, "skill");
            return new SkillEntry(Enum<ProfessionId>(f[0], "skill"), Int(f[1], "skill"), Int(f[2], "skill"));
        }

        public static StationEntry Station(string v)
        {
            var f = Split(v, 2, "station");
            return new StationEntry(NonEmpty(f[0], "station"), Int(f[1], "station"));
        }

        public static PartEntry Part(string v)
        {
            var f = Split(v, 4, "part");
            return new PartEntry(NonEmpty(f[0], "part"), Int(f[1], "part"), NonEmpty(f[2], "part"), Int(f[3], "part"));
        }

        public static SlotEntry Slot(string v)
        {
            var f = Split(v, 5, "slot");
            return new SlotEntry(NonEmpty(f[0], "slot"), NonEmpty(f[1], "slot"), Int(f[2], "slot"),
                NonEmpty(f[3], "slot"), Int(f[4], "slot"));
        }

        public static JobEntry Job(string v)
        {
            var f = Split(v, 5, "job");
            return new JobEntry(NonEmpty(f[0], "job"), NonEmpty(f[1], "job"), Seconds(f[2], "job"),
                Enum<ProfessionId>(f[3], "job"), Int(f[4], "job"));
        }

        public static ItemEntry Item(string v)
        {
            var f = Split(v, 5, "item");
            var mods = f[4].Length == 0 ? new string[0] : f[4].Split(',').Select(m => NonEmpty(m, "item")).ToArray();
            return new ItemEntry(NonEmpty(f[0], "item"), NonEmpty(f[1], "item"), Quality(f[2], "item"), Int(f[3], "item"), mods);
        }

        public static InventoryEntry Inventory(string v)
        {
            var f = v.Split(':');
            if (f.Length == 3) return InventoryEntry.Instance(NonEmpty(f[0], "inv"), Int(f[1], "inv"), NonEmpty(f[2], "inv"));
            f = Split(v, 4, "inv");
            return InventoryEntry.Stack(NonEmpty(f[0], "inv"), Int(f[1], "inv"), NonEmpty(f[2], "inv"), Int(f[3], "inv"));
        }
    }
}
