using System;
using NUnit.Framework;
using Specimen.Core.StateMachines;

namespace Specimen.Tests.Core.StateMachines
{
    public class StateMachineLifecycleTests : StateMachineFixture
    {
        [Test]
        public void Start_EntersInitialState()
        {
            Sm.Start(Phase.Calm);
            Assert.That(Sm.CurrentId, Is.EqualTo(Phase.Calm));
            Assert.That(Log, Is.EqualTo(new[] { "enter:calm" }));
        }

        [Test]
        public void Tick_BeforeStart_DoesNothing()
        {
            Sm.Tick(1f);
            Assert.That(Calm.Ticked, Is.Zero);
        }

        [Test]
        public void ChangeState_DuringEnter_IsRejected()
        {
            var sm = new StateMachine<Phase>();
            var reentrant = new ReentrantState(() => sm.ChangeState(Phase.Calm));
            sm.AddState(Phase.Calm, new SpyState("c", Log)).AddState(Phase.Incursion, reentrant);
            sm.Start(Phase.Calm);
            Assert.Throws<InvalidOperationException>(() => sm.ChangeState(Phase.Incursion));
        }

        [Test]
        public void InvalidConfigurations_AreRejected()
        {
            var sm = new StateMachine<Phase>().AddState(Phase.Calm, Calm);
            Assert.Throws<InvalidOperationException>(() => sm.AddState(Phase.Calm, Calm));
            Assert.Throws<InvalidOperationException>(() => sm.AddTransition(Phase.Calm, Phase.Siege, () => true));
            Assert.Throws<InvalidOperationException>(() => sm.Start(Phase.Siege));
            Assert.Throws<InvalidOperationException>(() => sm.ChangeState(Phase.Calm));
            sm.Start(Phase.Calm);
            Assert.Throws<InvalidOperationException>(() => sm.Start(Phase.Calm));
        }
    }
}
