namespace Specimen.Core.Combat.Perception
{
    /// <summary>Cómo ha percibido un enemigo a un objetivo. Ver tiene prioridad sobre oír.</summary>
    public enum Awareness
    {
        None,
        Heard,
        Seen,
    }

    /// <summary>Un sentido: decide si el observador detecta al objetivo con un alcance ya modificado.</summary>
    public interface ISense
    {
        bool Detects(Observer observer, PerceivedTarget target, float range);
    }

    /// <summary>Visión en cono: dentro del alcance y del semiángulo de la mirada.</summary>
    public sealed class VisionSense : ISense
    {
        private readonly float _halfAngle;

        public VisionSense(float halfAngle) => _halfAngle = halfAngle;

        public bool Detects(Observer observer, PerceivedTarget target, float range) =>
            observer.ToPoint(target.Position).Magnitude <= range && observer.AngleTo(target.Position) <= _halfAngle;
    }

    /// <summary>Oído omnidireccional: el ruido del objetivo (0-1) escala el alcance auditivo.</summary>
    public sealed class HearingSense : ISense
    {
        public bool Detects(Observer observer, PerceivedTarget target, float range) =>
            target.Noise > 0f && observer.ToPoint(target.Position).Magnitude <= range * target.Noise;
    }
}
