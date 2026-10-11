using System.Collections.Generic;
using Specimen.Core.Combat.Enemies;

namespace Specimen.Core.Combat.Bosses
{
    /// <summary>Definición del jefe del Acto I (piso 5): tres fases cada vez más agresivas.</summary>
    public static class ActOneBoss
    {
        public const string Id = "guardian-acto-1";
        public const int Floor = 5;
        public const int Ring = 3;

        public static EnemyArchetype Archetype() => new EnemyArchetype(
            Id, new EnemyStats(400f, 14f, 12f), DamageType.Physical, Resistances.None);

        public static IReadOnlyList<BossPhase> Phases() => new[]
        {
            new BossPhase("vigilia", 1f, 1f),
            new BossPhase("furia", 0.6f, 1.25f),
            new BossPhase("desesperacion", 0.25f, 1.6f),
        };

        public static BossEncounter Create(EnemyFactory factory)
            => new BossEncounter(factory.Create(Archetype(), new EnemyDepth(Floor, Ring)), Phases());
    }
}
