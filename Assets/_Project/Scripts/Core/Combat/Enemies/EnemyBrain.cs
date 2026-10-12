using Specimen.Core.Abilities;
using Specimen.Core.Common;

namespace Specimen.Core.Combat.Enemies
{
    /// <summary>
    /// IA cuerpo a cuerpo: espera hasta ver al objetivo dentro del agro, lo persigue, ataca al alcance
    /// respetando el enfriamiento y lo suelta si se aleja más allá del abandono.
    /// </summary>
    public sealed class EnemyBrain
    {
        private readonly AbilityCooldown _cooldown;

        public EnemyBrain(EnemyBrainConfig config)
        {
            Config = Guard.NotNull(config, nameof(config));
            _cooldown = new AbilityCooldown(config.AttackCooldown);
        }

        public EnemyBrainConfig Config { get; }
        public bool IsEngaged { get; private set; }

        /// <summary>Al recibir daño se engancha aunque el atacante esté fuera del radio de agro (como en WoW).</summary>
        public void Provoke() => IsEngaged = true;

        public EnemyAction Decide(float distance, bool selfAlive, bool targetAlive, float deltaSeconds)
        {
            _cooldown.Tick(deltaSeconds);
            if (!selfAlive) return EnemyAction.Muerto;
            if (!targetAlive || distance > Config.LeashRange) IsEngaged = false;
            else if (distance <= Config.AggroRange) IsEngaged = true;
            if (!IsEngaged) return EnemyAction.Esperar;
            if (distance > Config.AttackRange) return EnemyAction.Perseguir;
            if (!_cooldown.IsReady) return EnemyAction.Esperar;
            _cooldown.Start();
            return EnemyAction.Atacar;
        }
    }
}
