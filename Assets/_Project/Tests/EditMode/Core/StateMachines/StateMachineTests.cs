using System;
using System.Collections.Generic;
using NUnit.Framework;
using Specimen.Core.StateMachines;

namespace Specimen.Tests.Core.StateMachines
{
    public class StateMachineTests
    {
        private enum Phase { Calm, Incursion, Ascent, Siege }

        private sealed class SpyState : StateBase
        {
            private readonly string _name;
            private readonly List<string> _log;
            public float Ticked;

            public SpyState(string name, List<string> log) { _name = name; _log = log; }
            public override void Enter() => _log.Add("enter:" + _name);
            public override void Exit() => _log.Add("exit:" + _name);
            public override void Tick(float dt) => Ticked += dt;
        }

        private List<string> _log;
        private SpyState _calm, _incursion, _ascent, _siege;
        private StateMachine<Phase> _sm;

        [SetUp]
        public void SetUp()
        {
            _log = new List<string>();
            _calm = new SpyState("calm", _log);
            _incursion = new SpyState("incursion", _log);
            _ascent = new SpyState("ascent", _log);
            _siege = new SpyState("siege", _log);
            _sm = new StateMachine<Phase>()
                .AddState(Phase.Calm, _calm)
                .AddState(Phase.Incursion, _incursion)
                .AddState(Phase.Ascent, _ascent)
                .AddState(Phase.Siege, _siege);
        }

        [Test]
        public void Start_EntersInitialState()
        {
            _sm.Start(Phase.Calm);
            Assert.That(_sm.CurrentId, Is.EqualTo(Phase.Calm));
            Assert.That(_log, Is.EqualTo(new[] { "enter:calm" }));
        }

        [Test]
        public void Tick_BeforeStart_DoesNothing()
        {
            _sm.Tick(1f);
            Assert.That(_calm.Ticked, Is.Zero);
        }

        [Test]
        public void Tick_UpdatesCurrentState_WhenNoTransitionFires()
        {
            _sm.AddTransition(Phase.Calm, Phase.Incursion, () => false);
            _sm.Start(Phase.Calm);
            _sm.Tick(0.5f);
            Assert.That(_calm.Ticked, Is.EqualTo(0.5f));
        }

        [Test]
        public void Transition_ExitsThenEnters_AndRaisesEvent()
        {
            bool doorsOpen = false;
            (Phase from, Phase to) changed = default;
            _sm.AddTransition(Phase.Calm, Phase.Incursion, () => doorsOpen);
            _sm.StateChanged += (f, t) => changed = (f, t);
            _sm.Start(Phase.Calm);

            doorsOpen = true;
            _sm.Tick(1f);

            Assert.That(_sm.CurrentId, Is.EqualTo(Phase.Incursion));
            Assert.That(_log, Is.EqualTo(new[] { "enter:calm", "exit:calm", "enter:incursion" }));
            Assert.That(changed, Is.EqualTo((Phase.Calm, Phase.Incursion)));
            Assert.That(_calm.Ticked, Is.Zero, "En el tick de la transición no se actualiza el estado saliente");
        }

        [Test]
        public void OnlyFirstMatchingTransition_FiresPerTick()
        {
            _sm.AddTransition(Phase.Calm, Phase.Incursion, () => true);
            _sm.AddTransition(Phase.Calm, Phase.Siege, () => true);
            _sm.AddTransition(Phase.Incursion, Phase.Ascent, () => true);
            _sm.Start(Phase.Calm);

            _sm.Tick(0f);
            Assert.That(_sm.CurrentId, Is.EqualTo(Phase.Incursion));
            _sm.Tick(0f);
            Assert.That(_sm.CurrentId, Is.EqualTo(Phase.Ascent));
        }

        [Test]
        public void AnyTransition_HasPriority_AndDoesNotRetriggerOnTarget()
        {
            bool allDead = false;
            _sm.AddTransition(Phase.Calm, Phase.Incursion, () => true);
            _sm.AddAnyTransition(Phase.Siege, () => allDead);
            _sm.Start(Phase.Calm);

            allDead = true;
            _sm.Tick(0f);
            Assert.That(_sm.CurrentId, Is.EqualTo(Phase.Siege));

            _log.Clear();
            _sm.Tick(1f);
            Assert.That(_log, Is.Empty, "No debe re-entrar en el mismo estado");
            Assert.That(_siege.Ticked, Is.EqualTo(1f));
        }

        [Test]
        public void ChangeState_ManuallyWorks()
        {
            _sm.Start(Phase.Calm);
            _sm.ChangeState(Phase.Ascent);
            Assert.That(_sm.CurrentId, Is.EqualTo(Phase.Ascent));
        }

        [Test]
        public void ChangeState_DuringEnter_IsRejected()
        {
            var sm = new StateMachine<Phase>();
            var reentrant = new ReentrantState(() => sm.ChangeState(Phase.Calm));
            sm.AddState(Phase.Calm, new SpyState("c", _log)).AddState(Phase.Incursion, reentrant);
            sm.Start(Phase.Calm);
            Assert.Throws<InvalidOperationException>(() => sm.ChangeState(Phase.Incursion));
        }

        [Test]
        public void InvalidConfigurations_AreRejected()
        {
            var sm = new StateMachine<Phase>().AddState(Phase.Calm, _calm);
            Assert.Throws<InvalidOperationException>(() => sm.AddState(Phase.Calm, _calm));
            Assert.Throws<InvalidOperationException>(() => sm.AddTransition(Phase.Calm, Phase.Siege, () => true));
            Assert.Throws<InvalidOperationException>(() => sm.Start(Phase.Siege));
            Assert.Throws<InvalidOperationException>(() => sm.ChangeState(Phase.Calm));
            sm.Start(Phase.Calm);
            Assert.Throws<InvalidOperationException>(() => sm.Start(Phase.Calm));
        }

        private sealed class ReentrantState : StateBase
        {
            private readonly Action _onEnter;
            public ReentrantState(Action onEnter) => _onEnter = onEnter;
            public override void Enter() => _onEnter();
        }
    }
}
