using UnityEngine;

namespace Specimen.Runtime.Sandbox
{
    /// <summary>Paneles secundarios del HUD: registro de combate, ayuda, barras de enemigos y aviso de banco.</summary>
    public sealed partial class SandboxHud
    {
        private const string HelpText =
            "WASD mover · Shift esprintar · Espacio esquivar · Rueda zoom\n" +
            "Clic izq / J atacar · Q habilidad de firma · E usar banco\n" +
            "1-6 / Tab cambiar de kit · R reiniciar · F1 dañarte · F2 curarte · H ocultar ayuda";

        private void DrawFeed()
        {
            float y = Screen.height - 30f - CombatFeed.Capacity * 20f;
            foreach (string line in _context.Feed.Recent())
            {
                GUI.Label(new Rect(10f, y, 700f, 22f), line);
                y += 20f;
            }
        }

        private void DrawHelp()
        {
            GUI.Box(new Rect(Screen.width - 470f, 10f, 460f, 70f), HelpText);
        }

        private void DrawEnemyBars()
        {
            var camera = Camera.main;
            if (camera == null) return;
            DrawRespawnLabels(camera);
            foreach (var enemy in _context.Enemies.Alive())
            {
                var screen = camera.WorldToScreenPoint(enemy.transform.position + Vector3.up * (enemy.Height + 0.3f));
                if (screen.z <= 0f) continue;
                var rect = new Rect(screen.x - 50f, Screen.height - screen.y, 100f, 6f);
                GUI.DrawTexture(rect, Texture2D.whiteTexture, ScaleMode.StretchToFill, false, 0f, new Color(0f, 0f, 0f, 0.6f), 0f, 0f);
                var fill = rect;
                fill.width *= enemy.HealthFraction;
                GUI.DrawTexture(fill, Texture2D.whiteTexture, ScaleMode.StretchToFill, false, 0f, new Color(0.9f, 0.2f, 0.2f), 0f, 0f);
                GUI.Label(new Rect(rect.x - 20f, rect.y - 20f, 160f, 20f), enemy.DisplayName);
            }
        }

        private void DrawRespawnLabels(Camera camera)
        {
            foreach (var enemy in _context.Enemies.All)
            {
                if (enemy == null || !enemy.IsDead || enemy.RespawnRemaining <= 0f) continue;
                var screen = camera.WorldToScreenPoint(enemy.transform.position + Vector3.up * 1.2f);
                if (screen.z <= 0f) continue;
                GUI.Label(new Rect(screen.x - 60f, Screen.height - screen.y, 160f, 20f),
                    $"{enemy.DisplayName}: vuelve en {enemy.RespawnRemaining:0} s");
            }
        }

        private void DrawBenchPrompt()
        {
            foreach (var bench in _benches)
            {
                if (bench == null || !bench.IsPlayerNear()) continue;
                GUI.Box(new Rect(Screen.width / 2f - 150f, Screen.height - 80f, 300f, 26f), $"E · usar banco de {bench.Profession}");
                return;
            }
        }
    }
}
