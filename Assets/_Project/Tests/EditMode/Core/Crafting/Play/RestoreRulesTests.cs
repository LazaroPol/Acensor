using NUnit.Framework;
using Specimen.Core.Crafting.Definitions;
using Specimen.Core.Crafting.Play;
using Specimen.Core.Crafting.Save;
using Specimen.Core.Professions;

namespace Specimen.Tests.Core.Crafting.Play
{
    /// <summary>OF-16: reglas pequeñas de la carga: una estación por tipo y oficios ajustados a la curva.</summary>
    [TestFixture]
    public class RestoreRulesTests
    {
        [Test]
        public void HayUnaSolaEstacionPorTipo()
        {
            var play = new PlayFixtures();
            Assert.AreSame(play.Stations.Create(StationType.Fragua), play.Stations.Create(StationType.Fragua));
            Assert.AreEqual(1, play.Stations.Sessions.Count);
        }

        [Test]
        public void RestaurarUnOficioAjustaNivelYXpALaCurva()
        {
            var skill = new ProfessionSkill(ProfessionId.Herrero);
            skill.Restore(999, 5);
            Assert.AreEqual(skill.Curve.MaxLevel, skill.Level);
            Assert.AreEqual(0, skill.Xp);
            skill.Restore(0, -4);
            Assert.AreEqual(1, skill.Level);
            Assert.AreEqual(0, skill.Xp);
        }

        [Test]
        public void UnGuardadoVacioNoVaciaLaBolsa()
        {
            var play = new PlayFixtures();
            play.GoodPalo();
            new CraftingRestorer(PlayFixtures.Catalog).Apply(new CraftingSaveData(), play.Profile, play.Stations);
            Assert.AreEqual(1, play.Profile.Bag.CountOf("palo_madera"));
        }
    }
}
