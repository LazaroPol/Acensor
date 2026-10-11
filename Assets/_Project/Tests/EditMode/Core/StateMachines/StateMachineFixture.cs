using System;
using System.Collections.Generic;
using NUnit.Framework;
using Specimen.Core.StateMachines;

namespace Specimen.Tests.Core.StateMachines
{
    /// <summary>Base compartida: cuatro estados espía con registro de Enter/Exit.</summary>
    public abstract class StateMachineFixture
    {
        protected enum Phase { Calm, Incursion, Ascent, Siege }

        protected sealed class SpyState : StateBase
        {
            private readonly string _name;
            private readonly List<string> _log;
            public float Ticked;

            public SpyState(string name, List<string> log) { _name = name; _log = log; }
            public override void Enter() => _log.Add("enter:" + _name);
            public override void Exit() => _log.Add("exit:" + _name);
            public override void Tick(float dt) => Ticked += dt;
        }

        protected sealed class ReentrantState : StateBase
        {
            private readonly Action _onEnter;
            public ReentrantState(Action onEnter) => _onEnter = onEnter;
            public override void Enter() => _onEnter();
        }

        protected List<string> Log;
        protected SpyState Calm, Incursion, Ascent, Siege;
        protected StateMachine<Phase> Sm;

        [SetUp]
        public void SetUpMachine()
        {
            Log = new List<string>();
            Calm = new SpyState("calm", Log);
            Incursion = new SpyState("incursion", Log);
            Ascent = new SpyState("ascent", Log);
            Siege = new SpyState("siege", Log);
            Sm = new StateMachine<Phase>()
                .AddState(Phase.Calm, Calm)
                .AddState(Phase.Incursion, Incursion)
                .AddState(Phase.Ascent, Ascent)
                .AddState(Phase.Siege, Siege);
        }
    }
}
