using Specimen.Runtime.UI;
using UnityEngine;

namespace Specimen.Runtime.Sandbox.Ui
{
    /// <summary>Barra de acción inferior: Atacar con el arma (F), firma (Q) y hechizos 1-5 con enfriamiento radial y tooltip.</summary>
    public sealed class HudActionBar : MonoBehaviour
    {
        private const float Cell = 64f;
        private const int Spells = 5;

        private SandboxContext _context;
        private SandboxPlayer _player;
        private AutoAttack _attack;
        private SignatureCaster _signature;
        private SpellCaster _spells;
        private UiSlot _attackSlot;
        private UiSlot _signatureSlot;
        private readonly UiSlot[] _spellSlots = new UiSlot[Spells];

        public void Init(UiRoot root, SandboxContext context, SandboxPlayer player)
        {
            _context = context;
            _player = player;
            _attack = player.GetComponent<AutoAttack>();
            _signature = player.GetComponent<SignatureCaster>();
            _spells = player.GetComponent<SpellCaster>();
            float width = (Spells + 2) * Cell + 30f;
            var bar = UiFactory.Panel(root.Rect, "BarraAccion", Vector2.zero, new Vector2(width, Cell + 12f), UiTheme.Background);
            bar.anchorMin = bar.anchorMax = bar.pivot = new Vector2(0.5f, 0f);
            bar.anchoredPosition = new Vector2(0f, 28f);
            _attackSlot = UiSlot.Create(bar, new Vector2(8f, 6f), Cell - 4f, "F");
            _attackSlot.Hover.Text = () => "<size=16><b>Atacar</b></size>\nAtaque automático con el arma contra tu objetivo " +
                                           "(F o clic derecho sobre un enemigo).\n\n" + ItemView.WeaponLines(_attack.Weapon, _player.Stats);
            _attackSlot.Hover.LeftClick = () => { if (_attack.Active) _attack.Stop(); else _attack.Begin(); };
            _signatureSlot = UiSlot.Create(bar, new Vector2(8f + Cell, 6f), Cell - 4f, "Q");
            _signatureSlot.Hover.Text = () => _signature.Slot != null
                ? $"<b>{_signature.Slot.Definition.DisplayName}</b>\nHabilidad de firma del kit (Q)" : null;
            for (int i = 0; i < Spells; i++)
            {
                int index = i;
                var slot = UiSlot.Create(bar, new Vector2(22f + (i + 2) * Cell, 6f), Cell - 4f, (i + 1).ToString());
                slot.Hover.Text = () => index < _spells.Slots.Count
                    ? SpellView.Tooltip(_spells.Slots[index].Spell, _context.Hero.Level) : null;
                slot.Hover.LeftClick = () => _spells.Cast(index);
                _spellSlots[i] = slot;
            }
        }

        private void Update()
        {
            if (_player == null || _player.Kit == null) return;
            bool pulse = _attack.Active && Mathf.PingPong(Time.time * 2f, 1f) > 0.5f;
            _attackSlot.Set("At", new Color(0.45f, 0.3f, 0.2f), _attack.Active ? (pulse ? Color.white : UiTheme.Gold) : UiTheme.GoldDim);
            _attackSlot.SetCooldown(_attack.Active ? _attack.SwingFraction : 0f);
            if (_signature.Slot != null)
            {
                _signatureSlot.Set(ItemView.Glyph(_signature.Slot.Definition.DisplayName), new Color(0.55f, 0.4f, 0.1f), UiTheme.Gold);
                _signatureSlot.SetCooldown(_signature.Slot.Cooldown.Fraction);
            }
            int level = _context.Hero.Level;
            for (int i = 0; i < Spells; i++)
            {
                if (i >= _spells.Slots.Count)
                {
                    _spellSlots[i].Clear();
                    continue;
                }
                var slot = _spells.Slots[i];
                bool known = level >= slot.Spell.Level;
                bool affordable = _player.Vitals.ClassResource.Current >= slot.Spell.Cost;
                _spellSlots[i].Set(SpellView.Glyph(slot.Spell), SpellView.Tint(slot.Spell),
                    known ? (affordable ? UiTheme.Gold : UiTheme.Bad) : UiTheme.GoldDim, 0, !known);
                _spellSlots[i].SetCooldown(known ? slot.CooldownFraction : 0f);
            }
        }
    }
}
