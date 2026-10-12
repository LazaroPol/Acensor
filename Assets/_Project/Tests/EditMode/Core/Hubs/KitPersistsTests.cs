using NUnit.Framework;
using Specimen.Core.Armory;
using Specimen.Core.Classes;
using Specimen.Core.Composition;
using Specimen.Core.Hubs;
using Specimen.Core.Iteration;

namespace Specimen.Tests.Core.Hubs
{
    public class KitPersistsTests
    {
        private sealed class NoLoader : ISceneLoader { public void Load(SceneId scene) { } }

        [Test]
        public void ChosenKit_SurvivesLeavingTheArmoryAndTheSceneChange()
        {
            var services = GameComposition.Build(new NoLoader());
            var run = services.Resolve<IterationRun>();
            var catalog = services.Resolve<IClassKitCatalog>();
            var armory = new ArmoryPresenter(services.Resolve<ClassKitArmory>(), catalog, run, GameComposition.LocalPlayerId);
            run.Start();
            var chosen = catalog.All[2].Id;
            Assert.That(armory.Choose(chosen), Is.True);
            Assert.That(armory.Leave(), Is.True);
            services.Resolve<SceneFlow>().GoTo(SceneId.Ascensor);
            run.Tick(0.1f);
            Assert.That(run.Phase, Is.EqualTo(IterationPhase.Calm));
            Assert.That(services.Resolve<ClassKitArmory>().TryGetKit(GameComposition.LocalPlayerId, out var kit), Is.True);
            Assert.That(kit.Id, Is.EqualTo(chosen));
        }
    }
}
