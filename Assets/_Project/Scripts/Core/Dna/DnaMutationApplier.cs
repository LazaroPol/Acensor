using System;
using Specimen.Core.Stats;

namespace Specimen.Core.Dna
{
    /// <summary>Aplica las mutaciones desbloqueadas a la hoja de atributos de un clon nuevo.</summary>
    public sealed class DnaMutationApplier
    {
        private readonly DnaTree _tree;
        private readonly IDnaEffectCatalog _effects;

        public DnaMutationApplier(DnaTree tree, IDnaEffectCatalog effects)
        {
            _tree = tree ?? throw new ArgumentNullException(nameof(tree));
            _effects = effects ?? throw new ArgumentNullException(nameof(effects));
        }

        /// <summary>Retira lo aplicado antes (idempotente) y aplica cada nodo desbloqueado. Devuelve los modificadores añadidos.</summary>
        public int Apply(StatSheet sheet, DnaAllocation dna)
        {
            if (sheet == null) throw new ArgumentNullException(nameof(sheet));
            if (dna == null) throw new ArgumentNullException(nameof(dna));
            sheet.RemoveAllFromSource(this);
            var added = 0;
            foreach (var id in dna.Unlocked)
            {
                if (!_tree.TryGet(id, out var node)) continue;
                foreach (var effect in _effects.EffectsOf(node))
                {
                    if (!sheet.Has(effect.Stat)) continue;
                    sheet.AddModifier(effect.Stat, StatModifier.Percent(effect.Percent, this));
                    added++;
                }
            }
            return added;
        }
    }
}
