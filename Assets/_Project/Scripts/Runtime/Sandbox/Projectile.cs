using System;
using UnityEngine;

namespace Specimen.Runtime.Sandbox
{
    /// <summary>Proyectil de hechizo: avanza en línea recta y golpea al primer enemigo vivo que toca.</summary>
    public sealed class Projectile : MonoBehaviour
    {
        private Vector3 _direction;
        private float _speed;
        private float _range;
        private float _radius;
        private float _travelled;
        private EnemyRegistry _enemies;
        private Action<SandboxEnemy> _onHit;

        public static Projectile Launch(Vector3 origin, Vector3 direction, float speed, float range, float radius,
            Color color, EnemyRegistry enemies, Action<SandboxEnemy> onHit)
        {
            var go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            go.name = "Proyectil";
            Destroy(go.GetComponent<Collider>());
            go.transform.position = origin;
            go.transform.localScale = Vector3.one * Mathf.Max(0.25f, radius * 0.6f);
            Tint(go.GetComponent<Renderer>(), color);
            var light = go.AddComponent<Light>();
            light.type = LightType.Point;
            light.color = color;
            light.range = 4f;
            light.intensity = 3f;
            var projectile = go.AddComponent<Projectile>();
            projectile._direction = direction.normalized;
            projectile._speed = speed;
            projectile._range = range;
            projectile._radius = radius;
            projectile._enemies = enemies;
            projectile._onHit = onHit;
            return projectile;
        }

        private static void Tint(Renderer renderer, Color color)
        {
            var unlit = Shader.Find("Universal Render Pipeline/Unlit");
            if (unlit != null) renderer.material = new Material(unlit);
            renderer.material.color = color;
        }

        private void Update()
        {
            float step = _speed * Time.deltaTime;
            transform.position += _direction * step;
            _travelled += step;
            foreach (var enemy in _enemies.Alive())
            {
                var offset = enemy.transform.position - transform.position;
                offset.y = 0f;
                if (offset.magnitude > _radius + enemy.Radius) continue;
                _onHit?.Invoke(enemy);
                Destroy(gameObject);
                return;
            }
            if (_travelled >= _range) Destroy(gameObject);
        }
    }
}
