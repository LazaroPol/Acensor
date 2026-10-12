using System;
using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Common;
using Specimen.Core.Items;
using Specimen.Core.Professions;

namespace Specimen.Core.Crafting.Knowledge
{
    /// <summary>
    /// Pergamino, plano, tratado o volumen de libro de oficio (GDD §17.3): oficio, nivel mínimo para
    /// entenderlo y recetas que enseña. Un volumen pertenece a una colección de <see cref="BookVolumes"/>.
    /// </summary>
    public sealed class KnowledgeDefinition
    {
        public const int BookVolumes = 5;
        public const int MaxTreatiseRecipes = 3;

        public string Id { get; }
        public KnowledgeType Type { get; }
        public Rarity Rarity { get; }
        public ProfessionId Profession { get; }
        public int MinLevel { get; }
        public IReadOnlyList<string> Teaches { get; }
        /// <summary>Libro al que pertenece el volumen; null si no es un volumen.</summary>
        public string CollectionId { get; }
        /// <summary>Número de volumen (1..5); 0 si no es un volumen.</summary>
        public int Volume { get; }

        public bool IsVolume => Type == KnowledgeType.BookVolume;

        public KnowledgeDefinition(string id, KnowledgeType type, Rarity rarity, ProfessionId profession,
            int minLevel, IEnumerable<string> teaches, string collectionId = null, int volume = 0)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException("Id vacío.", nameof(id));
            Id = id;
            Type = type;
            Rarity = rarity;
            Profession = profession;
            MinLevel = Guard.Positive(minLevel, nameof(minLevel));
            Teaches = RequireTeaches(type, teaches);
            (CollectionId, Volume) = RequireVolume(type, collectionId, volume);
        }

        /// <summary>Atajo para un volumen de libro: su id es «colección.volN».</summary>
        public static KnowledgeDefinition BookVolume(string collectionId, int volume, ProfessionId profession,
            int minLevel, IEnumerable<string> teaches = null) =>
            new KnowledgeDefinition($"{collectionId}.vol{volume}", KnowledgeType.BookVolume, Rarity.Epic,
                profession, minLevel, teaches, collectionId, volume);

        private static IReadOnlyList<string> RequireTeaches(KnowledgeType type, IEnumerable<string> teaches)
        {
            var list = (teaches ?? Enumerable.Empty<string>()).ToList();
            if (list.Any(string.IsNullOrWhiteSpace) || list.Distinct().Count() != list.Count)
                throw new ArgumentException("Recetas vacías o repetidas.", nameof(teaches));
            var ok = type switch
            {
                KnowledgeType.Scroll => list.Count == 1,
                KnowledgeType.Treatise => list.Count >= 1 && list.Count <= MaxTreatiseRecipes,
                KnowledgeType.BookVolume => true,
                _ => list.Count >= 1
            };
            if (!ok) throw new ArgumentException($"Número de recetas no válido para {type}.", nameof(teaches));
            return list.AsReadOnly();
        }

        private static (string, int) RequireVolume(KnowledgeType type, string collectionId, int volume)
        {
            if (type != KnowledgeType.BookVolume)
            {
                if (collectionId != null || volume != 0)
                    throw new ArgumentException("Solo los volúmenes tienen colección.", nameof(collectionId));
                return (null, 0);
            }
            if (string.IsNullOrWhiteSpace(collectionId))
                throw new ArgumentException("Volumen sin colección.", nameof(collectionId));
            if (volume < 1 || volume > BookVolumes)
                throw new ArgumentOutOfRangeException(nameof(volume), $"Volumen fuera de 1..{BookVolumes}.");
            return (collectionId, volume);
        }

        public override string ToString() => $"{Type} {Id} ({Profession} {MinLevel})";
    }
}
