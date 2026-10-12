namespace Specimen.Core.Hud
{
    public enum NoticeSeverity
    {
        Info,
        Warning,
        Danger
    }

    /// <summary>Aviso temporal del HUD (texto ya listo para mostrar).</summary>
    public readonly struct HudNotice
    {
        public HudNotice(string text, NoticeSeverity severity, float secondsLeft)
        {
            Text = text;
            Severity = severity;
            SecondsLeft = secondsLeft;
        }

        public string Text { get; }
        public NoticeSeverity Severity { get; }
        public float SecondsLeft { get; }
    }
}
