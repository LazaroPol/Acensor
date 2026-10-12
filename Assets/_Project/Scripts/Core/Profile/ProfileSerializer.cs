using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Specimen.Core.Profile
{
    /// <summary>Formato de texto versionado, una clave por línea: <c>v1</c>, <c>points=N</c>, <c>node=Id</c>, <c>fragment=Id</c>.</summary>
    public static class ProfileSerializer
    {
        public const string Version = "v1";

        public static string Serialize(ProfileData data)
        {
            var sb = new StringBuilder(Version).Append('\n');
            sb.Append("points=").Append(data.FreeDnaPoints.ToString(CultureInfo.InvariantCulture)).Append('\n');
            foreach (var id in data.UnlockedNodes.OrderBy(x => x, StringComparer.Ordinal)) sb.Append("node=").Append(id).Append('\n');
            foreach (var id in data.Fragments.OrderBy(x => x, StringComparer.Ordinal)) sb.Append("fragment=").Append(id).Append('\n');
            return sb.ToString();
        }

        public static ProfileData Deserialize(string text)
        {
            var lines = (text ?? string.Empty).Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(l => l.TrimEnd('\r')).ToArray();
            if (lines.Length == 0 || lines[0] != Version) throw new ProfileFormatException("Versión de perfil no soportada.");
            int? points = null;
            var nodes = new List<string>();
            var fragments = new List<string>();
            foreach (var line in lines.Skip(1))
            {
                var eq = line.IndexOf('=');
                if (eq <= 0 || eq == line.Length - 1) throw new ProfileFormatException($"Línea inválida: {line}");
                var key = line.Substring(0, eq);
                var value = line.Substring(eq + 1);
                if (key == "points") points = ParsePoints(value);
                else if (key == "node") nodes.Add(value);
                else if (key == "fragment") fragments.Add(value);
                else throw new ProfileFormatException($"Clave desconocida: {key}");
            }
            if (points == null) throw new ProfileFormatException("Falta «points».");
            return new ProfileData(points.Value, nodes, fragments);
        }

        private static int ParsePoints(string value)
        {
            if (!int.TryParse(value, NumberStyles.None, CultureInfo.InvariantCulture, out var n))
                throw new ProfileFormatException($"Puntos inválidos: {value}");
            return n;
        }
    }
}
