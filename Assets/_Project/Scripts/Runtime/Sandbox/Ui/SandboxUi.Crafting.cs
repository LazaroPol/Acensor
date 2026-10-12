using Specimen.Runtime.UI;
using UnityEngine;

namespace Specimen.Runtime.Sandbox.Ui
{
    /// <summary>Ventana de mods (clic izquierdo en un objeto de la bolsa) y anuncio de subida de nivel.</summary>
    public sealed partial class SandboxUi
    {
        private void AddModWindow(UiRoot root)
        {
            var mods = Add(UiWindow.Spawn<ModWindow>(root, new Vector2(1040f, 200f), w => w.Context = _context));
            _bag.OpenMods = mods.Show;
        }

        private void OnLevelUp(int level)
        {
            _player.RefreshProgression(true);
            string spell = _player.Kit != null ? SpellCaster.LearnedText(_context.Hero.SpellbookFor(_player.Kit.Id), level) : null;
            _messages.Announce($"¡Nivel {level}!\n<size=20>+1 punto de talento{(spell != null ? " · " + spell : string.Empty)}</size>");
            _context.Feed.Add($"Has alcanzado el nivel {level}. {spell}");
        }
    }
}
