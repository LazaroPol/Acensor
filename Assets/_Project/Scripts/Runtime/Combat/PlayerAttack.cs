using Specimen.Core.Classes;
using Specimen.Core.Combat;
using Specimen.Core.Combat.Attacks;
using Specimen.Core.Movement;
using Specimen.Core.Randomness;
using Specimen.Core.Stats;
using Specimen.Core.Vitals;
using Specimen.Runtime.Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Specimen.Runtime.Combat
{
    /// <summary>
    /// Ataque básico del kit (clic izquierdo o F): melé en arco o proyectil de hechizo que cobra recurso de clase.
    /// Si hay un enemigo cerca, el jugador se gira hacia él antes de golpear. Muestra en pantalla qué ha pasado.
    /// </summary>
    public sealed class PlayerAttack : MonoBehaviour
    {
        private const float AimRange = 6f;
        private const float EnemyRadius = 0.6f;
        private readonly DamageCalculator _damage = new DamageCalculator(new Pcg32Random((ulong)System.Environment.TickCount));
        private CharacterVitals _vitals;
        private StatSheet _stats;
        private ClassAttack _attack;
        private string _feedback = string.Empty;
        private float _feedbackLeft;

        public void Init(ClassKit kit, CharacterVitals vitals)
        {
            _vitals = vitals;
            Equip(kit);
        }

        /// <summary>Cambia el ataque al del kit indicado (al coger otro set en la Armería).</summary>
        public void Equip(ClassKit kit)
        {
            if (kit == null || _vitals == null) return;
            _stats = KitStats.For(kit, Bootstrap.GameBootstrap.Services);
            float speed = _stats.Has(StatType.AttackSpeed) ? _stats.ValueOf(StatType.AttackSpeed) : 1f;
            _attack = new ClassAttack(ClassAttackProfiles.For(kit.Id), _vitals.ClassResource, speed);
        }

        private void Update()
        {
            _feedbackLeft -= Time.deltaTime;
            if (_attack == null) return;
            _attack.Tick(Time.deltaTime);
            if (!Pressed()) return;
            var result = _attack.TryStart();
            if (result == AttackStartResult.NoResource) Say("Sin recurso de clase para el hechizo");
            if (result != AttackStartResult.Started) return;
            AimAtNearest();
            if (_attack.Profile.Style == AttackStyle.Hechizo) Cast(); else Swing();
        }

        private void AimAtNearest()
        {
            EnemyActor best = null;
            float bestDistance = AimRange;
            foreach (var enemy in FindObjectsByType<EnemyActor>(FindObjectsSortMode.None))
            {
                float distance = Flat(enemy.transform.position - transform.position).magnitude;
                if (!enemy.IsDead && distance < bestDistance) { best = enemy; bestDistance = distance; }
            }
            if (best == null) return;
            var to = Flat(best.transform.position - transform.position);
            if (to.sqrMagnitude > 1e-4f) transform.rotation = Quaternion.LookRotation(to, Vector3.up);
        }

        private void Swing()
        {
            var profile = _attack.Profile;
            var me = new PlanarVector(transform.position.x, transform.position.z);
            int hits = 0;
            foreach (var enemy in FindObjectsByType<EnemyActor>(FindObjectsSortMode.None))
            {
                var target = new PlanarVector(enemy.transform.position.x, enemy.transform.position.z);
                if (enemy.IsDead || !MeleeArc.Contains(me, transform.eulerAngles.y, target, profile.Range, profile.ArcDegrees, EnemyRadius)) continue;
                Strike(enemy);
                hits++;
            }
            SpellBolt.Flash(transform.position + transform.forward * 1.2f + Vector3.up * 1f, 0.8f, new Color(1f, 0.9f, 0.4f, 0.8f), 0.15f);
            Say(hits > 0 ? $"Golpe: {hits} enemigo(s)" : "Golpe al aire");
        }

        private void Cast()
        {
            var profile = _attack.Profile;
            var origin = transform.position + Vector3.up * 1.2f + transform.forward * 0.6f;
            SpellBolt.Launch(origin, transform.forward, profile.ProjectileSpeed, profile.Range, new Color(0.4f, 0.7f, 1f), Strike);
            Say("Hechizo lanzado");
        }

        private void Strike(EnemyActor enemy)
        {
            var hit = _attack.Profile.HitFor(_stats.ValueOf(StatType.Damage));
            var crit = new CritRules(_stats.Has(StatType.CritChance) ? Mathf.Clamp01(_stats.ValueOf(StatType.CritChance)) : 0f);
            enemy.Hurt(_damage.Resolve(hit, crit, enemy.Defense).Final);
        }

        private void Say(string text)
        {
            _feedback = text;
            _feedbackLeft = 1.2f;
        }

        private void OnGUI()
        {
            if (_feedbackLeft <= 0f) return;
            GUI.Label(new Rect(Screen.width / 2f - 200f, Screen.height - 90f, 400f, 28f), _feedback);
        }

        private static Vector3 Flat(Vector3 v) => new Vector3(v.x, 0f, v.z);

        private static bool Pressed() =>
            (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            || (Keyboard.current != null && Keyboard.current.fKey.wasPressedThisFrame);
    }
}
