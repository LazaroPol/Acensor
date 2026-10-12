using NUnit.Framework;
using Specimen.Core.Art;
using Specimen.Core.Composition;

namespace Specimen.Tests.Core.Composition
{
    public class ExtractionFlowTests
    {
        private sealed class NoLoader : ISceneLoader { public void Load(SceneId scene) { } }

        [Test]
        public void Attic_LeadsToExtraction_ThenLab_ThenArmory()
        {
            var flow = GameComposition.Build(new NoLoader()).Resolve<SceneFlow>();
            flow.GoTo(SceneId.Ascensor);
            flow.GoTo(SceneId.Atico);
            Assert.That(flow.GoTo(SceneId.Extraccion), Is.True);
            Assert.That(flow.GoTo(SceneId.Ascensor), Is.False);
            Assert.That(flow.GoTo(SceneId.Laboratorio), Is.True);
            Assert.That(flow.GoTo(SceneId.Armeria), Is.True);
        }

        [Test]
        public void Extraction_UsesTheLabMood()
        {
            Assert.That(MoodSelector.For(SceneId.Extraccion), Is.EqualTo(ColorMood.Laboratorio));
        }
    }
}
