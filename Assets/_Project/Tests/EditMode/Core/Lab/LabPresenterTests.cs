using System.Linq;
using NUnit.Framework;
using Specimen.Core.Dna;
using Specimen.Core.Lab;

namespace Specimen.Tests.Core.Lab
{
    public class LabPresenterTests
    {
        private DnaTree _tree;
        private DnaAllocation _allocation;
        private LabPresenter _lab;

        [SetUp]
        public void SetUp()
        {
            var tierLock = TierLock.CreateDefault();
            _tree = DnaTree.CreateDefault();
            _allocation = new DnaAllocation(_tree, tierLock);
            _lab = new LabPresenter(_tree, _allocation, tierLock);
        }

        private LabNodeView Node(string id) =>
            _lab.Branches().SelectMany(b => b.Nodes).Single(n => n.Id == id);

        private DnaNode FirstOf(DnaBranch branch, int tier) =>
            _tree.Nodes.Where(n => n.Branch == branch && n.Tier == tier).OrderBy(n => n.Id, System.StringComparer.Ordinal).First();

        [Test]
        public void Branches_ListsAllFiveBranchesWithNodesByTier()
        {
            var branches = _lab.Branches();
            Assert.That(branches.Count, Is.EqualTo(5));
            foreach (var b in branches)
                Assert.That(b.Nodes.Select(n => n.Tier), Is.Ordered);
        }

        [Test]
        public void NodeStates_FollowPointsAndTierLock()
        {
            var tier1 = FirstOf(DnaBranch.Potencia, 1);
            var tier2 = FirstOf(DnaBranch.Potencia, 2);
            Assert.That(Node(tier1.Id).State, Is.EqualTo(NodeState.TooExpensive));
            _allocation.AddPoints(10);
            Assert.That(Node(tier1.Id).State, Is.EqualTo(NodeState.Available));
            Assert.That(Node(tier2.Id).State, Is.EqualTo(NodeState.TierLocked));
            _lab.Select(tier1.Id);
            _lab.UnlockSelected();
            Assert.That(Node(tier1.Id).State, Is.EqualTo(NodeState.Unlocked));
        }

        [Test]
        public void UnlockSelected_ReportsMessagesInSpanish()
        {
            Assert.That(_lab.UnlockSelected(), Is.EqualTo(UnlockResult.UnknownNode));
            Assert.That(_lab.Message, Is.EqualTo("Elige una mutación."));
            var node = FirstOf(DnaBranch.Agilidad, 1);
            _lab.Select(node.Id);
            _lab.UnlockSelected();
            Assert.That(_lab.Message, Is.EqualTo("No tienes puntos de ADN suficientes."));
            _allocation.AddPoints(node.Cost);
            _lab.UnlockSelected();
            Assert.That(_lab.Message, Is.EqualTo("Mutación desbloqueada."));
            _lab.UnlockSelected();
            Assert.That(_lab.Message, Is.EqualTo("Ya tienes esta mutación."));
            Assert.That(_lab.FreePoints, Is.Zero);
        }

        [Test]
        public void Select_MarksNodeAndIgnoresUnknownIds()
        {
            var node = FirstOf(DnaBranch.Cognicion, 1);
            _lab.Select(node.Id);
            Assert.That(Node(node.Id).IsSelected, Is.True);
            _lab.Select("no-existe");
            Assert.That(_lab.SelectedId, Is.Null);
        }

        [Test]
        public void PointsMissingForSelected_ShowsTierGap()
        {
            var tier2 = FirstOf(DnaBranch.Vitalidad, 2);
            _lab.Select(tier2.Id);
            Assert.That(_lab.PointsMissingForSelected(), Is.EqualTo(5));
            _lab.Select(null);
            Assert.That(_lab.PointsMissingForSelected(), Is.Zero);
        }
    }
}
