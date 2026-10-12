using Specimen.Core.Armory;
using Specimen.Core.Classes;
using Specimen.Core.Composition;
using Specimen.Core.Elevator;
using Specimen.Core.Events;
using Specimen.Core.Hubs;
using Specimen.Core.Hud;
using Specimen.Core.Iteration;
using UnityEngine;

namespace Specimen.Runtime.Hubs
{
    /// <summary>
    /// Ascensor-Refugio caminable: pedestales de ranuras de módulo (E muestra la ranura), el Laboratorio como última
    /// estación y las puertas del Ascensor (E marca «listo» en la Calma o sale al piso en la Incursión).
    /// </summary>
    public static class ElevatorRoom
    {
        public static void Mount(GameObject host, IServiceResolver s, IterationRun run, SceneFlow flow)
        {
            var platform = s.Resolve<ElevatorPlatform>();
            var elevator = new ElevatorPresenter(platform, run, GameComposition.LocalPlayerId);
            var room = host.AddComponent<HubRoom>();
            int slots = platform.Slots.Count;
            int lab = slots;
            var layout = new HubRoomLayout(14, 9, slots + 1);
            room.Build(layout, KitOf(s).CreateStatSheet(), run, new HudNotices(s.Resolve<IEventBus>()),
                i => i == room.ElevatorIndex ? DoorLabel(elevator) : i == lab ? "Entrar al Laboratorio Genético" : elevator.SlotLabels()[i],
                i =>
                {
                    if (i == lab) flow.GoTo(SceneId.Laboratorio);
                    else if (i == room.ElevatorIndex) UseDoor(elevator, flow);
                });
            host.AddComponent<ElevatorScreen>().Init(elevator, flow);
        }

        private static string DoorLabel(ElevatorPresenter e) =>
            e.CanExit ? "Salir al piso" : !e.IsCalm ? "Puertas cerradas" : e.IsReady ? "Listo ✔ (cancelar)" : "Estoy listo para salir";

        private static void UseDoor(ElevatorPresenter e, SceneFlow flow)
        {
            if (e.CanExit) flow.GoTo(SceneId.Incursion);
            else e.ToggleReady();
        }

        private static ClassKit KitOf(IServiceResolver s) =>
            s.Resolve<ClassKitArmory>().TryGetKit(GameComposition.LocalPlayerId, out var kit) ? kit : s.Resolve<IClassKitCatalog>().All[0];
    }
}
