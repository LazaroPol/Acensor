using NUnit.Framework;
using Specimen.Core.Extraction;

namespace Specimen.Tests.Core.Extraction
{
    public class BloodDrawSequenceTests
    {
        [Test]
        public void Default_RunsCalmBloodBlackoutAndFinishes()
        {
            var seq = BloodDrawSequence.CreateDefault();
            Assert.That(seq.Current.Tone, Is.EqualTo(BloodDrawTone.Calm));
            seq.Tick(3f);
            Assert.That(seq.Current.Tone, Is.EqualTo(BloodDrawTone.Blood));
            seq.Tick(2.5f);
            Assert.That(seq.Current.Tone, Is.EqualTo(BloodDrawTone.Blackout));
            Assert.That(seq.IsFinished, Is.False);
            seq.Tick(2.5f);
            Assert.That(seq.IsFinished, Is.True);
            Assert.That(seq.Progress, Is.EqualTo(1f));
        }

        [Test]
        public void Progress_IsFractionOfCurrentStep_AndCarriesOverflow()
        {
            var seq = new BloodDrawSequence(new[]
            {
                new BloodDrawStep("a", 2f, BloodDrawTone.Calm), new BloodDrawStep("b", 4f, BloodDrawTone.Blood)
            });
            seq.Tick(1f);
            Assert.That(seq.Progress, Is.EqualTo(0.5f).Within(1e-5f));
            seq.Tick(2f);
            Assert.That(seq.Current.Text, Is.EqualTo("b"));
            Assert.That(seq.Progress, Is.EqualTo(0.25f).Within(1e-5f));
            seq.Tick(100f);
            Assert.That(seq.IsFinished, Is.True);
            Assert.That(seq.Current.Text, Is.EqualTo("b"));
        }

        [Test]
        public void EmptyScript_IsRejected()
        {
            Assert.That(() => new BloodDrawSequence(new BloodDrawStep[0]), Throws.ArgumentException);
        }
    }
}
