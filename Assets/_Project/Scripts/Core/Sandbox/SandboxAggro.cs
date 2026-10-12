using Specimen.Core.Combat.Enemies;

namespace Specimen.Core.Sandbox
{
    /// <summary>
    /// IA más tranquila para el Campo de pruebas: detectan al jugador a 5 m (no a 9), caminan algo más despacio y,
    /// si les pegas desde lejos, te persiguen hasta 28 m antes de rendirse.
    /// </summary>
    public static class SandboxAggro
    {
        public const float AggroRange = 5f;
        public const float LeashRange = 28f;
        public const float MoveSpeed = 2.3f;

        public static EnemyBrainConfig Config() => new EnemyBrainConfig(AggroRange, LeashRange, moveSpeed: MoveSpeed);
    }
}
