using System.Linq;
using System.Text;
using static Specimen.Core.Crafting.Save.SaveFields;

namespace Specimen.Core.Crafting.Save
{
    /// <summary>Escribe <see cref="CraftingSaveData"/> como texto <c>oficios v1</c>, una clave por línea.</summary>
    public static class CraftingSaveWriter
    {
        public const string Header = "oficios v1";

        public static string Write(CraftingSaveData data)
        {
            var sb = new StringBuilder(Header).Append('\n');
            foreach (var s in data.Skills) Line(sb, "skill", s.Profession.ToString(), Text(s.Level), Text(s.Xp));
            foreach (var r in data.KnownRecipes) Line(sb, "known", Id(r));
            foreach (var v in data.ReadVolumes) Line(sb, "read", Id(v));
            foreach (var s in data.Stations) Line(sb, "station", Id(s.Station), Text(s.Level));
            foreach (var p in data.Parts) Line(sb, "part", Id(p.Station), Text(p.Slot), Id(p.ItemId), Text(p.Durability));
            foreach (var s in data.Slots) Line(sb, "slot", Id(s.Station), Id(s.Kind), Text(s.Index), Id(s.ItemId), Text(s.Count));
            foreach (var j in data.Jobs)
                Line(sb, "job", Id(j.Station), Id(j.RecipeId), Text(j.RemainingSeconds), j.Profession.ToString(), Text(j.Level));
            foreach (var i in data.Items)
                Line(sb, "item", Id(i.InstanceId), Id(i.ItemId), Text((int)i.Quality), Text(i.Durability),
                    string.Join(",", i.Mods.Select(Id)));
            foreach (var e in data.Inventory)
            {
                if (e.IsInstance) Line(sb, "inv", Id(e.Inventory), Text(e.Slot), Id(e.Ref));
                else Line(sb, "inv", Id(e.Inventory), Text(e.Slot), Id(e.Ref), Text(e.Count));
            }
            return sb.ToString();
        }

        private static void Line(StringBuilder sb, string key, params string[] fields)
            => sb.Append(key).Append('=').Append(string.Join(":", fields)).Append('\n');
    }
}
