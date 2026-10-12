using System.Collections.Generic;
using Specimen.Core.Combat.Targeting;
using Specimen.Runtime.UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Specimen.Runtime.Sandbox
{
    /// <summary>
    /// Objetivo del jugador al estilo WoW: Tab recorre los enemigos cercanos, clic izquierdo sobre uno lo selecciona
    /// y clic derecho lo selecciona y empieza a atacarle. Un anillo rojo en el suelo marca el objetivo.
    /// </summary>
    [DefaultExecutionOrder(-10)]
    public sealed class PlayerTarget : MonoBehaviour
    {
        private const float ClickRadius = 70f;

        private SandboxContext _context;
        private Transform _ring;

        public SandboxEnemy Current { get; private set; }
        public bool HasTarget => Current != null && !Current.IsDead;

        public void Init(SandboxContext context)
        {
            _context = context;
            _ring = World.WorldMaterials.Shape(PrimitiveType.Cylinder, null, Vector3.zero, new Vector3(1f, 0.01f, 1f),
                new Color(0.9f, 0.15f, 0.1f)).transform;
            _ring.name = "AnilloObjetivo";
            _ring.gameObject.SetActive(false);
        }

        public void Set(SandboxEnemy enemy)
        {
            Current = enemy != null && !enemy.IsDead ? enemy : null;
        }

        public void Clear() => Current = null;

        private void Update()
        {
            var input = _context.Input;
            if (Current != null && Current.IsDead) Current = null;
            if (input.NextTarget.WasPressedThisFrame()) Cycle();
            bool overUi = UiRoot.PointerOverUi();
            if (!overUi && (input.Select.WasPressedThisFrame() || input.AutoAttack.WasPressedThisFrame()) && Mouse.current != null)
            {
                var clicked = UnderMouse(Mouse.current.position.ReadValue());
                if (clicked != null) Set(clicked);
            }
            UpdateRing();
        }

        private void Cycle()
        {
            var alive = new List<SandboxEnemy>(_context.Enemies.Alive());
            var candidates = new List<(int, float)>();
            for (int i = 0; i < alive.Count; i++)
                candidates.Add((i, Vector3.Distance(alive[i].transform.position, transform.position)));
            int current = Current != null ? alive.IndexOf(Current) : -1;
            int? next = TabTargeting.Next(candidates, current >= 0 ? current : (int?)null);
            Current = next.HasValue ? alive[next.Value] : null;
            if (Current == null) _context.Events.Fail("No hay enemigos cerca");
        }

        private SandboxEnemy UnderMouse(Vector2 mouse)
        {
            var camera = Camera.main;
            if (camera == null) return null;
            SandboxEnemy best = null;
            float bestDistance = ClickRadius;
            foreach (var enemy in _context.Enemies.Alive())
            {
                var screen = camera.WorldToScreenPoint(enemy.transform.position + Vector3.up * enemy.Height * 0.5f);
                if (screen.z <= 0f) continue;
                float distance = Vector2.Distance(mouse, screen);
                if (distance >= bestDistance) continue;
                best = enemy;
                bestDistance = distance;
            }
            return best;
        }

        private void UpdateRing()
        {
            _ring.gameObject.SetActive(HasTarget);
            if (!HasTarget) return;
            float size = Current.Radius * 2.6f + 0.4f;
            _ring.position = Current.transform.position + Vector3.up * 0.04f;
            _ring.localScale = new Vector3(size, 0.01f, size);
        }

        private void OnDestroy()
        {
            if (_ring != null) Destroy(_ring.gameObject);
        }
    }
}
