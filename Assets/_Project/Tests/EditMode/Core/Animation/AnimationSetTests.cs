using System;
using NUnit.Framework;
using Specimen.Core.Animation;

namespace Specimen.Tests.Core.Animation
{
    [TestFixture]
    public class AnimationSetTests
    {
        private static readonly AnimationSet[] Sets = { DefaultAnimationSets.Heroe(), DefaultAnimationSets.Monstruo() };

        [Test]
        public void DefaultSetsCoverEverySlot()
        {
            foreach (var set in Sets)
                foreach (AnimSlot slot in Enum.GetValues(typeof(AnimSlot)))
                    Assert.That(set.CandidatesFor(slot), Is.Not.Empty, $"{set.Name}.{slot}");
        }

        [Test]
        public void HeroUsesSwordAndSpellClips()
        {
            var hero = DefaultAnimationSets.Heroe();
            Assert.That(hero.CandidatesFor(AnimSlot.AtaqueMele)[0], Is.EqualTo("Sword_Regular_A"));
            Assert.That(hero.CandidatesFor(AnimSlot.Hechizo)[0], Is.EqualTo("Spell_Simple_Shoot"));
        }

        [Test]
        public void MonsterWalksLikeAZombie()
            => Assert.That(DefaultAnimationSets.Monstruo().CandidatesFor(AnimSlot.Andar)[0],
                Is.EqualTo("Zombie_Walk_Fwd_Loop"));

        [Test]
        public void MissingSlotHasNoCandidates()
        {
            var set = new AnimationSet("vacio");
            Assert.That(set.Has(AnimSlot.Golpe), Is.False);
            Assert.That(set.CandidatesFor(AnimSlot.Golpe), Is.Empty);
        }

        [Test]
        public void RejectsEmptyCandidatesAndName()
        {
            Assert.Throws<ArgumentException>(() => new AnimationSet("x").With(AnimSlot.Reposo));
            Assert.Throws<ArgumentException>(() => new AnimationSet(" "));
        }

        [Test]
        public void ActionSlotsHaveTriggersAndLocomotionDoesNot()
        {
            foreach (AnimSlot slot in Enum.GetValues(typeof(AnimSlot)))
            {
                bool hasTrigger = AnimParams.TriggerOf(slot) != null;
                bool expected = !AnimParams.IsLocomotion(slot) && slot != AnimSlot.Muerte;
                Assert.That(hasTrigger, Is.EqualTo(expected), slot.ToString());
            }
        }
    }
}
