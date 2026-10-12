using System;
using NUnit.Framework;
using Specimen.Core.Art;
using Specimen.Core.Composition;
using Specimen.Core.Iteration.Presence;

namespace Specimen.Tests.Core.Art
{
    public class MoodTests
    {
        [Test]
        public void Profiles_RespectTheDualityOfTheGdd()
        {
            var refuge = MoodProfiles.Of(ColorMood.Refugio);
            var floor = MoodProfiles.Of(ColorMood.Piso);
            var trapped = MoodProfiles.Of(ColorMood.Atrapado);
            Assert.That(refuge.Saturation, Is.GreaterThan(0f));
            Assert.That(refuge.Temperature, Is.GreaterThan(0f));
            Assert.That(floor.Saturation, Is.LessThan(0f));
            Assert.That(floor.Temperature, Is.LessThan(0f));
            Assert.That(floor.FogDensity, Is.GreaterThan(refuge.FogDensity));
            Assert.That(trapped.Exposure, Is.GreaterThan(refuge.Exposure));
        }

        [Test]
        public void Lerp_InterpolatesAndClamps()
        {
            var a = MoodProfiles.Of(ColorMood.Refugio);
            var b = MoodProfiles.Of(ColorMood.Piso);
            Assert.That(MoodProfile.Lerp(a, b, 0.5f).Saturation, Is.EqualTo(-5f).Within(1e-4));
            Assert.That(MoodProfile.Lerp(a, b, 9f).Saturation, Is.EqualTo(b.Saturation));
            Assert.That(MoodProfile.Lerp(a, b, -1f).Temperature, Is.EqualTo(a.Temperature));
        }

        [Test]
        public void Selector_MapsLocationsAndScenes()
        {
            Assert.That(MoodSelector.For(PlayerLocation.InElevator), Is.EqualTo(ColorMood.Refugio));
            Assert.That(MoodSelector.For(PlayerLocation.InFloor), Is.EqualTo(ColorMood.Piso));
            Assert.That(MoodSelector.For(PlayerLocation.Trapped), Is.EqualTo(ColorMood.Atrapado));
            Assert.That(MoodSelector.For(SceneId.Armeria), Is.EqualTo(ColorMood.Refugio));
            Assert.That(MoodSelector.For(SceneId.Laboratorio), Is.EqualTo(ColorMood.Laboratorio));
            Assert.That(MoodSelector.For(SceneId.Incursion), Is.EqualTo(ColorMood.Piso));
        }

        [Test]
        public void Transition_BlendsOverDurationFromWhatIsVisible()
        {
            var t = new MoodTransition(ColorMood.Refugio, 2f);
            t.SetTarget(ColorMood.Piso);
            Assert.That(t.IsBlending, Is.True);
            t.Tick(1f);
            Assert.That(t.Current.Saturation, Is.EqualTo(-5f).Within(1e-4));
            t.SetTarget(ColorMood.Atrapado);
            Assert.That(t.Current.Saturation, Is.EqualTo(-5f).Within(1e-4));
            t.Tick(5f);
            Assert.That(t.IsBlending, Is.False);
            Assert.That(t.Current.Exposure, Is.EqualTo(MoodProfiles.Of(ColorMood.Atrapado).Exposure));
        }

        [Test]
        public void Transition_RejectsNonPositiveDuration() =>
            Assert.Throws<ArgumentOutOfRangeException>(() => new MoodTransition(ColorMood.Piso, 0f));
    }
}
