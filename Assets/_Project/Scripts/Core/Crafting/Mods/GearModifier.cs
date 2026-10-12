namespace Specimen.Core.Crafting.Mods
{
    /// <summary>Cómo se aplica un modificador: suma plana o porcentaje sobre el total.</summary>
    public enum GearModifierKind
    {
        Flat,
        Percent
    }

    /// <summary>Modificador de un mod sobre un stat de equipo. Percent en tanto por uno (0,2 = +20 %).</summary>
    public readonly struct GearModifier
    {
        public GearModifier(GearStat stat, GearModifierKind kind, float value)
        {
            Stat = stat;
            Kind = kind;
            Value = value;
        }

        public GearStat Stat { get; }
        public GearModifierKind Kind { get; }
        public float Value { get; }

        public static GearModifier Flat(GearStat stat, float value) =>
            new GearModifier(stat, GearModifierKind.Flat, value);

        public static GearModifier Percent(GearStat stat, float value) =>
            new GearModifier(stat, GearModifierKind.Percent, value);

        public override string ToString() =>
            Kind == GearModifierKind.Flat ? $"{Value:+0.##;-0.##} {Stat}" : $"{Value * 100f:+0.##;-0.##} % {Stat}";
    }
}
