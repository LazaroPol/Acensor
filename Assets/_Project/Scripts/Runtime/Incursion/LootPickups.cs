using System.Collections.Generic;
using Specimen.Core.Floors;
using UnityEngine;

namespace Specimen.Runtime.Incursion
{
    /// <summary>Botín greybox: un cubo verde por cámara; se recoge al pasar por encima.</summary>
    public sealed class LootPickups : MonoBehaviour
    {
        private const float PickRange = 1.3f;
        private readonly List<GameObject> _items = new List<GameObject>();
        private Transform _player;

        public int Collected { get; private set; }

        public void Init(FloorLayout floor, FloorGreyboxLayout layout, Transform player)
        {
            _player = player;
            foreach (var room in floor.Rooms)
            {
                if (room.Kind != RoomKind.Chamber) continue;
                var (x, z) = layout.CenterOf(room.Id);
                var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                go.name = "Botín";
                Object.Destroy(go.GetComponent<Collider>());
                go.transform.localScale = Vector3.one * 0.5f;
                go.transform.position = new Vector3(x - 2f, 0.25f, z - 2f);
                go.GetComponent<Renderer>().material.color = new Color(0.2f, 0.85f, 0.3f);
                _items.Add(go);
            }
        }

        private void Update()
        {
            for (int i = _items.Count - 1; i >= 0; i--)
            {
                if ((_items[i].transform.position - _player.position).sqrMagnitude > PickRange * PickRange) continue;
                Destroy(_items[i]);
                _items.RemoveAt(i);
                Collected++;
            }
        }
    }
}
