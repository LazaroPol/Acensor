using NUnit.Framework;
using Specimen.Core.Crafting.Save;
using Specimen.Core.Items;

namespace Specimen.Tests.Core.Crafting.Save
{
    [TestFixture]
    public class CraftingSaveFormatTests
    {
        [TestCase("")]
        [TestCase("oficios v2\n")]
        [TestCase("oficios v1\nbasura")]
        [TestCase("oficios v1\nfoo=1")]
        [TestCase("oficios v1\nskill=Herrero:7")]
        [TestCase("oficios v1\nskill=Pirata:1:0")]
        [TestCase("oficios v1\nskill=3:1:0")]
        [TestCase("oficios v1\nskill=Herrero:-1:0")]
        [TestCase("oficios v1\nknown=")]
        [TestCase("oficios v1\nstation=:2")]
        [TestCase("oficios v1\njob=f:r:-3:Herrero:1")]
        [TestCase("oficios v1\njob=f:r:NaN:Herrero:1")]
        [TestCase("oficios v1\nitem=a:b:9:1:")]
        [TestCase("oficios v1\nitem=a:b:1:1:mod.a,,mod.b")]
        [TestCase("oficios v1\ninv=bolsa:1:fantasma")]
        [TestCase("oficios v1\ninv=bolsa:1:x:2:3")]
        [TestCase("oficios v1\nitem=a:b:1:1:\nitem=a:c:1:1:")]
        public void CorruptTextThrows(string text)
            => Assert.Throws<CraftingSaveFormatException>(() => CraftingSaveReader.Read(text));

        [Test]
        public void UnsaveableIdThrowsOnWrite()
        {
            var d = new CraftingSaveData();
            d.Items.Add(new ItemEntry("a:b", "x", Quality.None, 1));
            Assert.Throws<CraftingSaveFormatException>(() => CraftingSaveWriter.Write(d));
        }
    }
}
