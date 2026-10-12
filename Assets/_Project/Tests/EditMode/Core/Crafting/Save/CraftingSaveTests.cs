using NUnit.Framework;
using Specimen.Core.Crafting.Save;
using Specimen.Core.Items;

namespace Specimen.Tests.Core.Crafting.Save
{
    [TestFixture]
    public class CraftingSaveTests
    {
        [Test]
        public void SaveThenLoadReproducesState()
        {
            var store = new InMemoryCraftingSaveStore();
            var save = new CraftingSave(store);
            save.Save(CraftingSaveFixtures.Full());
            Assert.That(CraftingSaveWriter.Write(save.Load()), Is.EqualTo(store.Text));
            Assert.That(save.Load().Items[0].InstanceId, Is.EqualTo("ab12"));
        }

        [Test]
        public void LoadWithoutSaveIsEmpty()
            => Assert.That(new CraftingSave(new InMemoryCraftingSaveStore()).Load().Jobs, Is.Empty);

        [Test]
        public void CorruptSaveThrowsKeepsOriginalAndBacksUp()
        {
            var store = new InMemoryCraftingSaveStore { Text = "oficios v1\nskill=roto" };
            Assert.Throws<CraftingSaveFormatException>(() => new CraftingSave(store).Load());
            Assert.That(store.Text, Is.EqualTo("oficios v1\nskill=roto"));
            Assert.That(store.Backup, Is.EqualTo("oficios v1\nskill=roto"));
        }

        [Test]
        public void UnsaveableDataDoesNotOverwritePreviousSave()
        {
            var store = new InMemoryCraftingSaveStore();
            var save = new CraftingSave(store);
            save.Save(CraftingSaveFixtures.Full());
            var previous = store.Text;
            var bad = new CraftingSaveData();
            bad.KnownRecipes.Add("con=igual");
            Assert.Throws<CraftingSaveFormatException>(() => save.Save(bad));
            Assert.That(store.Text, Is.EqualTo(previous));
            Assert.That(store.Backup, Is.Null);
        }

        [Test]
        public void NullStoreIsRejected()
            => Assert.That(() => new CraftingSave(null), Throws.Exception);
    }
}
