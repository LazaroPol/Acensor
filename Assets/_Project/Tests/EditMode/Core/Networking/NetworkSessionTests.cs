using System;
using System.Collections.Generic;
using NUnit.Framework;
using Specimen.Core.Networking;

namespace Specimen.Tests.Core.Networking
{
    public class NetworkSessionTests
    {
        private FakeTransport _transport;
        private NetworkSession _session;
        private List<NetworkRole> _changes;

        [SetUp]
        public void SetUp()
        {
            _transport = new FakeTransport();
            _session = new NetworkSession(_transport);
            _changes = new List<NetworkRole>();
            _session.RoleChanged += _changes.Add;
        }

        private static SessionSettings Settings => new SessionSettings(SessionEndpoint.Local(), 4);

        [Test]
        public void StartsOffline_AndAuthoritative() =>
            Assert.That((_session.Role, _session.IsRunning, _session.IsAuthority), Is.EqualTo((NetworkRole.Offline, false, true)));

        [Test]
        public void StartHost_PassesSettings_AndNotifies()
        {
            var settings = Settings;
            Assert.That(_session.StartHost(settings), Is.True);
            Assert.That((_session.Role, _transport.LastSettings, _session.IsAuthority), Is.EqualTo((NetworkRole.Host, settings, true)));
            Assert.That(_changes, Is.EqualTo(new[] { NetworkRole.Host }));
        }

        [Test]
        public void StartClient_IsNotAuthority()
        {
            var endpoint = new SessionEndpoint("192.168.1.5", 7777);
            _session.StartClient(endpoint);
            Assert.That((_session.Role, _session.IsAuthority, _transport.LastEndpoint), Is.EqualTo((NetworkRole.Client, false, endpoint)));
        }

        [Test]
        public void StartServer_SetsServerRole()
        {
            _session.StartServer(Settings);
            Assert.That(_session.Role, Is.EqualTo(NetworkRole.Server));
        }

        [Test]
        public void FailedStart_StaysOffline_WithoutNotifying()
        {
            _transport.Succeeds = false;
            Assert.That(_session.StartHost(Settings), Is.False);
            Assert.That((_session.Role, _changes.Count), Is.EqualTo((NetworkRole.Offline, 0)));
        }

        [Test]
        public void CannotStartTwice()
        {
            _session.StartHost(Settings);
            Assert.Throws<InvalidOperationException>(() => _session.StartClient(SessionEndpoint.Local()));
        }

        [Test]
        public void Shutdown_ReturnsToOffline_AndAllowsRestart()
        {
            _session.StartHost(Settings);
            _session.Shutdown();
            Assert.That((_session.Role, _transport.Shutdowns), Is.EqualTo((NetworkRole.Offline, 1)));
            Assert.That(_session.StartClient(SessionEndpoint.Local()), Is.True);
        }

        [Test]
        public void Shutdown_WhenOffline_DoesNothing()
        {
            _session.Shutdown();
            Assert.That((_transport.Shutdowns, _changes.Count), Is.EqualTo((0, 0)));
        }

        [Test]
        public void NotifyDisconnected_GoesOffline_WithoutCallingTransport()
        {
            _session.StartClient(SessionEndpoint.Local());
            _session.NotifyDisconnected();
            Assert.That((_session.Role, _transport.Shutdowns), Is.EqualTo((NetworkRole.Offline, 0)));
            Assert.That(_changes, Is.EqualTo(new[] { NetworkRole.Client, NetworkRole.Offline }));
        }

        [Test]
        public void RequiresTransport_AndArguments()
        {
            Assert.Throws<ArgumentNullException>(() => new NetworkSession(null));
            Assert.Throws<ArgumentNullException>(() => _session.StartHost(null));
        }
    }
}
