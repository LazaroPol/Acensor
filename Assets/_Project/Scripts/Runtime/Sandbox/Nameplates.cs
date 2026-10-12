using UnityEngine;

namespace Specimen.Runtime.Sandbox
{
    /// <summary>Placas de nombre y vida sobre las criaturas, y cuenta atrás de reaparición de las caídas.</summary>
    public sealed class Nameplates : MonoBehaviour
    {
        private SandboxContext _context;
        private GUIStyle _style;

        public void Init(SandboxContext context) => _context = context;

        private void OnGUI()
        {
            var camera = Camera.main;
            if (_context == null || camera == null) return;
            _style ??= new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold };
            foreach (var enemy in _context.Enemies.All)
            {
                if (enemy == null) continue;
                if (enemy.IsDead) Respawn(camera, enemy);
                else Plate(camera, enemy);
            }
        }

        private void Plate(Camera camera, SandboxEnemy enemy)
        {
            var screen = camera.WorldToScreenPoint(enemy.transform.position + Vector3.up * (enemy.Height + 0.3f));
            if (screen.z <= 0f || screen.z > 40f) return;
            var rect = new Rect(screen.x - 50f, Screen.height - screen.y, 100f, 7f);
            Fill(rect, new Color(0f, 0f, 0f, 0.7f));
            var fill = rect;
            fill.width *= enemy.HealthFraction;
            Fill(fill, new Color(0.85f, 0.15f, 0.12f));
            _style.normal.textColor = new Color(1f, 0.85f, 0.4f);
            GUI.Label(new Rect(rect.x - 50f, rect.y - 20f, 200f, 20f), enemy.DisplayName, _style);
        }

        private void Respawn(Camera camera, SandboxEnemy enemy)
        {
            if (enemy.RespawnRemaining <= 0f) return;
            var screen = camera.WorldToScreenPoint(enemy.transform.position + Vector3.up * 1.2f);
            if (screen.z <= 0f || screen.z > 40f) return;
            _style.normal.textColor = new Color(0.75f, 0.75f, 0.75f);
            GUI.Label(new Rect(screen.x - 100f, Screen.height - screen.y, 200f, 20f),
                $"{enemy.DisplayName}: vuelve en {enemy.RespawnRemaining:0} s", _style);
        }

        private static void Fill(Rect rect, Color color) =>
            GUI.DrawTexture(rect, Texture2D.whiteTexture, ScaleMode.StretchToFill, false, 0f, color, 0f, 0f);
    }
}
