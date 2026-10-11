using Specimen.Core.Iteration.Presence;

namespace Specimen.Core.Iteration
{
    /// <summary>Entradas y salidas del ascensor, y rescate por el Hueco de Emergencia.</summary>
    public sealed partial class IterationRun
    {
        public PlayerLocation Location(int playerId) => _ctx.Presence.Location(playerId);

        public void LeaveElevator(int playerId) =>
            Signal(IterationPhase.Incursion, () => _ctx.Presence.LeaveElevator(playerId));

        public void ReturnToElevator(int playerId) =>
            Signal(IterationPhase.Incursion, () => _ctx.Presence.ReturnToElevator(playerId));

        /// <summary>Un atrapado alcanza el Hueco de Emergencia y trepa hasta el ascensor del piso actual.</summary>
        public void ReachEmergencyShaft(int playerId)
        {
            Signal(IterationPhase.Calm, IterationPhase.Incursion, () => _ctx.Presence.Rescue(playerId));
            _ctx.Events.Publish(new PlayerRescued(playerId, _ctx.Floor));
        }
    }
}
