namespace Specimen.Core.Hud
{
    /// <summary>Barra de un recurso del HUD: etiqueta, valores y fracción llena (0..1).</summary>
    public readonly struct HudBar
    {
        public HudBar(string label, float current, float max)
        {
            Label = label;
            Current = current;
            Max = max;
        }

        public string Label { get; }
        public float Current { get; }
        public float Max { get; }
        public float Fraction => Max > 0f ? System.Math.Min(1f, System.Math.Max(0f, Current / Max)) : 0f;
    }
}
