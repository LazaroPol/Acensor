using System.Linq;
using NUnit.Framework;
using Specimen.Core.Crafting;
using Specimen.Core.Crafting.Authoring;
using Specimen.Core.Crafting.Content;
using Specimen.Core.Crafting.Definitions;
using Specimen.Core.Crafting.Knowledge;
using Specimen.Core.Crafting.Mods;
using Specimen.Core.Crafting.Stations;
using Specimen.Core.Items;
using Specimen.Core.Professions;

namespace Specimen.Tests.Core.Crafting.Authoring
{
    public class AssetMapperTests
    {
        [Test]
        public void Item_ida_y_vuelta_conserva_los_campos()
        {
            var data = new ItemData { id = "lingote", name = "Lingote", category = ItemCategory.Material,
                rarity = Rarity.Uncommon, maxStack = 20, tier = 2, weight = 1.5f, tags = MaterialTags.Metal, iconKey = "ico" };
            var item = ItemAssetMapper.ToDefinition(data);
            Assert.That((item.Id, item.MaxStack, item.Tier, item.IconKey), Is.EqualTo(("lingote", 20, 2, "ico")));
            Assert.That(item.HasTag(MaterialTags.Metal), Is.True);
            var back = ItemAssetMapper.FromDefinition(item);
            Assert.That((back.name, back.rarity, back.weight, back.iconKey), Is.EqualTo(("Lingote", Rarity.Uncommon, 1.5f, "ico")));
        }

        [Test]
        public void Item_sin_icono_usa_su_id_y_no_lo_duplica_al_volver()
        {
            var item = ItemAssetMapper.ToDefinition(new ItemData { id = "x", name = "X" });
            Assert.That(item.IconKey, Is.EqualTo("x"));
            Assert.That(ItemAssetMapper.FromDefinition(item).iconKey, Is.Null);
        }

        [Test]
        public void Receta_por_etiqueta_con_desbloqueo_compuesto_hace_ida_y_vuelta()
        {
            var data = new RecipeData
            {
                id = "r", profession = ProfessionId.Herrero, station = StationType.Fragua, xp = 5, craftSeconds = 2f,
                inputs = new[] { new IngredientData { tag = MaterialTags.Metal, quantity = 2, minTier = 1 } },
                outputs = new[] { new OutputData { itemId = "lingote", quantity = 3 } },
                unlockKnowledge = new[] { "k1" }, unlockCollections = new[] { "c1" }, requiredParts = new[] { "pieza.a" }
            };
            var lingote = new ItemDefinition("lingote", "Lingote", ItemCategory.Material, Rarity.Common, maxStack: 10);
            var recipe = RecipeAssetMapper.ToDefinition(data, id => lingote);
            Assert.That(recipe.Inputs[0].IsByTag, Is.True);
            Assert.That(recipe.MainOutput.Quantity, Is.EqualTo(3));
            Assert.That(recipe.Unlock, Is.InstanceOf<AllOfCondition>());
            var back = RecipeAssetMapper.FromDefinition(recipe);
            Assert.That(back.unlockKnowledge, Is.EqualTo(new[] { "k1" }));
            Assert.That(back.unlockCollections, Is.EqualTo(new[] { "c1" }));
            Assert.That((back.inputs[0].tag, back.inputs[0].minTier, back.requiredParts[0]),
                Is.EqualTo((MaterialTags.Metal, 1, "pieza.a")));
        }

        [Test]
        public void Receta_sin_desbloqueo_es_siempre_conocida()
        {
            Assert.That(UnlockAssetMapper.ToCondition(null, new string[0]), Is.SameAs(UnlockCondition.AlwaysKnown));
            Assert.That(UnlockAssetMapper.Flatten(UnlockCondition.AlwaysKnown).knowledge, Is.Empty);
        }

        [Test]
        public void Conocimiento_volumen_hace_ida_y_vuelta()
        {
            var vol = KnowledgeDefinition.BookVolume("libro", 2, ProfessionId.Sastre, 15, new[] { "r" });
            var back = KnowledgeAssetMapper.ToDefinition(KnowledgeAssetMapper.FromDefinition(vol));
            Assert.That((back.Id, back.CollectionId, back.Volume, back.Teaches.Single()),
                Is.EqualTo(("libro.vol2", "libro", 2, "r")));
        }

        [Test]
        public void Pergamino_con_coleccion_vacia_no_falla()
        {
            var data = new KnowledgeData { id = "p", type = KnowledgeType.Scroll, teaches = new[] { "r" }, collectionId = "" };
            Assert.That(KnowledgeAssetMapper.ToDefinition(data).CollectionId, Is.Null);
        }

        [Test]
        public void Pieza_hace_ida_y_vuelta_con_su_item()
        {
            var entry = DefaultStationParts.All.First();
            var back = StationPartAssetMapper.ToEntry(StationPartAssetMapper.FromEntry(entry));
            Assert.That((back.Id, back.Name, back.Enables, back.Definition.Station),
                Is.EqualTo((entry.Id, entry.Name, entry.Enables, entry.Definition.Station)));
            Assert.That(back.Item.Weight, Is.EqualTo(entry.Item.Weight));
        }

        [Test]
        public void Mod_con_requisitos_hace_ida_y_vuelta()
        {
            var mod = new ModDefinition("mod.x", "X", ModSlotType.Filo, GearTags.Arma,
                new[] { GearModifier.Percent(GearStat.DañoCortante, 0.1f) },
                new ModRequirements(Quality.Good, ProfessionId.Herrero, 5), removable: false, weight: 0.2f);
            var back = ModAssetMapper.ToDefinition(ModAssetMapper.FromDefinition(mod));
            Assert.That((back.Requirements.Profession, back.Requirements.MinLevel, back.Removable, back.Item.Weight),
                Is.EqualTo(((ProfessionId?)ProfessionId.Herrero, 5, false, 0.2f)));
            Assert.That(back.Modifiers.Single().Value, Is.EqualTo(0.1f));
        }

        [Test]
        public void Curva_y_calidad_por_defecto_hacen_ida_y_vuelta()
        {
            var data = CurveAssetMapper.FromCurve(ProfessionCurve.Default, ProfessionMilestones.Default);
            Assert.That(CurveAssetMapper.ToCurve(data).XpToNext(7), Is.EqualTo(ProfessionCurve.Default.XpToNext(7)));
            Assert.That(CurveAssetMapper.ToMilestones(data).For(12).MaxQuality,
                Is.EqualTo(ProfessionMilestones.Default.For(12).MaxQuality));
            var rules = QualityAssetMapper.ToRules(QualityAssetMapper.FromRules(QualityRules.Default));
            Assert.That(rules.FromScore(22), Is.EqualTo(QualityRules.Default.FromScore(22)));
            Assert.That(rules.Weights.PerTier, Is.EqualTo(QualityRules.Default.Weights.PerTier));
        }
    }
}
