using System;
using NUnit.Framework;
using Specimen.Core.Elevator;
using Specimen.Core.Stats;
using Specimen.Core.Vitals;

namespace Specimen.Tests.Core.Elevator
{
    [TestFixture]
    public class BonfireRestTests
    {
        private const float Eps = 1e-4f;
        private static readonly BonfireConfig Config = new BonfireConfig(2f, 10f, 20f);

        private sealed class FixedLevels : IModuleLevels
        {
            public int Bonfire;
            public int LevelOf(ModuleType type) => type == ModuleType.Bonfire ? Bonfire : 0;
        }

        private CharacterVitals _vitals;
        private Fatigue _fatigue;
        private FixedLevels _levels;
        private BonfireRest _rest;

        [SetUp]
        public void SetUp()
        {
            var sheet = new StatSheet();
            sheet.Define(StatType.MaxHealth, 100f);
            sheet.Define(StatType.MaxClassResource, 50f);
            sheet.Define(StatType.MaxStamina, 80f);
            _vitals = new CharacterVitals(sheet);
            _fatigue = new Fatigue(sheet.Get(StatType.MaxStamina), new FatigueConfig(1, 2, 0.1f, 5));
            _levels = new FixedLevels { Bonfire = 1 };
            _rest = new BonfireRest(_vitals, _fatigue, _levels, Config);
            _vitals.Health.Drain(50f);
            _vitals.Stamina.Drain(60f);
        }

        [Test]
        public void RegeneratesHealthAndStaminaPerSecond()
        {
            _rest.Tick(2f);
            Assert.That(_vitals.Health.Current, Is.EqualTo(54f).Within(Eps));
            Assert.That(_vitals.Stamina.Current, Is.EqualTo(40f).Within(Eps));
        }

        [Test]
        public void RegenerationScalesWithLevelAndCapsAtMax()
        {
            _levels.Bonfire = 3;
            _rest.Tick(1f);
            Assert.That(_vitals.Health.Current, Is.EqualTo(56f).Within(Eps));
            _rest.Tick(100f);
            Assert.That(_vitals.Health.IsFull && _vitals.Stamina.IsFull, Is.True);
        }

        [Test]
        public void RecoversOneFatiguePointPerIntervalAndNotifies()
        {
            _fatigue.Add(3);
            int events = 0;
            _rest.FatigueRecovered += _ => events++;
            _rest.Tick(19f);
            Assert.That(_fatigue.Points, Is.EqualTo(3));
            _rest.Tick(21f);
            Assert.That(_fatigue.Points, Is.EqualTo(1));
            Assert.That(events, Is.EqualTo(2));
        }

        [Test]
        public void HigherLevelRecoversFatigueFaster()
        {
            _fatigue.Add(5);
            _levels.Bonfire = 2;
            _rest.Tick(30f);
            Assert.That(_fatigue.Points, Is.EqualTo(2));
        }

        [Test]
        public void DoesNotBankRestWhileUnfatigued()
        {
            _rest.Tick(100f);
            _fatigue.Add(1);
            _rest.Tick(1f);
            Assert.That(_fatigue.Points, Is.EqualTo(1));
        }

        [Test]
        public void WithoutBonfireNothingHappens()
        {
            _levels.Bonfire = 0;
            _fatigue.Add(2);
            _rest.Tick(100f);
            Assert.That(_vitals.Health.Current, Is.EqualTo(50f));
            Assert.That(_fatigue.Points, Is.EqualTo(2));
            Assert.That(_rest.Level, Is.Zero);
        }

        [Test]
        public void NegativeDeltaIsRejected()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _rest.Tick(-1f));
        }

        [Test]
        public void PlatformReportsModuleLevels()
        {
            var p = ElevatorFixtures.Platform();
            var inv = ElevatorFixtures.Stocked(10, 10);
            Assert.That(p.LevelOf(ModuleType.Bonfire), Is.Zero);
            p.TryBuild(1, ModuleType.Bonfire, inv);
            p.TryUpgrade(1, inv);
            Assert.That(p.LevelOf(ModuleType.Bonfire), Is.EqualTo(2));
            Assert.That(p.LevelOf(ModuleType.Forge), Is.Zero);
        }
    }
}
