using System;
using NUnit.Framework;
using Specimen.Core.Iteration.Voting;

namespace Specimen.Tests.Core.Iteration.Voting
{
    public class MajorityVoteTests
    {
        [TestCase(1, 1, true)]
        [TestCase(2, 1, false)]
        [TestCase(3, 2, true)]
        [TestCase(4, 2, false)]
        [TestCase(4, 3, true)]
        public void Passes_WithStrictMajorityOfTheRoll(int voters, int yes, bool passed)
        {
            var vote = new MajorityVote(Range(voters));
            for (int i = 1; i <= yes; i++) vote.Cast(i, true);
            Assert.That(vote.HasPassed, Is.EqualTo(passed));
        }

        [Test]
        public void ChangingVote_ReplacesPreviousBallot()
        {
            var vote = new MajorityVote(Range(1));
            vote.Cast(1, true);
            vote.Cast(1, false);
            Assert.That((vote.YesCount, vote.HasPassed), Is.EqualTo((0, false)));
        }

        [Test]
        public void Reset_ClearsBallots()
        {
            var vote = new MajorityVote(Range(1));
            vote.Cast(1, true);
            vote.Reset();
            Assert.That(vote.YesCount, Is.Zero);
        }

        [Test]
        public void UnknownVoters_AndEmptyRoll_AreRejected()
        {
            Assert.Throws<ArgumentException>(() => new MajorityVote(new int[0]));
            Assert.Throws<ArgumentException>(() => new MajorityVote(Range(2)).Cast(7, true));
        }

        private static int[] Range(int n)
        {
            var ids = new int[n];
            for (int i = 0; i < n; i++) ids[i] = i + 1;
            return ids;
        }
    }
}
