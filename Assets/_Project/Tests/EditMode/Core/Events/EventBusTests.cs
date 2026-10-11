using System;
using System.Collections.Generic;
using NUnit.Framework;
using Specimen.Core.Events;

namespace Specimen.Tests.Core.Events
{
    public class EventBusTests
    {
        private struct DoorsClosing { public float SecondsLeft; }
        private struct PlayerDied { }

        private EventBus _bus;

        [SetUp]
        public void SetUp() => _bus = new EventBus();

        [Test]
        public void Publish_DeliversToSubscribersOfThatTypeOnly()
        {
            float received = -1; int deaths = 0;
            _bus.Subscribe<DoorsClosing>(e => received = e.SecondsLeft);
            _bus.Subscribe<PlayerDied>(_ => deaths++);

            _bus.Publish(new DoorsClosing { SecondsLeft = 15f });

            Assert.That(received, Is.EqualTo(15f));
            Assert.That(deaths, Is.Zero);
        }

        [Test]
        public void Publish_WithoutSubscribers_DoesNothing() => Assert.DoesNotThrow(() => _bus.Publish(new PlayerDied()));

        [Test]
        public void Handlers_AreCalledInSubscriptionOrder()
        {
            var order = new List<int>();
            _bus.Subscribe<PlayerDied>(_ => order.Add(1));
            _bus.Subscribe<PlayerDied>(_ => order.Add(2));
            _bus.Publish(new PlayerDied());
            Assert.That(order, Is.EqualTo(new[] { 1, 2 }));
        }

        [Test]
        public void DisposingSubscription_StopsDelivery_AndIsIdempotent()
        {
            int calls = 0;
            var sub = _bus.Subscribe<PlayerDied>(_ => calls++);
            sub.Dispose();
            sub.Dispose();
            _bus.Publish(new PlayerDied());
            Assert.That(calls, Is.Zero);
            Assert.That(_bus.SubscriberCount<PlayerDied>(), Is.Zero);
        }

        [Test]
        public void UnsubscribingDuringPublish_IsSafe()
        {
            int calls = 0;
            IDisposable sub = null;
            sub = _bus.Subscribe<PlayerDied>(_ => { calls++; sub.Dispose(); });
            _bus.Subscribe<PlayerDied>(_ => calls++);
            _bus.Publish(new PlayerDied());
            _bus.Publish(new PlayerDied());
            Assert.That(calls, Is.EqualTo(3));
        }

        [Test]
        public void SubscribingDuringPublish_TakesEffectNextPublish()
        {
            int late = 0;
            _bus.Subscribe<PlayerDied>(_ => _bus.Subscribe<PlayerDied>(__ => late++));
            _bus.Publish(new PlayerDied());
            Assert.That(late, Is.Zero);
        }

        [Test]
        public void FailingHandler_DoesNotPreventOthers_AndErrorIsReported()
        {
            bool secondCalled = false;
            _bus.Subscribe<PlayerDied>(_ => throw new InvalidOperationException("boom"));
            _bus.Subscribe<PlayerDied>(_ => secondCalled = true);

            var ex = Assert.Throws<AggregateException>(() => _bus.Publish(new PlayerDied()));
            Assert.That(secondCalled, Is.True);
            Assert.That(ex.InnerExceptions[0].Message, Is.EqualTo("boom"));
        }

        [Test]
        public void Subscribe_RejectsNullHandler() => Assert.Throws<ArgumentNullException>(() => _bus.Subscribe<PlayerDied>(null));
    }
}
