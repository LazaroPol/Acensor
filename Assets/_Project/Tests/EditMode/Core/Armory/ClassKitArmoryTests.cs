using System;
using NUnit.Framework;
using Specimen.Core.Armory;
using Specimen.Core.Classes;

namespace Specimen.Tests.Core.Armory
{
    [TestFixture]
    public class ClassKitArmoryTests
    {
        private ClassKitArmory _armory;

        [SetUp]
        public void SetUp() => _armory = new ClassKitArmory(ClassKitCatalog.CreateDefault());

        [Test]
        public void PlayerGetsClaimedKit()
        {
            Assert.That(_armory.TryClaim(1, ClassKitId.Bastion), Is.True);
            Assert.That(_armory.TryGetKit(1, out var kit), Is.True);
            Assert.That(kit.Id, Is.EqualTo(ClassKitId.Bastion));
        }

        [Test]
        public void PlayerCannotHoldTwoKits()
        {
            _armory.TryClaim(1, ClassKitId.Bastion);
            Assert.That(_armory.TryClaim(1, ClassKitId.Vigia), Is.False);
        }

        [Test]
        public void OnlyOneDuplicateAllowedByDefault()
        {
            Assert.That(_armory.TryClaim(1, ClassKitId.Oraculo), Is.True);
            Assert.That(_armory.TryClaim(2, ClassKitId.Oraculo), Is.True);
            Assert.That(_armory.TryClaim(3, ClassKitId.Oraculo), Is.False);
            _armory.TryClaim(3, ClassKitId.Duelista);
            Assert.That(_armory.TryClaim(4, ClassKitId.Duelista), Is.False);
            Assert.That(_armory.DuplicatesInUse, Is.EqualTo(1));
        }

        [Test]
        public void ReleasingDuplicateFreesTheSlot()
        {
            _armory.TryClaim(1, ClassKitId.Oraculo);
            _armory.TryClaim(2, ClassKitId.Oraculo);
            Assert.That(_armory.Release(2), Is.True);
            Assert.That(_armory.DuplicatesInUse, Is.EqualTo(0));
            Assert.That(_armory.TryClaim(2, ClassKitId.Arcanista), Is.True);
            Assert.That(_armory.TryClaim(3, ClassKitId.Arcanista), Is.True);
        }

        [Test]
        public void ZeroDuplicatesMakesKitsUnique()
        {
            var armory = new ClassKitArmory(ClassKitCatalog.CreateDefault(), 0);
            armory.TryClaim(1, ClassKitId.Vigia);
            Assert.That(armory.TryClaim(2, ClassKitId.Vigia), Is.False);
        }

        [Test]
        public void ResetClearsClaims()
        {
            _armory.TryClaim(1, ClassKitId.Bastion);
            _armory.Reset();
            Assert.That(_armory.TryGetKit(1, out _), Is.False);
            Assert.That(_armory.CountOf(ClassKitId.Bastion), Is.EqualTo(0));
            Assert.That(_armory.Release(1), Is.False);
        }

        [Test]
        public void NullCatalogThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new ClassKitArmory(null));
        }
    }
}
