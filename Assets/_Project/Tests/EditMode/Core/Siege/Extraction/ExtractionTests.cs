using System;
using NUnit.Framework;
using Specimen.Core.Siege.Extraction;
using Specimen.Core.Siege.Scoring;

namespace Specimen.Tests.Core.Siege.Extraction
{
    public class ExtractionTests
    {
        [Test]
        public void Conversion_FloorsTotalOverConstant()
        {
            var c = new DnaConversion(100f);
            Assert.That(c.ToDnaPoints(new ScoreResult(150f, 1.2f)), Is.EqualTo(1));
            Assert.That(c.ToDnaPoints(new ScoreResult(1000f, 1.5f)), Is.EqualTo(15));
            Assert.That(c.ToDnaPoints(new ScoreResult(99f, 1f)), Is.EqualTo(0));
            Assert.Throws<ArgumentOutOfRangeException>(() => new DnaConversion(0f));
            Assert.That(DnaConversion.Default.Constant, Is.EqualTo(100f));
        }

        [Test]
        public void Osprey_RejectsInvalidSetup()
        {
            Assert.Throws<ArgumentNullException>(() => new OspreyExtraction(null));
            Assert.Throws<ArgumentException>(() => new OspreyExtraction(new int[0]));
        }

        [Test]
        public void Boarding_RequiresArrival_AndKnownPlayers()
        {
            var o = new OspreyExtraction(new[] { 1, 2 });
            Assert.Throws<InvalidOperationException>(() => o.Board(1));
            Assert.Throws<InvalidOperationException>(() => o.Depart());
            o.Arrive();
            Assert.Throws<InvalidOperationException>(() => o.Arrive());
            Assert.Throws<ArgumentException>(() => o.Board(9));
        }

        [Test]
        public void Board_IsIdempotent_AndAllBoardedIsDetected()
        {
            var o = new OspreyExtraction(new[] { 1, 2, 2 });
            o.Arrive();
            Assert.That(o.Board(1), Is.True);
            Assert.That(o.Board(1), Is.False);
            Assert.That(o.AllBoarded, Is.False);
            o.Board(2);
            Assert.That(o.AllBoarded, Is.True);
        }

        [Test]
        public void Depart_ReportsBoardedAndLeftBehind_Once()
        {
            var o = new OspreyExtraction(new[] { 1, 2, 3 });
            o.Arrive();
            o.Board(2);
            var r = o.Depart();
            Assert.That(r.Boarded, Is.EqualTo(new[] { 2 }));
            Assert.That(r.LeftBehind, Is.EqualTo(new[] { 1, 3 }));
            Assert.That(r.EveryoneBoarded, Is.False);
            Assert.That(o.HasDeparted, Is.True);
            Assert.Throws<InvalidOperationException>(() => o.Depart());
            Assert.Throws<InvalidOperationException>(() => o.Board(1));
        }
    }
}
