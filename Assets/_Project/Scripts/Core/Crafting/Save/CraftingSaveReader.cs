using System;
using System.Collections.Generic;
using System.Linq;

namespace Specimen.Core.Crafting.Save
{
    /// <summary>Lee el texto <c>oficios v1</c>; cualquier error lanza <see cref="CraftingSaveFormatException"/>.</summary>
    public static class CraftingSaveReader
    {
        private static readonly Dictionary<string, Action<CraftingSaveData, string>> Parsers =
            new Dictionary<string, Action<CraftingSaveData, string>>(StringComparer.Ordinal)
            {
                ["skill"] = (d, v) => d.Skills.Add(SaveLineParsers.Skill(v)),
                ["known"] = (d, v) => d.KnownRecipes.Add(SaveLineParsers.SingleId(v, "known")),
                ["read"] = (d, v) => d.ReadVolumes.Add(SaveLineParsers.SingleId(v, "read")),
                ["station"] = (d, v) => d.Stations.Add(SaveLineParsers.Station(v)),
                ["part"] = (d, v) => d.Parts.Add(SaveLineParsers.Part(v)),
                ["slot"] = (d, v) => d.Slots.Add(SaveLineParsers.Slot(v)),
                ["job"] = (d, v) => d.Jobs.Add(SaveLineParsers.Job(v)),
                ["item"] = (d, v) => d.Items.Add(SaveLineParsers.Item(v)),
                ["inv"] = (d, v) => d.Inventory.Add(SaveLineParsers.Inventory(v)),
            };

        public static CraftingSaveData Read(string text)
        {
            var lines = (text ?? string.Empty).Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(l => l.TrimEnd('\r')).ToArray();
            if (lines.Length == 0 || lines[0] != CraftingSaveWriter.Header)
                throw new CraftingSaveFormatException("Versión de guardado de oficios no soportada.");
            var data = new CraftingSaveData();
            foreach (var line in lines.Skip(1))
            {
                var eq = line.IndexOf('=');
                if (eq <= 0) throw new CraftingSaveFormatException($"Línea inválida: {line}");
                var key = line.Substring(0, eq);
                if (!Parsers.TryGetValue(key, out var parse)) throw new CraftingSaveFormatException($"Clave desconocida: {key}");
                parse(data, line.Substring(eq + 1));
            }
            CheckReferences(data);
            return data;
        }

        private static void CheckReferences(CraftingSaveData data)
        {
            var ids = new HashSet<string>(StringComparer.Ordinal);
            foreach (var item in data.Items)
                if (!ids.Add(item.InstanceId)) throw new CraftingSaveFormatException($"Instancia duplicada: {item.InstanceId}");
            foreach (var e in data.Inventory.Where(e => e.IsInstance && !ids.Contains(e.Ref)))
                throw new CraftingSaveFormatException($"El inventario apunta a una instancia inexistente: {e.Ref}");
        }
    }
}
