using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Common;
using Specimen.Core.Crafting.Knowledge;
using Specimen.Core.Crafting.Session;

namespace Specimen.Core.Crafting.Play
{
    /// <summary>
    /// Leer pergaminos, planos y tratados desde la bolsa (GDD §17.7, clic derecho): usa el oficio del
    /// jugador, registra las recetas y consume el objeto solo si aprendió algo.
    /// </summary>
    public sealed class KnowledgeShelf
    {
        private readonly Dictionary<string, KnowledgeDefinition> _byId;
        private readonly CraftingProfile _profile;

        public KnowledgeShelf(IEnumerable<KnowledgeDefinition> knowledge, CraftingProfile profile)
        {
            _byId = Guard.NotNull(knowledge, nameof(knowledge)).ToDictionary(k => k.Id);
            _profile = Guard.NotNull(profile, nameof(profile));
        }

        public bool IsKnowledge(string itemId) => itemId != null && _byId.ContainsKey(itemId);

        public KnowledgeDefinition Find(string itemId) =>
            itemId != null && _byId.TryGetValue(itemId, out var k) ? k : null;

        /// <summary>Lee el objeto de la bolsa; null si no es un conocimiento o no lo lleva.</summary>
        public ReadResult? Read(string itemId)
        {
            var knowledge = Find(itemId);
            if (knowledge == null || _profile.Bag.CountOf(itemId) == 0) return null;
            var result = KnowledgeReader.Read(knowledge, _profile.Skill(knowledge.Profession), _profile.Knowledge);
            if (result.Consumed) _profile.Bag.Remove(itemId);
            return result;
        }

        /// <summary>Mensaje para el HUD tras leer.</summary>
        public static string Message(ReadResult result)
        {
            var k = result.Knowledge;
            string trade = StationLabels.Profession(k.Profession);
            return result.Outcome switch
            {
                ReadOutcome.Learned => $"Aprendido: {KnowledgeItems.NameOf(k)} ({k.Teaches.Count} receta(s) nuevas).",
                ReadOutcome.AlreadyKnown => "Ya conoces todo lo que enseña.",
                ReadOutcome.TooLowLevel => $"Necesitas {trade} {k.MinLevel} para entenderlo.",
                _ => $"Es un texto de {trade}."
            };
        }
    }
}
