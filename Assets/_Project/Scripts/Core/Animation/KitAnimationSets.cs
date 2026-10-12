using Specimen.Core.Appearance;
using Specimen.Core.Classes;

namespace Specimen.Core.Animation
{
    /// <summary>
    /// Conjunto de animación de cada kit: las de Kevin Iglesias (HumanF@/HumanM@ según el cuerpo) como
    /// principales, por empuñadura, y Universal Animation Library de respaldo (andar, voltereta, interactuar).
    /// </summary>
    public static class KitAnimationSets
    {
        public static AnimationSet For(ClassKitId kit)
        {
            string me = ClassAppearances.For(kit).Body == BodyType.Femenino ? "HumanF@" : "HumanM@";
            string other = me == "HumanF@" ? "HumanM@" : "HumanF@";
            var grip = KitGrips.For(kit);
            return new AnimationSet($"Heroe_{kit}")
                .With(AnimSlot.Reposo, Kevin(me, other, IdleOf(grip)).Add("Idle_Loop"))
                .With(AnimSlot.Andar, "Walk_Loop", "Walk_Formal_Loop")
                .With(AnimSlot.Trotar, Kevin(me, other, "Run01_Forward").Add("Jog_Fwd_Loop"))
                .With(AnimSlot.Esprintar, "Sprint_Loop", me + "Run01_Forward")
                .With(AnimSlot.Esquiva, "Roll", "Slide_Start")
                .With(AnimSlot.AtaqueMele, Kevin(me, other, AttackOf(grip)).Add("Sword_Regular_A"))
                .With(AnimSlot.Hechizo, Kevin(me, other, SpellOf(kit)).Add("Spell_Simple_Shoot"))
                .With(AnimSlot.Golpe, Kevin(me, other, grip == WeaponGrip.Magia ? "CastingDamage01" : "CombatDamage01")
                    .Add("Hit_Chest"))
                .With(AnimSlot.Muerte, Kevin(me, other, "Death01").Add("Death01"))
                .With(AnimSlot.Interactuar, "Interact", "PickUp_Table");
        }

        private static string IdleOf(WeaponGrip grip)
        {
            switch (grip)
            {
                case WeaponGrip.DosManos: return "CombatIdle2H01";
                case WeaponGrip.Asta: return "CombatIdlePolearm01";
                case WeaponGrip.UnaMano: return "CombatIdle1H01";
                default: return "Idle01";
            }
        }

        private static string AttackOf(WeaponGrip grip)
        {
            switch (grip)
            {
                case WeaponGrip.DosManos: return "Attack2H01";
                case WeaponGrip.Asta: return "AttackPolearm01";
                default: return "Attack1H01_R";
            }
        }

        private static string SpellOf(ClassKitId kit)
        {
            switch (kit)
            {
                case ClassKitId.DruidaCeniza: return "MagicAttackOmni01";
                case ClassKitId.Arcanista: return "MagicAttackDirect2H01";
                default: return "MagicAttackDirect1H01_R";
            }
        }

        private static Candidates Kevin(string me, string other, string clip) => new Candidates(me + clip, other + clip);
    }
}
