using NUnit.Framework;
using Specimen.Core.Profile;

namespace Specimen.Tests.Core.Profile
{
    [TestFixture]
    public class ProfileSerializerTests
    {
        [Test]
        public void RoundTripKeepsData()
        {
            var data = new ProfileData(5, new[] { "b", "a" }, new[] { "f2", "f1" });
            var back = ProfileSerializer.Deserialize(ProfileSerializer.Serialize(data));
            Assert.That(back.FreeDnaPoints, Is.EqualTo(5));
            Assert.That(back.UnlockedNodes, Is.EqualTo(new[] { "a", "b" }));
            Assert.That(back.Fragments, Is.EqualTo(new[] { "f1", "f2" }));
        }

        [Test]
        public void SerializationIsDeterministic()
        {
            var a = new ProfileData(1, new[] { "x", "y" }, null);
            var b = new ProfileData(1, new[] { "y", "x" }, null);
            Assert.That(ProfileSerializer.Serialize(a), Is.EqualTo(ProfileSerializer.Serialize(b)));
        }

        [Test]
        public void ToleratesWindowsLineEndings()
            => Assert.That(ProfileSerializer.Deserialize("v1\r\npoints=2\r\n").FreeDnaPoints, Is.EqualTo(2));

        [Test]
        public void NullOrEmptyTextIsInvalid()
        {
            Assert.Throws<ProfileFormatException>(() => ProfileSerializer.Deserialize(null));
            Assert.Throws<ProfileFormatException>(() => ProfileSerializer.Deserialize("v1\n"));
        }
    }
}
