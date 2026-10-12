using System;

namespace Specimen.Core.Animation
{
    /// <summary>
    /// Convierte la velocidad real (m/s) en el parámetro 0…1 del blend de locomoción:
    /// 0 reposo, 1/3 andar, 2/3 trotar, 1 esprintar, interpolando entre umbrales.
    /// </summary>
    public sealed class LocomotionBlend
    {
        public const float Walk = 1f / 3f;
        public const float Jog = 2f / 3f;
        public const float Sprint = 1f;

        private readonly float _walkSpeed;
        private readonly float _jogSpeed;
        private readonly float _sprintSpeed;

        public LocomotionBlend(float walkSpeed = 1.6f, float jogSpeed = 4f, float sprintSpeed = 6.4f)
        {
            if (!(walkSpeed > 0f && jogSpeed > walkSpeed && sprintSpeed > jogSpeed))
                throw new ArgumentException("Las velocidades deben ser positivas y crecientes.");
            _walkSpeed = walkSpeed;
            _jogSpeed = jogSpeed;
            _sprintSpeed = sprintSpeed;
        }

        public float Normalize(float speed)
        {
            if (float.IsNaN(speed) || speed <= 0f) return 0f;
            if (speed <= _walkSpeed) return Walk * speed / _walkSpeed;
            if (speed <= _jogSpeed) return Lerp(Walk, Jog, (speed - _walkSpeed) / (_jogSpeed - _walkSpeed));
            if (speed <= _sprintSpeed) return Lerp(Jog, Sprint, (speed - _jogSpeed) / (_sprintSpeed - _jogSpeed));
            return Sprint;
        }

        private static float Lerp(float a, float b, float t) => a + (b - a) * t;
    }
}
