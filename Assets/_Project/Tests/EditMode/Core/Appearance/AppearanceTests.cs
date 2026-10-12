using System;
using System.Collections.Generic;
using NUnit.Framework;
using Specimen.Core.Appearance;
using Specimen.Core.Classes;

namespace Specimen.Tests.Core.Appearance
{
    [TestFixture]
    public class AppearanceTests
    {
        [Test]
        public void EveryKitHasADistinctLook()
        {
            var seen = new HashSet<string>();
            foreach (ClassKitId kit in Enum.GetValues(typeof(ClassKitId)))
            {
                var look = ClassAppearances.For(kit);
                string signature = $"{AppearanceModels.BaseModel(look)}|{AppearanceModels.OutfitModel(look)}|" +
                                   $"{look.Variant}|{string.Join(",", look.Hair)}";
                Assert.That(seen.Add(signature), Is.True, $"{kit} repite aspecto");
            }
        }

        [Test]
        public void ExplorerUsesRangerAsBaseWithoutOutfit()
        {
            var look = new CharacterAppearance(BodyType.Femenino, OutfitId.Explorador, 3);
            Assert.That(AppearanceModels.BaseModel(look), Is.EqualTo("Female_Ranger"));
            Assert.That(AppearanceModels.OutfitModel(look), Is.Null);
            Assert.That(AppearanceModels.OutfitTexture(look), Is.EqualTo("T_Ranger_3_BaseColor"));
        }

        [Test]
        public void PeasantGoesOverTheBaseBody()
        {
            var look = new CharacterAppearance(BodyType.Masculino, OutfitId.Campesino, 2);
            Assert.That(AppearanceModels.BaseModel(look), Is.EqualTo("Superhero_Male_FullBody"));
            Assert.That(AppearanceModels.OutfitModel(look), Is.EqualTo("Male_Peasant"));
            Assert.That(AppearanceModels.OutfitTexture(look), Is.EqualTo("T_Peasant_2_BaseColor"));
        }

        [Test]
        public void TightSuitHasNoOutfitNorVariantTexture()
        {
            var look = new CharacterAppearance(BodyType.Femenino, OutfitId.Ajustado, 1, "Hair_Long");
            Assert.That(AppearanceModels.OutfitModel(look), Is.Null);
            Assert.That(AppearanceModels.OutfitMaterialKey(look), Is.Null);
            Assert.That(AppearanceModels.OutfitTexture(look), Is.Null);
            Assert.That(look.Hair, Is.EqualTo(new[] { "Hair_Long" }));
        }

        [Test]
        public void BaseVariantUsesPackDefaultTexture()
            => Assert.That(AppearanceModels.OutfitTexture(ClassAppearances.For(ClassKitId.Bastion)), Is.Null);

        [Test]
        public void RejectsVariantBelowOne()
            => Assert.Throws<ArgumentOutOfRangeException>(
                () => new CharacterAppearance(BodyType.Masculino, OutfitId.Ajustado, 0));
    }
}
