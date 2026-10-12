using Specimen.Core.Armory;
using Specimen.Core.Classes;
using Specimen.Core.Composition;
using Specimen.Core.Events;
using Specimen.Core.Hubs;
using Specimen.Core.Hud;
using Specimen.Core.Iteration;
using UnityEngine;

namespace Specimen.Runtime.Hubs
{
    /// <summary>Armería caminable: un pedestal por kit (E lo coge) y el Ascensor al fondo (E sale con kit en mano).</summary>
    public static class ArmoryRoom
    {
        public static void Mount(GameObject host, IServiceResolver s, IterationRun run, SceneFlow flow)
        {
            var catalog = s.Resolve<IClassKitCatalog>();
            var armory = new ArmoryPresenter(s.Resolve<ClassKitArmory>(), catalog, run, GameComposition.LocalPlayerId);
            var room = host.AddComponent<HubRoom>();
            var layout = new HubRoomLayout(14, 9, catalog.All.Count);
            room.Build(layout, catalog.All[0].CreateStatSheet(), run, new HudNotices(s.Resolve<IEventBus>()),
                i => i == room.ElevatorIndex ? (armory.CanLeave ? "Subir al Ascensor" : armory.Message) : Label(armory, i),
                i =>
                {
                    if (i != room.ElevatorIndex) armory.Choose(armory.Entries()[i].Id);
                    else if (armory.Leave()) flow.GoTo(SceneId.Ascensor);
                });
            host.AddComponent<ArmoryScreen>().Init(armory, null);
        }

        private static string Label(ArmoryPresenter armory, int i)
        {
            var kit = armory.Entries()[i];
            string mark = kit.Availability == KitAvailability.Mine ? "✔ " : string.Empty;
            return $"{mark}Coger {kit.Name} ({kit.RoleAndResource})";
        }
    }
}
