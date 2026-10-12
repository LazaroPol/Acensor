using System;
using NUnit.Framework;
using Specimen.Core.Crafting.Definitions;

namespace Specimen.Tests.Core.Crafting.Definitions
{
    [TestFixture]
    public class UnlockConditionTests
    {
        private FakeKnowledge _k;

        [SetUp]
        public void SetUp() => _k = new FakeKnowledge();

        [Test]
        public void AlwaysKnownSiempreSeCumple() => Assert.IsTrue(UnlockCondition.AlwaysKnown.IsMet(_k));

        [Test]
        public void KnowledgeRequiredExigeHaberLeidoElConocimiento()
        {
            var c = UnlockCondition.KnowledgeRequired("plano.crisol");
            Assert.IsFalse(c.IsMet(_k));
            _k.Known.Add("plano.crisol");
            Assert.IsTrue(c.IsMet(_k));
        }

        [Test]
        public void CollectionCompleteExigeLaColeccionEntera()
        {
            var c = UnlockCondition.CollectionComplete("libro.herrero");
            Assert.IsFalse(c.IsMet(_k));
            _k.Completed.Add("libro.herrero");
            Assert.IsTrue(c.IsMet(_k));
        }

        [Test]
        public void YSoloSeCumpleConTodas()
        {
            var c = UnlockCondition.KnowledgeRequired("tratado").And(UnlockCondition.CollectionComplete("libro"));
            _k.Known.Add("tratado");
            Assert.IsFalse(c.IsMet(_k));
            _k.Completed.Add("libro");
            Assert.IsTrue(c.IsMet(_k));
            Assert.IsInstanceOf<AllOfCondition>(c);
        }

        [Test]
        public void DatosInvalidosFallanPronto()
        {
            Assert.Throws<ArgumentException>(() => UnlockCondition.KnowledgeRequired(" "));
            Assert.Throws<ArgumentException>(() => UnlockCondition.CollectionComplete(null));
            Assert.Throws<ArgumentException>(() => UnlockCondition.All());
            Assert.Throws<ArgumentException>(() => UnlockCondition.All(UnlockCondition.AlwaysKnown, null));
        }
    }
}
