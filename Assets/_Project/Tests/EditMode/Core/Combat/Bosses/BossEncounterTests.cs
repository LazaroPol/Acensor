using System;
using System.Collections.Generic;
using NUnit.Framework;
using Specimen.Core.Combat;
using Specimen.Core.Combat.Bosses;
using Specimen.Core.Combat.Enemies;

namespace Specimen.Tests.Core.Combat.Bosses
{
    public class BossEncounterTests
    {
        private static Enemy Boss() => new Enemy(
            new EnemyArchetype("b", new EnemyStats(100f, 10f, 0f), DamageType.Physical, Resistances.None),
            new EnemyDepth(1, 1), new EnemyStats(100f, 10f, 0f));

        private static BossPhase[] Phases() => new[]
        {
            new BossPhase("a", 1f, 1f), new BossPhase("b", 0.5f, 2f), new BossPhase("c", 0.2f, 3f),
        };

        [Test]
        public void Phase_RejectsBadValues()
        {
            Assert.Throws<ArgumentException>(() => new BossPhase(" ", 1f, 1f));
            Assert.Throws<ArgumentOutOfRangeException>(() => new BossPhase("x", 1.1f, 1f));
            Assert.Throws<ArgumentOutOfRangeException>(() => new BossPhase("x", 0.5f, 0f));
        }

        [Test]
        public void Encounter_RejectsInvalidPhaseLists()
        {
            Assert.Throws<ArgumentNullException>(() => new BossEncounter(null, Phases()));
            Assert.Throws<ArgumentException>(() => new BossEncounter(Boss(), new BossPhase[0]));
            Assert.Throws<ArgumentException>(() => new BossEncounter(Boss(), new[] { new BossPhase("a", 0.9f, 1f) }));
            Assert.Throws<ArgumentException>(() => new BossEncounter(Boss(),
                new[] { new BossPhase("a", 1f, 1f), new BossPhase("b", 1f, 1f) }));
        }

        [Test]
        public void Starts_InFirstPhase_WithFullHealth()
        {
            var e = new BossEncounter(Boss(), Phases());
            Assert.That(e.CurrentPhase.Id, Is.EqualTo("a"));
            Assert.That(e.Health, Is.EqualTo(100f));
            Assert.That(e.IsDefeated, Is.False);
        }

        [Test]
        public void Damage_AdvancesPhase_AndRaisesEventOnce()
        {
            var e = new BossEncounter(Boss(), Phases());
            var seen = new List<string>();
            e.PhaseEntered += p => seen.Add(p.Id);
            e.TakeDamage(40f);
            Assert.That(e.CurrentPhase.Id, Is.EqualTo("a"));
            e.TakeDamage(10f);
            e.TakeDamage(5f);
            Assert.That(e.CurrentPhase.Id, Is.EqualTo("b"));
            Assert.That(seen, Is.EqualTo(new[] { "b" }));
        }

        [Test]
        public void BigHit_CrossesSeveralPhases_InOrder()
        {
            var e = new BossEncounter(Boss(), Phases());
            var seen = new List<string>();
            e.PhaseEntered += p => seen.Add(p.Id);
            e.TakeDamage(90f);
            Assert.That(seen, Is.EqualTo(new[] { "b", "c" }));
            Assert.That(e.CurrentPhase.Id, Is.EqualTo("c"));
        }

        [Test]
        public void Attack_UsesPhaseMultiplier()
        {
            var e = new BossEncounter(Boss(), Phases());
            Assert.That(e.Attack().Amount, Is.EqualTo(10f));
            e.TakeDamage(60f);
            Assert.That(e.Attack().Amount, Is.EqualTo(20f));
        }

        [Test]
        public void Lethal_Damage_DefeatsBoss_AndIgnoresFurtherDamage()
        {
            var e = new BossEncounter(Boss(), Phases());
            e.TakeDamage(500f);
            Assert.That(e.IsDefeated, Is.True);
            Assert.That(e.Health, Is.EqualTo(0f));
            Assert.DoesNotThrow(() => e.TakeDamage(10f));
            Assert.Throws<ArgumentOutOfRangeException>(() => e.TakeDamage(-1f));
        }

        [Test]
        public void ActOneBoss_IsScaledToFloorFive_WithThreePhases()
        {
            var factory = new EnemyFactory(new LinearDepthScaling(DepthScalingConfig.Default));
            var e = ActOneBoss.Create(factory);
            Assert.That(e.Boss.Depth.Floor, Is.EqualTo(ActOneBoss.Floor));
            Assert.That(e.Health, Is.GreaterThan(400f));
            e.TakeDamage(e.Health * 0.8f);
            Assert.That(e.CurrentPhase.Id, Is.EqualTo("desesperacion"));
        }
    }
}
