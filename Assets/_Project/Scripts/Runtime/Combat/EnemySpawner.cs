using Specimen.Core.Combat;
using Specimen.Core.Combat.Enemies;
using Specimen.Core.Floors;
using Specimen.Core.Vitals;
using UnityEngine;

namespace Specimen.Runtime.Combat
{
    /// <summary>Coloca enemigos de pruebas (esferas y cubos) en las cámaras del piso, escalados por piso y anillo.</summary>
    public static class EnemySpawner
    {
        private const int PerRoom = 2;

        public static void Populate(FloorLayout floor, FloorGreyboxLayout layout, int floorNumber, Transform player,
            CharacterVitals vitals)
        {
            var factory = new EnemyFactory(new LinearDepthScaling(DepthScalingConfig.Default));
            var archetype = new EnemyArchetype("greybox", new EnemyStats(40f, 6f, 2f), DamageType.Physical, Resistances.None);
            foreach (var room in floor.Rooms)
            {
                if (room.Kind != RoomKind.Chamber) continue;
                var (cx, cz) = layout.CenterOf(room.Id);
                for (int i = 0; i < PerRoom; i++)
                {
                    var enemy = factory.Create(archetype, new EnemyDepth(System.Math.Max(1, floorNumber), room.Ring));
                    var go = GameObject.CreatePrimitive(i % 2 == 0 ? PrimitiveType.Sphere : PrimitiveType.Cube);
                    go.name = "Enemigo";
                    Object.Destroy(go.GetComponent<Collider>());
                    go.GetComponent<Renderer>().material.color = new Color(0.8f, 0.2f, 0.2f);
                    go.transform.position = new Vector3(cx + (i - 0.5f) * 3f, 0.5f, cz + 1f);
                    go.AddComponent<EnemyActor>().Init(enemy, player, vitals);
                }
            }
        }
    }
}
