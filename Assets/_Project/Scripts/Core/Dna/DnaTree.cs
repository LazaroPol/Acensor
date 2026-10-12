using System;
using System.Collections.Generic;

namespace Specimen.Core.Dna
{
    /// <summary>Catálogo de nodos del árbol, indexado por id.</summary>
    public sealed class DnaTree
    {
        private readonly Dictionary<string, DnaNode> _nodes = new Dictionary<string, DnaNode>();

        public DnaTree(IEnumerable<DnaNode> nodes)
        {
            if (nodes == null) throw new ArgumentNullException(nameof(nodes));
            foreach (var node in nodes)
            {
                if (_nodes.ContainsKey(node.Id)) throw new ArgumentException($"Id duplicado: {node.Id}.");
                _nodes.Add(node.Id, node);
            }
        }

        public IEnumerable<DnaNode> Nodes => _nodes.Values;

        public bool TryGet(string id, out DnaNode node) => _nodes.TryGetValue(id ?? string.Empty, out node);

        /// <summary>2 nodos por nivel y rama (ids «Rama-T3-a»); coste = nivel.</summary>
        public static DnaTree CreateDefault()
        {
            var list = new List<DnaNode>();
            foreach (DnaBranch branch in Enum.GetValues(typeof(DnaBranch)))
                for (var tier = 1; tier <= 5; tier++)
                    foreach (var suffix in new[] { "a", "b" })
                        list.Add(new DnaNode($"{branch}-T{tier}-{suffix}", branch, tier, tier));
            return new DnaTree(list);
        }
    }
}
