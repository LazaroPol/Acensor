using NUnit.Framework;

namespace Specimen.Tests.Core.StateMachines
{
    public class StateMachineTransitionTests : StateMachineFixture
    {
        [Test]
        public void Tick_UpdatesCurrentState_WhenNoTransitionFires()
        {
            Sm.AddTransition(Phase.Calm, Phase.Incursion, () => false);
            Sm.Start(Phase.Calm);
            Sm.Tick(0.5f);
            Assert.That(Calm.Ticked, Is.EqualTo(0.5f));
        }

        [Test]
        public void Transition_ExitsThenEnters_AndRaisesEvent()
        {
            bool doorsOpen = false;
            (Phase from, Phase to) changed = default;
            Sm.AddTransition(Phase.Calm, Phase.Incursion, () => doorsOpen);
            Sm.StateChanged += (f, t) => changed = (f, t);
            Sm.Start(Phase.Calm);

            doorsOpen = true;
            Sm.Tick(1f);

            Assert.That(Sm.CurrentId, Is.EqualTo(Phase.Incursion));
            Assert.That(Log, Is.EqualTo(new[] { "enter:calm", "exit:calm", "enter:incursion" }));
            Assert.That(changed, Is.EqualTo((Phase.Calm, Phase.Incursion)));
            Assert.That(Calm.Ticked, Is.Zero, "En el tick de la transición no se actualiza el estado saliente");
        }

        [Test]
        public void OnlyFirstMatchingTransition_FiresPerTick()
        {
            Sm.AddTransition(Phase.Calm, Phase.Incursion, () => true);
            Sm.AddTransition(Phase.Calm, Phase.Siege, () => true);
            Sm.AddTransition(Phase.Incursion, Phase.Ascent, () => true);
            Sm.Start(Phase.Calm);

            Sm.Tick(0f);
            Assert.That(Sm.CurrentId, Is.EqualTo(Phase.Incursion));
            Sm.Tick(0f);
            Assert.That(Sm.CurrentId, Is.EqualTo(Phase.Ascent));
        }

        [Test]
        public void AnyTransition_HasPriority_AndDoesNotRetriggerOnTarget()
        {
            bool allDead = false;
            Sm.AddTransition(Phase.Calm, Phase.Incursion, () => true);
            Sm.AddAnyTransition(Phase.Siege, () => allDead);
            Sm.Start(Phase.Calm);

            allDead = true;
            Sm.Tick(0f);
            Assert.That(Sm.CurrentId, Is.EqualTo(Phase.Siege));

            Log.Clear();
            Sm.Tick(1f);
            Assert.That(Log, Is.Empty, "No debe re-entrar en el mismo estado");
            Assert.That(Siege.Ticked, Is.EqualTo(1f));
        }

        [Test]
        public void ChangeState_ManuallyWorks()
        {
            Sm.Start(Phase.Calm);
            Sm.ChangeState(Phase.Ascent);
            Assert.That(Sm.CurrentId, Is.EqualTo(Phase.Ascent));
        }
    }
}
