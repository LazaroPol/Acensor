using System;
using System.Collections.Generic;
using NUnit.Framework;
using Specimen.Core.Echoes.Voting;

namespace Specimen.Tests.Core.Echoes.Voting
{
    public class EchoVoteTests
    {
        private sealed class Fragments : IFragmentSource
        {
            private readonly Dictionary<int, int> _map = new Dictionary<int, int>();
            public Fragments Set(int voter, int n) { _map[voter] = n; return this; }
            public int FragmentsOf(int voter) => _map.TryGetValue(voter, out var n) ? n : 0;
        }

        private static EchoVote Vote(int options = 2, Fragments f = null)
            => new EchoVote(new[] { 1, 2, 3, 4 }, options, f ?? new Fragments());

        [Test]
        public void Constructor_RejectsInvalidArguments()
        {
            Assert.Throws<ArgumentException>(() => new EchoVote(new int[0], 2, new Fragments()));
            Assert.Throws<ArgumentOutOfRangeException>(() => new EchoVote(new[] { 1 }, 1, new Fragments()));
            Assert.Throws<ArgumentNullException>(() => new EchoVote(new[] { 1 }, 2, null));
        }

        [Test]
        public void Cast_RejectsUnknownVoterAndBadOption()
        {
            var v = Vote();
            Assert.Throws<ArgumentException>(() => v.Cast(9, 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => v.Cast(1, 2));
            Assert.Throws<ArgumentOutOfRangeException>(() => v.Cast(1, -1));
        }

        [Test]
        public void Resolve_WithoutBallots_Throws() => Assert.Throws<InvalidOperationException>(() => Vote().Resolve());

        [Test]
        public void Majority_Wins_WithoutTieBreak()
        {
            var v = Vote(3);
            v.Cast(1, 2); v.Cast(2, 2); v.Cast(3, 0);
            var r = v.Resolve();
            Assert.That(r.Winner, Is.EqualTo(2));
            Assert.That(r.WasTieBreak, Is.False);
            Assert.That(v.CountFor(2), Is.EqualTo(2));
            Assert.That(v.IsComplete, Is.False);
        }

        [Test]
        public void Voter_CanChangeBallot()
        {
            var v = Vote();
            v.Cast(1, 0); v.Cast(1, 1);
            Assert.That(v.CountFor(0), Is.EqualTo(0));
            Assert.That(v.Resolve().Winner, Is.EqualTo(1));
        }

        [Test]
        public void Tie_IsDecidedByVoterWithMostFragments()
        {
            var v = Vote(2, new Fragments().Set(1, 2).Set(3, 7));
            v.Cast(1, 0); v.Cast(2, 0); v.Cast(3, 1); v.Cast(4, 1);
            var r = v.Resolve();
            Assert.That(v.IsComplete, Is.True);
            Assert.That(r.Winner, Is.EqualTo(1));
            Assert.That(r.WasTieBreak, Is.True);
        }

        [Test]
        public void Tie_IgnoresOptionsOutOfTheTie()
        {
            var v = Vote(3, new Fragments().Set(1, 1).Set(2, 1).Set(4, 99));
            v.Cast(1, 0); v.Cast(2, 0); v.Cast(3, 1); v.Cast(4, 2);
            Assert.That(v.Resolve().Winner, Is.EqualTo(0));
        }

        [Test]
        public void Tie_WithEqualFragments_PicksLowestOption()
        {
            var v = Vote();
            v.Cast(1, 1); v.Cast(2, 0);
            Assert.That(v.Resolve().Winner, Is.EqualTo(0));
        }
    }
}
