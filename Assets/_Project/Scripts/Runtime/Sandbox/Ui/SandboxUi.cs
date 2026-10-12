using System;
using System.Collections.Generic;
using Specimen.Core.Classes;
using Specimen.Runtime.UI;
using UnityEngine;

namespace Specimen.Runtime.Sandbox.Ui
{
    /// <summary>
    /// Interfaz del Campo de pruebas estilo MMO: HUD (marco, barra de acción, PX, minimapa, registro) y ventanas
    /// (bolsa, personaje, hechizos, talentos, profesión, estación y mapa) con sus atajos de teclado y botones de menú.
    /// </summary>
    public sealed class SandboxUi : MonoBehaviour
    {
        private readonly List<UiWindow> _windows = new List<UiWindow>();
        private SandboxContext _context;
        private SandboxPlayer _player;
        private HudMessages _messages;
        private InventoryWindow _bag;
        private CharacterWindow _character;
        private SpellbookWindow _spellbook;
        private TalentWindow _talents;
        private ProfessionWindow _profession;
        private StationWindow _station;
        private MapWindow _map;

        public void Init(SandboxContext context, SandboxPlayer player, IReadOnlyList<ClassKit> kits, Action<ClassKit> equip)
        {
            _context = context;
            _player = player;
            var root = UiRoot.Create();
            root.transform.SetParent(transform, false);
            gameObject.AddComponent<HudFrames>().Init(root, context, player);
            gameObject.AddComponent<HudActionBar>().Init(root, context, player);
            gameObject.AddComponent<HudTargetFrame>().Init(root, context, player);
            gameObject.AddComponent<CombatText>().Init(root, context, player.transform);
            gameObject.AddComponent<HudMinimap>().Init(root, context, player.transform);
            _messages = gameObject.AddComponent<HudMessages>();
            _messages.Init(root, context);
            context.Events.Error += _messages.Error;
            _bag = Add(UiWindow.Spawn<InventoryWindow>(root, new Vector2(1480f, 420f), w => { w.Context = context; w.Player = player; }));
            _character = Add(UiWindow.Spawn<CharacterWindow>(root, new Vector2(40f, 140f), w =>
            {
                (w.Context, w.Player, w.Kits, w.Equip) = (context, player, kits, equip);
            }));
            _spellbook = Add(UiWindow.Spawn<SpellbookWindow>(root, new Vector2(520f, 170f), w => { w.Context = context; w.Player = player; }));
            _talents = Add(UiWindow.Spawn<TalentWindow>(root, new Vector2(560f, 120f), w => { w.Context = context; w.Player = player; }));
            _profession = Add(UiWindow.Spawn<ProfessionWindow>(root, new Vector2(640f, 200f), w => { w.Context = context; w.Player = player; }));
            _station = Add(UiWindow.Spawn<StationWindow>(root, new Vector2(560f, 160f), w => { w.Context = context; w.OpenTrade = _profession.Show; }));
            _map = Add(UiWindow.Spawn<MapWindow>(root, new Vector2(540f, 110f), w => { w.Context = context; w.Player = player; }));
            BuildMenu(root);
            UiTooltip.Create(root);
            root.Windows.SetAsLastSibling();
            root.Overlay.SetAsLastSibling();
            context.BenchOpened += _station.Show;
            context.Hero.Progress.LeveledUp += OnLevelUp;
        }

        private T Add<T>(T window) where T : UiWindow
        {
            _windows.Add(window);
            return window;
        }

        private void BuildMenu(UiRoot root)
        {
            var labels = new (string text, UiWindow window)[]
            {
                ("Personaje (C)", _character), ("Bolsa (B)", _bag), ("Hechizos (P)", _spellbook),
                ("Talentos (N)", _talents), ("Mapa (M)", _map)
            };
            var menu = UiFactory.Node(root.Rect, "Menu", Vector2.zero, new Vector2(labels.Length * 124f, 34f));
            menu.anchorMin = menu.anchorMax = menu.pivot = new Vector2(1f, 0f);
            menu.anchoredPosition = new Vector2(-16f, 40f);
            for (int i = 0; i < labels.Length; i++)
            {
                var window = labels[i].window;
                UiFactory.Button(menu, labels[i].text, new Vector2(i * 124f, 0f), new Vector2(118f, 32f), window.Toggle, 13);
            }
        }

        private void OnDestroy()
        {
            if (_context == null) return;
            _context.BenchOpened -= _station.Show;
            _context.Events.Error -= _messages.Error;
            _context.Hero.Progress.LeveledUp -= OnLevelUp;
        }

        private void CloseOrClearTarget()
        {
            bool any = _windows.Exists(w => w.IsOpen);
            _windows.ForEach(w => w.Close());
            if (!any) _player.GetComponent<PlayerTarget>()?.Clear();
        }

        private void OnLevelUp(int level)
        {
            _player.RefreshProgression(true);
            string spell = _player.Kit != null ? SpellCaster.LearnedText(_context.Hero.SpellbookFor(_player.Kit.Id), level) : null;
            _messages.Announce($"¡Nivel {level}!\n<size=20>+1 punto de talento{(spell != null ? " · " + spell : string.Empty)}</size>");
            _context.Feed.Add($"Has alcanzado el nivel {level}. {spell}");
        }

        private void Update()
        {
            var input = _context.Input;
            if (input.Bag.WasPressedThisFrame()) _bag.Toggle();
            if (input.Character.WasPressedThisFrame()) _character.Toggle();
            if (input.Spellbook.WasPressedThisFrame()) _spellbook.Toggle();
            if (input.Talents.WasPressedThisFrame()) _talents.Toggle();
            if (input.Map.WasPressedThisFrame()) _map.Toggle();
            if (input.Help.WasPressedThisFrame()) _messages.ToggleHelp();
            if (input.Close.WasPressedThisFrame()) CloseOrClearTarget();
            if (_context.Interactions?.Current is BenchStation) return;
            if (_profession.IsOpen) _profession.Close();
            if (_station.IsOpen) _station.Close();
        }
    }
}
