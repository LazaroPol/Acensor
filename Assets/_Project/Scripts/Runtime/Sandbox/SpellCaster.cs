using System.Collections.Generic;
using Specimen.Core.Classes;
using Specimen.Core.Progression;
using Specimen.Core.Stats;
using Specimen.Runtime.UI;
using UnityEngine;

namespace Specimen.Runtime.Sandbox
{
    /// <summary>
    /// Barra de hechizos (teclas 1-5): un hueco por hechizo del kit; se desbloquean al subir de nivel.
    /// Aplica el poder de hechizos y la reducción de enfriamiento de los talentos.
    /// </summary>
    public sealed partial class SpellCaster : MonoBehaviour
    {
        private readonly List<SpellSlot> _slots = new List<SpellSlot>();
        private SandboxPlayer _player;
        private SandboxContext _context;
        private PlayerTarget _target;
        private AutoAttack _attack;

        public IReadOnlyList<SpellSlot> Slots => _slots;

        public void Init(SandboxPlayer player, SandboxContext context, PlayerTarget target, AutoAttack attack)
        {
            _player = player;
            _context = context;
            _target = target;
            _attack = attack;
            _player.Equipped += Rebuild;
        }

        private void OnDestroy()
        {
            if (_player != null) _player.Equipped -= Rebuild;
        }

        private void Rebuild(ClassKit kit)
        {
            _slots.Clear();
            foreach (var spell in _context.Hero.SpellbookFor(kit.Id).All) _slots.Add(new SpellSlot(spell));
        }

        private void Update()
        {
            float dt = Time.deltaTime;
            _context.Fury.Tick(dt);
            foreach (var slot in _slots) slot.Tick(dt);
            int pressed = _context.Input.SpellPressed();
            if (pressed >= 0) Cast(pressed);
        }

        public void Cast(int index)
        {
            if (index < 0 || index >= _slots.Count || !_player.IsAlive) return;
            var slot = _slots[index];
            if (!TargetReady(slot.Spell)) return;
            var result = slot.TryCast(_context.Hero.Level, _player.Vitals.ClassResource, _player.Bonuses.CooldownReduction);
            if (result != SpellCastResult.Cast)
            {
                _context.Events.Fail($"{slot.Spell.Name}: {Reason(result, slot)}");
                return;
            }
            _context.Feed.Add($"¡{slot.Spell.Name}!");
            Apply(slot.Spell);
        }

        private float Damage(SpellDefinition spell) =>
            _player.Stats.ValueOf(StatType.Damage) * spell.Power * (1f + _player.Bonuses.SpellPower);

        private static string Reason(SpellCastResult result, SpellSlot slot) => result switch
        {
            SpellCastResult.NotLearned => $"se aprende a nivel {slot.Spell.Level}",
            SpellCastResult.OnCooldown => $"en enfriamiento ({slot.Remaining:0.0} s)",
            _ => "recurso insuficiente"
        };

        /// <summary>¿Hay algún hechizo nuevo a este nivel? (para el aviso de subida de nivel).</summary>
        public static string LearnedText(Spellbook book, int level)
        {
            var names = new List<string>();
            foreach (var spell in book.LearnedAt(level)) names.Add(spell.Name);
            return names.Count > 0 ? "Nuevo hechizo: " + string.Join(", ", names) : null;
        }

        internal static bool BlockedByUi() => UiRoot.PointerOverUi();
    }
}
