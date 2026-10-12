using System.Linq;
using Specimen.Core.Vitals;
using UnityEngine;

namespace Specimen.Runtime.Sandbox
{
    /// <summary>HUD de depuración del Campo de pruebas (IMGUI): estado del kit, registro, ayuda y vida de enemigos.</summary>
    public sealed partial class SandboxHud : MonoBehaviour
    {
        private SandboxContext _context;
        private SandboxPlayer _player;
        private PlayerCombat _combat;
        private SignatureCaster _signature;
        private BenchStation[] _benches = new BenchStation[0];
        private bool _help = true;

        public void Init(SandboxContext context, SandboxPlayer player, BenchStation[] benches)
        {
            _context = context;
            _player = player;
            _combat = player.GetComponent<PlayerCombat>();
            _signature = player.GetComponent<SignatureCaster>();
            _benches = benches;
        }

        private void Update()
        {
            if (_context != null && _context.Input.Help.WasPressedThisFrame()) _help = !_help;
        }

        private void OnGUI()
        {
            if (_player == null || _player.Kit == null) return;
            DrawStatus();
            DrawFeed();
            if (_help) DrawHelp();
            DrawEnemyBars();
            DrawBenchPrompt();
        }

        private void DrawStatus()
        {
            GUILayout.BeginArea(new Rect(10f, 10f, 330f, 260f), GUI.skin.box);
            GUILayout.Label($"<b>{_player.Kit.DisplayName}</b> · {_player.Kit.Role} · {_player.Kit.Resource}", Rich());
            Bar("Vida", _player.Vitals.Health, new Color(0.85f, 0.2f, 0.2f));
            Bar("Recurso", _player.Vitals.ClassResource, new Color(0.3f, 0.5f, 1f));
            Bar("Aguante", _player.Vitals.Stamina, new Color(0.9f, 0.8f, 0.2f));
            var attack = _combat != null ? _combat.Attack : null;
            if (attack != null)
                GUILayout.Label($"Ataque ({attack.Profile.Style}): {(attack.IsReady ? "listo" : "recargando")}");
            var slot = _signature != null ? _signature.Slot : null;
            if (slot != null)
                GUILayout.Label($"Q · {slot.Definition.DisplayName}: {(slot.IsReady ? "lista" : $"{slot.Cooldown.Remaining:0.0}s")}");
            if (_player.IsShielded) GUILayout.Label("Escudo activo");
            string profession = _context.Bench.TryGetProfession(SandboxContext.LocalPlayer, out var p) ? p.ToString() : "—";
            GUILayout.Label($"Profesión: {profession} · Enemigos vivos: {_context.Enemies.AliveCount()}");
            string items = string.Join(", ", _context.Inventory.Stacks.Select(s => $"{s.Item.Definition.Name} x{s.Quantity}"));
            GUILayout.Label($"Botín: {(items.Length > 0 ? items : "—")}");
            GUILayout.EndArea();
        }

        private static void Bar(string label, ResourcePool pool, Color color)
        {
            GUILayout.Label($"{label}: {pool.Current:0}/{pool.Max:0}");
            var rect = GUILayoutUtility.GetRect(300f, 8f);
            GUI.DrawTexture(rect, Texture2D.whiteTexture, ScaleMode.StretchToFill, false, 0f, new Color(0f, 0f, 0f, 0.5f), 0f, 0f);
            rect.width *= pool.Fraction;
            GUI.DrawTexture(rect, Texture2D.whiteTexture, ScaleMode.StretchToFill, false, 0f, color, 0f, 0f);
        }

        private static GUIStyle Rich() => new GUIStyle(GUI.skin.label) { richText = true };
    }
}
