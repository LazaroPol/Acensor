namespace Specimen.Core.Animation
{
    /// <summary>
    /// Conjuntos por defecto con los clips de Universal Animation Library 1/2 (Quaternius) y, como
    /// alternativa, los de Kevin Iglesias (prefijo «HumanM@»).
    /// </summary>
    public static class DefaultAnimationSets
    {
        public static AnimationSet Heroe() => new AnimationSet("Heroe")
            .With(AnimSlot.Reposo, "Idle_Loop", "Sword_Idle", "HumanM@Idle01")
            .With(AnimSlot.Andar, "Walk_Loop", "Walk_Formal_Loop")
            .With(AnimSlot.Trotar, "Jog_Fwd_Loop", "HumanM@Run01_Forward")
            .With(AnimSlot.Esprintar, "Sprint_Loop", "Jog_Fwd_Loop")
            .With(AnimSlot.Esquiva, "Roll", "Slide_Start")
            .With(AnimSlot.AtaqueMele, "Sword_Regular_A", "HumanM@Attack1H01_R", "Sword_Attack", "Punch_Jab")
            .With(AnimSlot.Hechizo, "Spell_Simple_Shoot", "HumanM@MagicAttackDirect1H01_R - Cast")
            .With(AnimSlot.Golpe, "Hit_Chest", "HumanM@CombatDamage01", "Hit_Head")
            .With(AnimSlot.Muerte, "Death01", "HumanM@Death01")
            .With(AnimSlot.Interactuar, "Interact", "Fixing_Kneeling", "PickUp_Table");

        public static AnimationSet Monstruo() => new AnimationSet("Monstruo")
            .With(AnimSlot.Reposo, "Zombie_Idle_Loop", "Idle_Loop")
            .With(AnimSlot.Andar, "Zombie_Walk_Fwd_Loop", "Walk_Loop")
            .With(AnimSlot.Trotar, "Jog_Fwd_Loop", "Zombie_Walk_Fwd_Loop")
            .With(AnimSlot.Esprintar, "Sprint_Loop", "Jog_Fwd_Loop")
            .With(AnimSlot.Esquiva, "Roll")
            .With(AnimSlot.AtaqueMele, "Zombie_Scratch", "Punch_Cross", "Punch_Jab")
            .With(AnimSlot.Hechizo, "Spell_Simple_Shoot", "OverhandThrow")
            .With(AnimSlot.Golpe, "Hit_Knockback", "Hit_Chest")
            .With(AnimSlot.Muerte, "Death01")
            .With(AnimSlot.Interactuar, "Interact");
    }
}
