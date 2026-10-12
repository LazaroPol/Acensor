using System;
using Specimen.Core.Animation;
using Specimen.Core.Classes;
using Specimen.Core.Combat;
using Specimen.Core.Stats;
using Specimen.Core.Vitals;
using Specimen.Runtime.Characters;
using Specimen.Runtime.Player;
using UnityEngine;

namespace Specimen.Runtime.Sandbox
{
    /// <summary>
    /// Jugador del Campo de pruebas sobre el PlayerController de producción: cambia de kit (stats +
    /// modelo), anima la locomoción, recibe golpes (con escudo) y muere/revive.
    /// </summary>
    [RequireComponent(typeof(PlayerController))]
    public sealed partial class SandboxPlayer : MonoBehaviour
    {
        private PlayerController _controller;
        private CharacterAnimator _animator;
        private SandboxContext _context;
        private Transform _cameraYaw;
        private GameObject _visual;
        private Vector3 _lastPosition;
        private float _shieldLeft;
        private float _shieldReduction;

        public event Action<ClassKit> Equipped;

        public ClassKit Kit { get; private set; }
        public StatSheet Stats { get; private set; }
        public CharacterVitals Vitals => _controller.Vitals;
        public bool IsAlive => Vitals != null && Vitals.IsAlive;
        public bool IsShielded => _shieldLeft > 0f;
        public CharacterAnimator Animator => _animator;
        public PlayerController Controller => _controller;

        public void Init(SandboxContext context, Transform cameraYaw)
        {
            _controller = GetComponent<PlayerController>();
            _animator = gameObject.AddComponent<CharacterAnimator>();
            _context = context;
            _cameraYaw = cameraYaw;
            _lastPosition = transform.position;
        }

        public void Equip(ClassKit kit)
        {
            Kit = kit;
            Stats = kit.CreateStatSheet();
            _controller.Init(Stats, _cameraYaw);
            _controller.enabled = true;
            SwapVisual(_context.Art != null ? _context.Art.ClassPrefab(kit.Id) : null);
            _animator.Rebind();
            _shieldLeft = 0f;
            _context.Feed.Add($"Kit equipado: {kit.DisplayName} ({kit.Role})");
            Equipped?.Invoke(kit);
        }

        public void Shield(float reduction, float seconds)
        {
            _shieldReduction = Mathf.Clamp01(reduction);
            _shieldLeft = seconds;
        }

        public void ReceiveHit(Hit hit, string source)
        {
            if (!IsAlive) return;
            float armor = Stats.Has(StatType.Armor) ? Stats.ValueOf(StatType.Armor) : 0f;
            var result = _context.Hits.Resolve(hit, CritRules.None, new DefenseProfile(armor, Resistances.None));
            float amount = result.Final * (IsShielded ? 1f - _shieldReduction : 1f);
            Vitals.Health.Drain(amount);
            _animator.Play(AnimSlot.Golpe);
            _context.Feed.Add($"{source} te golpea: -{amount:0}{(IsShielded ? " (escudo)" : string.Empty)}");
        }

        private void SwapVisual(GameObject prefab)
        {
            if (_visual != null)
            {
                _visual.SetActive(false);
                Destroy(_visual);
            }
            _visual = VisualFactory.Character(prefab, transform, new Color(0.2f, 0.5f, 0.9f), 1.8f);
            _visual.name = "Visual";
        }

        private void Update()
        {
            if (Vitals == null) return;
            if (_shieldLeft > 0f) _shieldLeft -= Time.deltaTime;
            if (!IsAlive && _controller.enabled) Die();
            if (IsAlive && _controller.Input.Dodge.WasPressedThisFrame()) _animator.Play(AnimSlot.Esquiva);
        }

        private void LateUpdate()
        {
            var delta = transform.position - _lastPosition;
            delta.y = 0f;
            _lastPosition = transform.position;
            float dt = Mathf.Max(Time.deltaTime, 1e-4f);
            _animator.SetSpeed(IsAlive ? delta.magnitude / dt : 0f);
        }
    }
}
