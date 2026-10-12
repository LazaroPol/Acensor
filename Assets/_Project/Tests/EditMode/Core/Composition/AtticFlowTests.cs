using NUnit.Framework;
using Specimen.Core.Art;
using Specimen.Core.Composition;

namespace Specimen.Tests.Core.Composition
{
    public class AtticFlowTests
    {
        private sealed class NoLoader : ISceneLoader { public void Load(SceneId scene) { } }

        [Test]
        public void Attic_IsReachedFromTheElevatorAndReturnsToIt()
        {
            var flow = GameComposition.Build(new NoLoader()).Resolve<SceneFlow>();
            flow.GoTo(SceneId.Ascensor);
            Assert.That(flow.GoTo(SceneId.Atico), Is.True);
            Assert.That(flow.GoTo(SceneId.Armeria), Is.False);
            Assert.That(flow.GoTo(SceneId.Ascensor), Is.True);
        }

        [Test]
        public void Attic_UsesTheFloorMood()
        {
            Assert.That(MoodSelector.For(SceneId.Atico), Is.EqualTo(ColorMood.Piso));
        }
    }
}
