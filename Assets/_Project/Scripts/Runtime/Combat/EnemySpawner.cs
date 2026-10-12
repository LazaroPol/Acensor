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
        private static readonly EnemyFactory Factory = new EnemyFactory(new LinearDepthScaling(DepthScalingConfig.Default));
        private static readonly EnemyArchetype Archetype =
            new EnemyArchetype("greybox", new EnemyStats(40f, 6f, 2f), DamageType.Physical, Resistances.None);

        public static void Populate(FloorLayout floor, FloorGreyboxLayout layout, int floorNumber, Transform player,
            CharacterVitals vitals)
        {
            foreach (var room in floor.Rooms)
            {
                if (room.Kind != RoomKind.Chamber) continue;
                var (cx, cz) = layout.CenterOf(room.Id);
                for (int i = 0; i < PerRoom; i++)
                    SpawnOne(new Vector3(cx + (i - 0.5f) * 3f, 0.5f, cz + 1f), floorNumber, room.Ring, i, player, vitals);
            }
        }

        /// <summary>Crea un enemigo de pruebas (esfera o cubo según <paramref name="variant"/>) en una posición.</summary>
        public static EnemyActor SpawnOne(Vector3 position, int floorNumber, int ring, int variant, Transform player,
            CharacterVitals vitals)
        {
            var enemy = Factory.Create(Archetype, new EnemyDepth(System.Math.Max(1, floorNumber), ring));
            var go = GameObject.CreatePrimitive(variant % 2 == 0 ? PrimitiveType.Sphere : PrimitiveType.Cube);
            go.name = "Enemigo";
            Object.Destroy(go.GetComponent<Collider>());
            go.GetComponent<Renderer>().material.color = new Color(0.8f, 0.2f, 0.2f);
            go.transform.position = position;
            var actor = go.AddComponent<EnemyActor>();
            actor.Init(enemy, player, vitals);
            return actor;
        }
    }
}
