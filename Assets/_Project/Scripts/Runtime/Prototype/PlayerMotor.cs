using Specimen.Core.Movement;
using Specimen.Core.Stats;
using Specimen.Core.Vitals;
using UnityEngine;

namespace Specimen.Runtime.Prototype
{
    /// <summary>
    /// Mueve al jugador con un CharacterController según <see cref="PlanarMover"/> del núcleo.
    /// El sprint gasta el Aguante de <see cref="Vitals"/>; la dirección es relativa a la cámara.
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    public sealed class PlayerMotor : MonoBehaviour
    {
        [SerializeField] private float walkSpeed = 4f;
        [SerializeField] private float sprintMultiplier = 1.6f;
        [SerializeField] private float sprintStaminaPerSecond = 20f;
        [SerializeField] private float maxStamina = 100f;
        [SerializeField] private float staminaRegenPerSecond = 15f;
        [SerializeField] private float gravity = -20f;

        private CharacterController _controller;
        private PrototypeInput _input;
        private PlanarMover _mover;
        private ResourceRegen _regen;
        private Transform _cameraYaw;
        private float _verticalSpeed;

        public CharacterVitals Vitals { get; private set; }
        public bool IsSprinting => _mover != null && _mover.IsSprinting;

        public void SetCamera(Transform cameraYaw) => _cameraYaw = cameraYaw;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            var sheet = new StatSheet();
            sheet.Define(StatType.MaxHealth, 100f);
            sheet.Define(StatType.MaxClassResource, 100f);
            sheet.Define(StatType.MaxStamina, maxStamina);
            Vitals = new CharacterVitals(sheet);
            var config = new MovementConfig(walkSpeed, sprintMultiplier, sprintStaminaPerSecond);
            _mover = new PlanarMover(config, new SprintController(Vitals.Stamina, config));
            _regen = new ResourceRegen(Vitals.Stamina, staminaRegenPerSecond, 1f);
            _input = new PrototypeInput();
        }

        private void OnDestroy() => _input?.Dispose();

        private void Update()
        {
            float dt = Time.deltaTime;
            Vector2 move = _input.Move.ReadValue<Vector2>();
            float yaw = _cameraYaw != null ? _cameraYaw.eulerAngles.y : 0f;
            var velocity = _mover.Step(new PlanarVector(move.x, move.y), yaw, _input.Sprint.IsPressed(), dt);
            _regen.Paused = _mover.IsSprinting;
            _regen.Tick(dt);
            _verticalSpeed = _controller.isGrounded ? -1f : _verticalSpeed + gravity * dt;
            _controller.Move(new Vector3(velocity.X, _verticalSpeed, velocity.Z) * dt);
            if (velocity.Magnitude > MovementConfig.DeadZone)
                transform.rotation = Quaternion.Euler(0f, velocity.YawDegrees, 0f);
        }
    }
}
