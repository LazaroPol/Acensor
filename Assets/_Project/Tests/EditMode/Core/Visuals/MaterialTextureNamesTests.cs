using NUnit.Framework;
using Specimen.Core.Visuals.Packs;

namespace Specimen.Tests.Core.Visuals
{
    [TestFixture]
    public class MaterialTextureNamesTests
    {
        [TestCase("MI_Ranger", "Ranger")]
        [TestCase("M_Brick", "Brick")]
        [TestCase("MI_Hair_1 (Instance)", "Hair_1")]
        [TestCase("Material.001", "Material")]
        [TestCase("tools", "tools")]
        [TestCase("", "")]
        public void KeyStripsPrefixesAndSuffixes(string material, string key)
            => Assert.That(MaterialTextureNames.KeyOf(material), Is.EqualTo(key));

        [Test]
        public void BaseColorPrefersVariantThenConvention()
        {
            var c = MaterialTextureNames.BaseColorCandidates("MI_Ranger", "T_Ranger_3_BaseColor");
            Assert.That(c[0], Is.EqualTo("T_Ranger_3_BaseColor"));
            Assert.That(c, Does.Contain("T_Ranger_BaseColor"));
        }

        [TestCase("MI_Regular_Male", "T_Regular_Male_Dark_BaseColor")]
        [TestCase("MI_Superhero_Male", "T_Superhero_Male_Dark")]
        [TestCase("MI_Imp", "T_Imp_BaseColor_1")]
        [TestCase("MI_Eyes", "T_Eye_Brown")]
        [TestCase("Material", "Texture")]
        [TestCase("tools", "tools_bits_texture")]
        public void PackTexturesAreAmongCandidates(string material, string texture)
            => Assert.That(MaterialTextureNames.BaseColorCandidates(material), Does.Contain(texture));

        [Test]
        public void NormalAndEmissiveFollowConvention()
        {
            Assert.That(MaterialTextureNames.NormalOf("MI_Plaster"), Is.EqualTo("T_Plaster_Normal"));
            Assert.That(MaterialTextureNames.EmissiveOf("MI_Imp"), Is.EqualTo("T_Imp_Emissive"));
        }
    }
}
