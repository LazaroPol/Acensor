using NUnit.Framework;
using Specimen.Core.Animation;

namespace Specimen.Tests.Core.Animation
{
    [TestFixture]
    public class ClipLoopRuleTests
    {
        [TestCase("Idle_Loop", true)]
        [TestCase("Armature|Walk_Loop", true)]
        [TestCase("Zombie_Walk_Fwd_Loop", true)]
        [TestCase("Sword_Idle", true)]
        [TestCase("Roll", false)]
        [TestCase("Spell_Simple_Shoot", false)]
        [TestCase("Death01", false)]
        [TestCase("", false)]
        public void LoopsOnlyLoopClips(string clip, bool loops)
            => Assert.That(ClipLoopRule.IsLooping(clip), Is.EqualTo(loops));
    }
}
