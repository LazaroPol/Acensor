using System.Collections.Generic;
using Specimen.Core.Combat;
using Specimen.Core.Progression;
using Specimen.Runtime.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Specimen.Runtime.Sandbox.Ui
{
    /// <summary>
    /// Texto de combate flotante estilo WoW: tu daño con el arma en blanco, el de habilidades en amarillo, el que
    /// recibes en rojo, curas en verde; los críticos salen más grandes con «!». La experiencia de combate en azul
    /// claro y la de profesiones en verde.
    /// </summary>
    public sealed class CombatText : MonoBehaviour
    {
        private const float Lifetime = 1.3f;
        private const float Rise = 1.6f;

        public static readonly Color Weapon = Color.white;
        public static readonly Color Ability = new Color(1f, 0.86f, 0.12f);
        public static readonly Color Incoming = new Color(0.95f, 0.15f, 0.12f);
        public static readonly Color Heal = new Color(0.2f, 1f, 0.45f);
        public static readonly Color CombatXp = new Color(0.45f, 0.8f, 1f);
        public static readonly Color ProfessionXp = new Color(0.4f, 0.95f, 0.3f);

        private readonly List<(Text label, Vector3 world, float age, float scale)> _live = new List<(Text, Vector3, float, float)>();
        private SandboxContext _context;
        private Transform _player;
        private RectTransform _layer;

        public void Init(UiRoot root, SandboxContext context, Transform player)
        {
            _context = context;
            _player = player;
            _layer = UiFactory.Node(root.Rect, "TextoCombate", Vector2.zero, Vector2.zero);
            UiFactory.Stretch(_layer);
            _layer.SetAsFirstSibling();
            var events = context.Events;
            events.DamageDealt += OnDealt;
            events.DamageTaken += (at, result) => Spawn(at, $"-{result.Final:0}", Incoming, result.IsCritical);
            events.Healed += (at, amount) => { if (amount >= 1f) Spawn(at, $"+{amount:0}", Heal, false); };
            events.XpGained += OnXp;
        }

        private void OnDealt(SandboxEnemy enemy, DamageResult result, HitSource source)
        {
            var at = enemy.transform.position + Vector3.up * (enemy.Height + 0.4f);
            Spawn(at, result.IsCritical ? $"{result.Final:0}!" : $"{result.Final:0}",
                source == HitSource.Arma ? Weapon : Ability, result.IsCritical);
        }

        private void OnXp(int amount, XpSource source)
        {
            var at = _player.position + Vector3.up * 2.5f;
            Spawn(at, $"+{amount} PX", source == XpSource.Combate ? CombatXp : ProfessionXp, false, 0.85f);
        }

        public void Spawn(Vector3 world, string text, Color color, bool critical, float size = 1f)
        {
            var jitter = new Vector3(Random.Range(-0.4f, 0.4f), Random.Range(0f, 0.3f), 0f);
            var label = UiFactory.Label(_layer, text, Vector2.zero, new Vector2(240f, 60f), critical ? 40 : 26,
                TextAnchor.MiddleCenter, color);
            label.fontStyle = critical ? FontStyle.Bold : FontStyle.Normal;
            var outline = label.gameObject.AddComponent<Outline>();
            outline.effectColor = new Color(0f, 0f, 0f, 0.9f);
            ((RectTransform)label.transform).pivot = new Vector2(0.5f, 0.5f);
            _live.Add((label, world + jitter, 0f, (critical ? 1.5f : 1f) * size));
        }

        private void LateUpdate()
        {
            var camera = Camera.main;
            float scale = GetComponentInParent<Canvas>() is { } canvas ? canvas.scaleFactor : 1f;
            for (int i = _live.Count - 1; i >= 0; i--)
            {
                var (label, world, age, size) = _live[i];
                age += Time.deltaTime;
                if (age >= Lifetime || camera == null)
                {
                    Destroy(label.gameObject);
                    _live.RemoveAt(i);
                    continue;
                }
                _live[i] = (label, world, age, size);
                float t = age / Lifetime;
                var screen = camera.WorldToScreenPoint(world + Vector3.up * (Rise * t));
                label.enabled = screen.z > 0f;
                var rect = (RectTransform)label.transform;
                rect.anchoredPosition = new Vector2(screen.x / scale, -(Screen.height - screen.y) / scale);
                float pop = size * (age < 0.12f ? 1f + (0.12f - age) * 4f : 1f);
                rect.localScale = Vector3.one * pop;
                var color = label.color;
                label.color = new Color(color.r, color.g, color.b, t < 0.6f ? 1f : 1f - (t - 0.6f) / 0.4f);
            }
        }
    }
}
