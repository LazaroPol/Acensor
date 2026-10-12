using System;
using NUnit.Framework;
using Specimen.Core.Siege;

namespace Specimen.Tests.Core.Siege
{
    public class WaveDirectorTests
    {
        private static WaveDirector Director(int waves = 2) => new WaveDirector(new WaveConfig(waves, 2, 1, 10f));

        private static void ClearWave(WaveDirector d)
        {
            while (d.TrySpawn()) { }
            while (d.Alive > 0) d.OnEnemyKilled();
        }

        [Test]
        public void Config_RejectsInvalidValues()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new WaveConfig(0, 1, 0, 0f));
            Assert.Throws<ArgumentOutOfRangeException>(() => new WaveConfig(1, 0, 0, 0f));
            Assert.Throws<ArgumentOutOfRangeException>(() => new WaveConfig(1, 1, -1, 0f));
            Assert.Throws<ArgumentOutOfRangeException>(() => new WaveConfig(1, 1, 0, -1f));
        }

        [Test]
        public void Config_SizeGrowsPerWave_AndValidatesRange()
        {
            var c = new WaveConfig(3, 4, 2, 5f);
            Assert.That(new[] { c.SizeOf(1), c.SizeOf(2), c.SizeOf(3) }, Is.EqualTo(new[] { 4, 6, 8 }));
            Assert.Throws<ArgumentOutOfRangeException>(() => c.SizeOf(4));
            Assert.That(WaveConfig.Default.WaveCount, Is.EqualTo(5));
        }

        [Test]
        public void Director_RejectsNullConfig() => Assert.Throws<ArgumentNullException>(() => new WaveDirector(null));

        [Test]
        public void Idle_UntilStarted_AndStartsOnce()
        {
            var d = Director();
            Assert.That(d.State, Is.EqualTo(WaveState.Idle));
            Assert.That(d.TrySpawn(), Is.False);
            d.Start();
            Assert.That(d.State, Is.EqualTo(WaveState.InWave));
            Assert.That(d.CurrentWave, Is.EqualTo(1));
            Assert.Throws<InvalidOperationException>(() => d.Start());
        }

        [Test]
        public void Spawns_ExactlyTheWaveSize()
        {
            var d = Director();
            d.Start();
            Assert.That(d.TrySpawn(), Is.True);
            Assert.That(d.TrySpawn(), Is.True);
            Assert.That(d.TrySpawn(), Is.False);
            Assert.That(d.Alive, Is.EqualTo(2));
            Assert.That(d.PendingSpawns, Is.EqualTo(0));
        }

        [Test]
        public void ClearingWave_StartsIntermission_ThenNextWaveAfterPause()
        {
            var d = Director();
            var started = 0;
            d.WaveStarted += _ => started++;
            d.Start();
            ClearWave(d);
            Assert.That(d.State, Is.EqualTo(WaveState.Intermission));
            d.Tick(6f);
            Assert.That(d.State, Is.EqualTo(WaveState.Intermission));
            d.Tick(4f);
            Assert.That(d.State, Is.EqualTo(WaveState.InWave));
            Assert.That(d.CurrentWave, Is.EqualTo(2));
            Assert.That(d.PendingSpawns, Is.EqualTo(3));
            Assert.That(started, Is.EqualTo(2));
        }

        [Test]
        public void Wave_NotClearedWhileEnemiesPendingOrAlive()
        {
            var d = Director();
            d.Start();
            d.TrySpawn();
            d.OnEnemyKilled();
            Assert.That(d.State, Is.EqualTo(WaveState.InWave));
        }

        [Test]
        public void LastWaveCleared_RaisesSurvivedOnce()
        {
            var d = Director(1);
            var raised = 0;
            d.Survived += () => raised++;
            d.Start();
            ClearWave(d);
            d.Tick(100f);
            Assert.That(d.State, Is.EqualTo(WaveState.Survived));
            Assert.That(raised, Is.EqualTo(1));
            Assert.That(d.Kills, Is.EqualTo(2));
        }

        [Test]
        public void Kill_WithoutAliveEnemies_Throws()
        {
            var d = Director();
            d.Start();
            Assert.Throws<InvalidOperationException>(() => d.OnEnemyKilled());
            Assert.Throws<ArgumentOutOfRangeException>(() => d.Tick(-1f));
        }
    }
}
