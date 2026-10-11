using System;
using NUnit.Framework;
using Specimen.Core.Iteration.Presence;

namespace Specimen.Tests.Core.Iteration.Presence
{
    public class PresenceTrackerTests
    {
        private PresenceTracker _p;

        [SetUp]
        public void SetUp() => _p = new PresenceTracker(new[] { 1, 2, 3 });

        [Test]
        public void EveryoneStartsInTheElevator() =>
            Assert.That(_p.With(PlayerLocation.InElevator), Is.EqualTo(new[] { 1, 2, 3 }));

        [Test]
        public void LeaveAndReturn_MovesBetweenElevatorAndFloor()
        {
            _p.LeaveElevator(2);
            Assert.That(_p.Location(2), Is.EqualTo(PlayerLocation.InFloor));
            _p.ReturnToElevator(2);
            Assert.That(_p.Location(2), Is.EqualTo(PlayerLocation.InElevator));
        }

        [Test]
        public void ClosingDoors_TrapsOnlyThoseOutside()
        {
            _p.LeaveElevator(1); _p.LeaveElevator(3);
            Assert.That(_p.TrapThoseOutside(), Is.EqualTo(new[] { 1, 3 }));
            Assert.That(_p.With(PlayerLocation.Trapped), Is.EqualTo(new[] { 1, 3 }));
            Assert.That(_p.NotTrapped, Is.EqualTo(new[] { 2 }));
        }

        [Test]
        public void Rescue_ReturnsTrappedPlayerToElevator()
        {
            _p.LeaveElevator(1); _p.TrapThoseOutside();
            _p.Rescue(1);
            Assert.That(_p.Location(1), Is.EqualTo(PlayerLocation.InElevator));
        }

        [Test]
        public void InvalidMoves_AreRejected()
        {
            Assert.Throws<InvalidOperationException>(() => _p.ReturnToElevator(1), "Ya está dentro");
            Assert.Throws<InvalidOperationException>(() => _p.Rescue(1), "No está atrapado");
            _p.LeaveElevator(1); _p.TrapThoseOutside();
            Assert.Throws<InvalidOperationException>(() => _p.ReturnToElevator(1), "Atrapado no vuelve por la puerta");
            Assert.Throws<ArgumentException>(() => _p.Location(9));
        }
    }
}
