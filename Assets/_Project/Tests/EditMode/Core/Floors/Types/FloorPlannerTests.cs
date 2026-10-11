using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Specimen.Core.Floors;
using Specimen.Core.Floors.Types;

namespace Specimen.Tests.Core.Floors.Types
{
    public class FloorPlannerTests
    {
        private static readonly FloorPlanner Planner = new FloorPlanner(FloorTypeCatalog.Default);

        private static string Signature(FloorLayout l) =>
            string.Join(";", l.Rooms.Select(r => r.ToString())) + "|" +
            string.Join(";", l.Corridors.Select(c => $"{c.A}-{c.B}"));

        [Test]
        public void Plan_UsesRequestedTypeAndSeed()
        {
            var plan = Planner.Plan(FloorTypeKind.Boss, 7UL);
            Assert.That(plan.Type.Kind, Is.EqualTo(FloorTypeKind.Boss));
            Assert.That(plan.Seed, Is.EqualTo(7UL));
            Assert.That(plan.Layout.Rooms.Count, Is.EqualTo(1 + 2 + 1));
        }

        [Test]
        public void SameSeedAndType_IsReproducible()
        {
            var a = Planner.Plan(FloorTypeKind.Hybrid, 99UL);
            var b = Planner.Plan(FloorTypeKind.Hybrid, 99UL);
            Assert.That(Signature(a.Layout), Is.EqualTo(Signature(b.Layout)));
        }

        [Test]
        public void DifferentTypes_ProduceDifferentShapes()
        {
            var sizes = new[] { FloorTypeKind.Loot, FloorTypeKind.Boss, FloorTypeKind.Hybrid }
                .Select(k => Planner.Plan(k, 1UL).Layout.Rooms.Count);
            Assert.That(sizes.Distinct().Count(), Is.EqualTo(3));
        }

        [Test]
        public void UnknownType_Throws()
        {
            var planner = new FloorPlanner(new FloorTypeCatalog(new IFloorType[] { new LootFloor() }));
            Assert.Throws<KeyNotFoundException>(() => planner.Plan(FloorTypeKind.Puzzle, 1UL));
        }

        [Test]
        public void NullArguments_Throw()
        {
            Assert.Throws<ArgumentNullException>(() => new FloorPlanner(null));
            Assert.Throws<ArgumentNullException>(() => new FloorPlan(null, Planner.Plan(FloorTypeKind.Loot, 1UL).Layout));
        }
    }
}
