using System;
using NUnit.Framework;
using Specimen.Core.Networking;

namespace Specimen.Tests.Core.Networking
{
    public class SessionSettingsTests
    {
        [Test]
        public void LocalEndpoint_UsesDefaultPort() =>
            Assert.That(SessionEndpoint.Local().ToString(), Is.EqualTo("127.0.0.1:7777"));

        [Test]
        public void Endpoint_TrimsAddress() => Assert.That(new SessionEndpoint(" 10.0.0.2 ", 9000).Address, Is.EqualTo("10.0.0.2"));

        [TestCase(0)]
        [TestCase(65536)]
        public void Endpoint_RejectsPortOutOfRange(int port) =>
            Assert.Throws<ArgumentOutOfRangeException>(() => new SessionEndpoint("host", port));

        [Test]
        public void Endpoint_RejectsEmptyAddress() => Assert.Throws<ArgumentException>(() => new SessionEndpoint(" ", 7777));

        [Test]
        public void Settings_DefaultToSixPlayers_HostKeepsOneSlot()
        {
            var s = new SessionSettings(SessionEndpoint.Local());
            Assert.That((s.MaxPlayers, s.RemoteSlotsAsHost), Is.EqualTo((6, 5)));
        }

        [TestCase(0)]
        [TestCase(7)]
        public void Settings_RejectPlayersOutOfRange(int players) =>
            Assert.Throws<ArgumentOutOfRangeException>(() => new SessionSettings(SessionEndpoint.Local(), players));

        [Test]
        public void Settings_RequireEndpoint() => Assert.Throws<ArgumentNullException>(() => new SessionSettings(null));

        [TestCase(NetworkRole.Offline, true, true)]
        [TestCase(NetworkRole.Host, true, true)]
        [TestCase(NetworkRole.Server, true, false)]
        [TestCase(NetworkRole.Client, false, true)]
        public void Authority_FollowsServerAuthoritativeModel(NetworkRole role, bool authority, bool localPlayer) =>
            Assert.That((NetworkAuthority.IsAuthority(role), NetworkAuthority.HasLocalPlayer(role)), Is.EqualTo((authority, localPlayer)));
    }
}
