using System;
using System.Linq;
using NUnit.Framework;
using Specimen.Core.Floors;
using Specimen.Core.Floors.Remnants;

namespace Specimen.Tests.Core.Floors.Remnants
{
    public class RemnantPlacerTests
    {
        private static readonly RingCorridorGenerator Generator = new RingCorridorGenerator(RingLayoutConfig.Default);
        private static readonly RemnantPlacer Placer = new RemnantPlacer(RemnantRules.Default);
        private static readonly RingRemnantOdds Always = new RingRemnantOdds(1f, 1f);
        private static readonly RemnantPlacer Generous = new RemnantPlacer(new RemnantRules(new[] { Always, Always, Always }));

        private static string Signature(FloorRemnants r) =>
            string.Join(";", r.Remnants) + "|" + string.Join(";", r.Fragments);

        [Test]
        public void SameLayout_ProducesSameRemnants()
        {
            Assert.That(Signature(Placer.Place(Generator.Generate(7), new MemoryFragmentLog(20))),
                Is.EqualTo(Signature(Placer.Place(Generator.Generate(7), new MemoryFragmentLog(20)))));
        }

        [Test]
        public void DifferentSeeds_VaryTheRemnants()
        {
            var distinct = Enumerable.Range(1, 10)
                .Select(s => Signature(Placer.Place(Generator.Generate((ulong)s), new MemoryFragmentLog(20)))).Distinct();
            Assert.That(distinct.Count(), Is.GreaterThan(5));
        }

        [Test]
        public void OnlyChambers_GetRemnantsAndFragments([Range(1, 10)] int seed)
        {
            var layout = Generator.Generate((ulong)seed);
            var result = Generous.Place(layout, new MemoryFragmentLog(100));
            int chambers = layout.Rooms.Count(r => r.Kind == RoomKind.Chamber);
            Assert.That(result.Remnants.Count, Is.EqualTo(chambers));
            Assert.That(result.Fragments.Count, Is.EqualTo(chambers));
            Assert.That(result.Remnants.All(r => layout.Rooms[r.RoomId].Kind == RoomKind.Chamber), Is.True);
            Assert.That(result.RemnantsIn(0), Is.Empty);
            Assert.That(result.FragmentsIn(0), Is.Empty);
        }

        [Test]
        public void Fragments_AreUnique_AndLimitedByThePool()
        {
            var log = new MemoryFragmentLog(3);
            var result = Generous.Place(Generator.Generate(3), log);
            var ids = result.Fragments.Select(f => f.FragmentId).ToList();
            Assert.That(ids.Count, Is.EqualTo(3));
            Assert.That(ids, Is.Unique);
        }

        [Test]
        public void CollectedFragments_NeverReappear([Range(1, 10)] int seed)
        {
            var log = new MemoryFragmentLog(10);
            for (int id = 0; id < 10; id += 2) log.Collect(id);
            var result = Generous.Place(Generator.Generate((ulong)seed), log);
            Assert.That(result.Fragments.All(f => !log.Has(f.FragmentId)), Is.True);
        }

        [Test]
        public void Diaries_AreInYourHandwriting()
        {
            Assert.That(new Remnant(1, RemnantKind.Diary).IsInYourHandwriting, Is.True);
            Assert.That(new Remnant(1, RemnantKind.Corpse).IsInYourHandwriting, Is.False);
        }

        [Test]
        public void Place_RejectsNulls()
        {
            Assert.Throws<ArgumentNullException>(() => new RemnantPlacer(null));
            Assert.Throws<ArgumentNullException>(() => Placer.Place(null, new MemoryFragmentLog(1)));
            Assert.Throws<ArgumentNullException>(() => Placer.Place(Generator.Generate(1), null));
        }
    }
}
