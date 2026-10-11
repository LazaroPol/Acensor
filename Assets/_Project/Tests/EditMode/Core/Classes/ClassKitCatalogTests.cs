using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Specimen.Core.Classes;
using Specimen.Core.Stats;

namespace Specimen.Tests.Core.Classes
{
    [TestFixture]
    public class ClassKitCatalogTests
    {
        private static ClassKit Kit(ClassKitId id, ClassRole role = ClassRole.Dps)
            => new ClassKit(id, id.ToString(), role, ClassResourceType.Energy, ExplorationTraits.None,
                new Dictionary<StatType, float> { { StatType.MaxHealth, 100f } });

        [Test]
        public void DefaultHasSixKitsOneOfEachId()
        {
            var catalog = ClassKitCatalog.CreateDefault();
            Assert.That(catalog.All, Has.Count.EqualTo(6));
            var ids = Enum.GetValues(typeof(ClassKitId)).Cast<ClassKitId>();
            foreach (var id in ids) Assert.That(catalog.Get(id).Id, Is.EqualTo(id));
        }

        [Test]
        public void DefaultHasTwoKitsPerRole()
        {
            var catalog = ClassKitCatalog.CreateDefault();
            foreach (ClassRole role in Enum.GetValues(typeof(ClassRole)))
                Assert.That(catalog.ByRole(role), Has.Count.EqualTo(2), role.ToString());
        }

        [TestCase(ClassKitId.Bastion, ClassRole.Tank, ClassResourceType.Fury, ExplorationTraits.PushBlocks)]
        [TestCase(ClassKitId.Vigia, ClassRole.Tank, ClassResourceType.Focus, ExplorationTraits.Climb)]
        [TestCase(ClassKitId.Oraculo, ClassRole.Support, ClassResourceType.Mana, ExplorationTraits.RevealHidden)]
        [TestCase(ClassKitId.DruidaCeniza, ClassRole.Support, ClassResourceType.Essence, ExplorationTraits.Glide)]
        [TestCase(ClassKitId.Duelista, ClassRole.Dps, ClassResourceType.Energy, ExplorationTraits.Dash)]
        [TestCase(ClassKitId.Arcanista, ClassRole.Dps, ClassResourceType.Mana, ExplorationTraits.ShortTeleport)]
        public void DefaultKitsMatchGdd(ClassKitId id, ClassRole role, ClassResourceType res, ExplorationTraits trait)
        {
            var kit = ClassKitCatalog.CreateDefault().Get(id);
            Assert.That(kit.Role, Is.EqualTo(role));
            Assert.That(kit.Resource, Is.EqualTo(res));
            Assert.That(kit.HasTrait(trait), Is.True);
        }

        [Test]
        public void EveryDefaultKitDefinesAllStats()
        {
            var all = Enum.GetValues(typeof(StatType)).Cast<StatType>().ToList();
            foreach (var kit in ClassKitCatalog.CreateDefault().All)
                Assert.That(kit.BaseStats.Keys, Is.EquivalentTo(all), kit.DisplayName);
        }

        [Test]
        public void DuplicateIdIsRejected()
        {
            Assert.Throws<ArgumentException>(() =>
                new ClassKitCatalog(new[] { Kit(ClassKitId.Vigia), Kit(ClassKitId.Vigia) }));
        }

        [Test]
        public void UnknownIdThrows()
        {
            var catalog = new ClassKitCatalog(new[] { Kit(ClassKitId.Vigia) });
            Assert.Throws<KeyNotFoundException>(() => catalog.Get(ClassKitId.Bastion));
        }
    }
}
