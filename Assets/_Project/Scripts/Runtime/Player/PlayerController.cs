using Specimen.Core.Movement;
using Specimen.Core.Stats;
using Specimen.Core.Vitals;
using UnityEngine;

namespace Specimen.Runtime.Player
{
    /// <summary>
    /// Controlador del jugador: caminar, esprintar y esquivar con un CharacterController, relativo a la
    /// cámara. Toda la lógica (velocidades, Aguante, esquiva) vive en el núcleo; aquí solo se traduce a Unity.
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    public sealed class PlayerController : MonoBehaviour
    {
        [SerializeField] private float gravity = -20f;
        [SerializeField] private float staminaRegenPerSecond = 15f;
        [SerializeField] private float healthRegenPerSecond = 0.5f;
        [SerializeField] private float resourceRegenPerSecond = 1f;

        private CharacterController _body;
        private PlayerInput _input;
        private PlanarMover _mover;
        private DodgeController _dodge;
        private ResourceRegen _regen;
        private ResourceRegen _healthRegen;
        private AcceleratingRegen _resourceRegen;
        private Transform _cameraYaw;
        private float _verticalSpeed;

        public CharacterVitals Vitals { get; private set; }
        public PlayerInput Input => _input;

        /// <summary>Inyecta las estadísticas del personaje (las de la clase elegida) y la cámara de referencia.</summary>
        public void Init(StatSheet stats, Transform cameraYaw)
        {
            Vitals = new CharacterVitals(stats);
            var config = new MovementConfig();
            _mover = new PlanarMover(config, new SprintController(Vitals.Stamina, config));
            _dodge = new DodgeController(new DodgeConfig(), Vitals.Stamina);
            _regen = new ResourceRegen(Vitals.Stamina, staminaRegenPerSecond, 1f);
            _healthRegen = new ResourceRegen(Vitals.Health, healthRegenPerSecond);
            _resourceRegen = new AcceleratingRegen(Vitals.ClassResource, resourceRegenPerSecond, 2f, 3f, 5f);
            _cameraYaw = cameraYaw;
        }

        private void Awake()
        {
            _body = GetComponent<CharacterController>();
            _input = new PlayerInput();
        }

        private void OnDestroy() => _input?.Dispose();

        private void Update()
        {
            if (_mover == null) return;
            float dt = Time.deltaTime;
            Vector2 move = _input.Move.ReadValue<Vector2>();
            var stick = new PlanarVector(move.x, move.y);
            float yaw = _cameraYaw != null ? _cameraYaw.eulerAngles.y : 0f;
            var velocity = _mover.Step(stick, yaw, _input.Sprint.IsPressed(), dt);
            if (_input.Dodge.WasPressedThisFrame()) _dodge.TryStart(velocity.Magnitude > 0f ? velocity : Facing());
            var dash = _dodge.Step(dt);
            if (_dodge.IsDodging) velocity = dash;
            _regen.Paused = _mover.IsSprinting || _dodge.IsDodging;
            _regen.Tick(dt);
            _healthRegen.Tick(dt);
            _resourceRegen.Tick(dt);
            Move(velocity, dt);
        }

        private PlanarVector Facing() => new PlanarVector(transform.forward.x, transform.forward.z);

        private void Move(PlanarVector velocity, float dt)
        {
            _verticalSpeed = _body.isGrounded ? -1f : _verticalSpeed + gravity * dt;
            _body.Move(new Vector3(velocity.X, _verticalSpeed, velocity.Z) * dt);
            if (velocity.Magnitude > MovementConfig.DeadZone)
                transform.rotation = Quaternion.Euler(0f, velocity.YawDegrees, 0f);
        }
    }
}
