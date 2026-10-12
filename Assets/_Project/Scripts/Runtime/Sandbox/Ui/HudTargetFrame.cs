using Specimen.Core.Progression;
using Specimen.Runtime.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Specimen.Runtime.Sandbox.Ui
{
    /// <summary>Marco del objetivo (junto al del jugador): nombre con el color de dificultad, nivel y vida en números.</summary>
    public sealed class HudTargetFrame : MonoBehaviour
    {
        private SandboxContext _context;
        private PlayerTarget _target;
        private AutoAttack _attack;
        private GameObject _frame;
        private Text _name;
        private Text _level;
        private Text _state;
        private Image _health;
        private Text _healthLabel;

        public void Init(UiRoot root, SandboxContext context, SandboxPlayer player)
        {
            _context = context;
            _target = player.GetComponent<PlayerTarget>();
            _attack = player.GetComponent<AutoAttack>();
            var frame = UiFactory.Panel(root.Rect, "MarcoObjetivo", new Vector2(410f, 16f), new Vector2(330f, 92f), UiTheme.Background);
            _frame = frame.gameObject;
            var badge = UiFactory.Panel(frame, "Nivel", new Vector2(10f, 10f), new Vector2(40f, 40f), UiTheme.Background);
            _level = UiFactory.Label(badge, string.Empty, Vector2.zero, new Vector2(40f, 40f), 18, TextAnchor.MiddleCenter);
            _name = UiFactory.Label(frame, string.Empty, new Vector2(60f, 8f), new Vector2(260f, 22f), 16);
            _health = UiFactory.Bar(frame, new Vector2(60f, 34f), new Vector2(260f, 20f), UiTheme.Health, out _healthLabel);
            _state = UiFactory.Label(frame, string.Empty, new Vector2(60f, 60f), new Vector2(260f, 22f), 13, TextAnchor.UpperLeft,
                UiTheme.TextDim);
        }

        private void Update()
        {
            bool has = _target != null && _target.HasTarget;
            _frame.SetActive(has);
            if (!has) return;
            var enemy = _target.Current;
            var color = ColorOf(LevelDifficulty.Of(_context.Hero.Level, enemy.Level));
            _name.text = enemy.DisplayName;
            _name.color = color;
            _level.text = enemy.Level.ToString();
            _level.color = color;
            _health.fillAmount = enemy.HealthFraction;
            _healthLabel.text = $"{enemy.CurrentHealth:0} / {enemy.MaxHealth:0}";
            float distance = _attack.DistanceTo(enemy);
            bool inRange = distance <= _attack.RangeTo(enemy);
            _state.text = $"{distance:0} m · " + (inRange ? "<color=#5AE65A>al alcance del arma</color>" : "fuera de alcance") +
                          (_attack.Active ? " · <color=#FFD040>atacando</color>" : string.Empty);
        }

        public static Color ColorOf(Difficulty difficulty) => difficulty switch
        {
            Difficulty.Rojo => new Color(1f, 0.1f, 0.1f),
            Difficulty.Naranja => new Color(1f, 0.5f, 0.1f),
            Difficulty.Amarillo => new Color(1f, 0.85f, 0.1f),
            Difficulty.Verde => new Color(0.25f, 0.8f, 0.25f),
            _ => new Color(0.6f, 0.6f, 0.6f)
        };
    }
}
