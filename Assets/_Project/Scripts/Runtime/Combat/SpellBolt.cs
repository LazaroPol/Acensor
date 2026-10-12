using UnityEngine;

namespace Specimen.Runtime.Combat
{
    /// <summary>Proyectil de hechizo del loop: avanza recto y golpea al primer <see cref="EnemyActor"/> que toca.</summary>
    public sealed class SpellBolt : MonoBehaviour
    {
        private const float EnemyRadius = 0.6f;
        private Vector3 _direction;
        private float _speed;
        private float _range;
        private float _travelled;
        private System.Action<EnemyActor> _onHit;

        public static void Launch(Vector3 origin, Vector3 direction, float speed, float range, Color color,
            System.Action<EnemyActor> onHit)
        {
            var go = Flash(origin, 0.35f, color, range / Mathf.Max(speed, 0.1f) + 0.1f);
            go.name = "Hechizo";
            var bolt = go.AddComponent<SpellBolt>();
            bolt._direction = direction.normalized;
            bolt._speed = speed;
            bolt._range = range;
            bolt._onHit = onHit;
        }

        /// <summary>Esfera de color sin colisión que desaparece sola: sirve de destello de golpe o de hechizo.</summary>
        public static GameObject Flash(Vector3 position, float size, Color color, float seconds)
        {
            var go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            go.name = "Destello";
            Destroy(go.GetComponent<Collider>());
            go.transform.position = position;
            go.transform.localScale = Vector3.one * size;
            go.GetComponent<Renderer>().material.color = color;
            Destroy(go, seconds);
            return go;
        }

        private void Update()
        {
            float step = _speed * Time.deltaTime;
            transform.position += _direction * step;
            _travelled += step;
            foreach (var enemy in FindObjectsByType<EnemyActor>(FindObjectsSortMode.None))
            {
                var offset = enemy.transform.position - transform.position;
                offset.y = 0f;
                if (enemy.IsDead || offset.magnitude > EnemyRadius) continue;
                _onHit?.Invoke(enemy);
                Destroy(gameObject);
                return;
            }
            if (_travelled >= _range) Destroy(gameObject);
        }
    }
}
