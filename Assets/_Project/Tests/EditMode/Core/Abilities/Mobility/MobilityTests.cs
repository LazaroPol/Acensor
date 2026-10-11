using System.Linq;
using NUnit.Framework;
using Specimen.Core.Abilities.Mobility;
using Specimen.Core.Classes;
using Specimen.Core.Movement;

namespace Specimen.Tests.Core.Abilities.Mobility
{
    [TestFixture]
    public class MobilityTests
    {
        private static readonly MobilityConfig Config = new MobilityConfig();

        private static MobilityProfile ProfileOf(ClassKitId id)
            => MobilityProfile.For(DefaultClassKits.Create().Single(k => k.Id == id), Config);

        [Test]
        public void DuelistaGetsDashAndDoubleJump()
        {
            var p = ProfileOf(ClassKitId.Duelista);
            Assert.That(p.CanDash, Is.True);
            Assert.That(p.AirJumps, Is.EqualTo(1));
            Assert.That(p.DashAbility().StaminaCost, Is.EqualTo(Config.DashStaminaCost));
            Assert.That(p.TeleportAbility(), Is.Null);
        }

        [Test]
        public void ArcanistaTeleportsWithClassResource()
        {
            var p = ProfileOf(ClassKitId.Arcanista);
            Assert.That(p.CanTeleport, Is.True);
            Assert.That(p.CanDash, Is.False);
            Assert.That(p.DashAbility(), Is.Null);
            Assert.That(p.TeleportAbility().ResourceCost, Is.EqualTo(Config.TeleportResourceCost));
        }

        [Test]
        public void VigiaClimbsAndBastionDoesNot()
        {
            Assert.That(ProfileOf(ClassKitId.Vigia).CanClimb, Is.True);
            Assert.That(ProfileOf(ClassKitId.Bastion).CanClimb, Is.False);
            Assert.That(ProfileOf(ClassKitId.Bastion).AirJumps, Is.EqualTo(0));
        }

        [Test]
        public void OnlyGlidersFallSlowerWhenGliding()
        {
            var druida = ProfileOf(ClassKitId.DruidaCeniza);
            Assert.That(druida.FallSpeed(20f, true), Is.EqualTo(5f).Within(1e-5f));
            Assert.That(druida.FallSpeed(20f, false), Is.EqualTo(20f));
            Assert.That(ProfileOf(ClassKitId.Oraculo).FallSpeed(20f, true), Is.EqualTo(20f));
        }

        [Test]
        public void AirJumpsAreSpentAndRefilledOnLanding()
        {
            var jumps = new AirJumpCounter(1);
            Assert.That(jumps.TryJump(grounded: true), Is.True);
            Assert.That(jumps.TryJump(grounded: false), Is.True);
            Assert.That(jumps.TryJump(grounded: false), Is.False);
            jumps.Land();
            Assert.That(jumps.Remaining, Is.EqualTo(1));
        }

        [Test]
        public void TeleportIsClampedToRange()
        {
            var dest = ShortTeleport.Destination(new PlanarVector(1f, 1f), new PlanarVector(1f, 21f), 6f);
            Assert.That(dest.X, Is.EqualTo(1f).Within(1e-5f));
            Assert.That(dest.Z, Is.EqualTo(7f).Within(1e-5f));
            var near = ShortTeleport.Destination(PlanarVector.Zero, new PlanarVector(3f, 4f), 6f);
            Assert.That(near.Magnitude, Is.EqualTo(5f).Within(1e-5f));
        }
    }
}
