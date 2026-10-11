using NUnit.Framework;
using Specimen.Core.Iteration;
using Specimen.Tests.Core.TestDoubles;

namespace Specimen.Tests.Core.Iteration
{
    /// <summary>Iteración corta: 2 pisos, Calma 10 s, Incursión 30 s, avisos a 20 s y 5 s.</summary>
    public abstract class IterationRunFixture
    {
        protected const float Calm = 10f;
        protected const float Incursion = 30f;

        protected static readonly int[] Players = { 1, 2 };
        protected RecordingPublisher Events;
        protected IterationRun Run;

        [SetUp]
        public void SetUpRun()
        {
            var config = new IterationConfigBuilder()
                .WithFloors(2).WithCalm(Calm).WithIncursion(Incursion).WithDoorWarnings(20f, 5f).Build();
            Events = new RecordingPublisher();
            Run = new IterationRun(config, Events, Players);
        }

        /// <summary>Avanza el tiempo y deja que la máquina evalúe transiciones en el tick siguiente.</summary>
        protected void Advance(float seconds)
        {
            Run.Tick(seconds);
            Run.Tick(0f);
        }

        protected void EnterFirstCalm()
        {
            Run.Start();
            Run.LeaveArmory();
            Run.Tick(0f);
        }

        protected void PlayFloor()
        {
            Advance(Calm);
            Advance(Incursion);
            Run.Tick(0f);
        }
    }
}
