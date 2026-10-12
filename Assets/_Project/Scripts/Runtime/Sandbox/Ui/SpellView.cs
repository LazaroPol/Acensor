using Specimen.Core.Combat;
using Specimen.Core.Progression;
using UnityEngine;

namespace Specimen.Runtime.Sandbox.Ui
{
    /// <summary>Presentación de hechizos: icono (iniciales sobre el color de su tipo de daño) y tooltip.</summary>
    public static class SpellView
    {
        public static Color Tint(SpellDefinition spell) => spell.Kind switch
        {
            SpellKind.Curacion => new Color(0.25f, 0.6f, 0.3f),
            SpellKind.Escudo => new Color(0.3f, 0.4f, 0.65f),
            SpellKind.Impulso => new Color(0.45f, 0.45f, 0.5f),
            SpellKind.Furia => new Color(0.7f, 0.3f, 0.1f),
            _ => DamageColors.Of(spell.Type) * 0.75f
        };

        public static string Tooltip(SpellDefinition spell, int playerLevel)
        {
            string requirement = playerLevel >= spell.Level
                ? "<color=#5AE65A>Aprendido</color>"
                : $"<color=#F04A3A>Requiere nivel {spell.Level}</color>";
            return $"<size=16><b>{spell.Name}</b></size>\n" +
                   $"<color=#B0A890>{KindName(spell.Kind)} · Coste {spell.Cost:0} · Enfriamiento {spell.Cooldown:0.#} s</color>\n" +
                   $"{spell.Description}" + TargetNote(spell) + $"\n{requirement}";
        }

        private static string TargetNote(SpellDefinition spell) => spell.Kind switch
        {
            SpellKind.Golpe => "\n<color=#FFD040>Requiere objetivo · cuerpo a cuerpo (3 m) · daño basado en tu arma</color>",
            SpellKind.Proyectil => "\n<color=#FFD040>Requiere objetivo · alcance 25 m</color>",
            _ => string.Empty
        };

        public static string KindName(SpellKind kind) => kind switch
        {
            SpellKind.Golpe => "Golpe cuerpo a cuerpo",
            SpellKind.Proyectil => "Proyectil",
            SpellKind.Area => "Área",
            SpellKind.Curacion => "Curación",
            SpellKind.Escudo => "Defensa",
            SpellKind.Impulso => "Movimiento",
            _ => "Potenciador"
        };

        public static string Glyph(SpellDefinition spell) => ItemView.Glyph(spell.Name);
    }
}
