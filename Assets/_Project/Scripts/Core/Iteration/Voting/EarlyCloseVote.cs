using Specimen.Core.Common;

namespace Specimen.Core.Iteration.Voting
{
    /// <summary>
    /// Voto de cierre anticipado de puertas durante la Incursión (GDD §3.1). Si se aprueba, la Incursión
    /// se acorta al margen de gracia para que el grupo pueda volver. Solo se aplica una vez por piso.
    /// </summary>
    public sealed class EarlyCloseVote
    {
        private readonly MajorityVote _vote;

        public bool Triggered { get; private set; }
        public int YesCount => _vote.YesCount;

        public EarlyCloseVote(MajorityVote vote) => _vote = Guard.NotNull(vote, nameof(vote));

        public void Reset()
        {
            _vote.Reset();
            Triggered = false;
        }

        /// <summary>Registra el voto y, si la mayoría se alcanza por primera vez, acorta la Incursión.</summary>
        public void Cast(IterationContext ctx, int playerId, bool yes)
        {
            _vote.Cast(playerId, yes);
            if (Triggered || !_vote.HasPassed) return;

            Triggered = true;
            ctx.Timer.ShortenTo(ctx.Config.EarlyCloseGraceSeconds);
            ctx.Events.Publish(new EarlyCloseApproved(ctx.Floor, ctx.Timer.Remaining));
        }
    }

    /// <summary>El grupo ha votado cerrar antes: las puertas se cierran en <see cref="SecondsLeft"/>.</summary>
    public readonly struct EarlyCloseApproved
    {
        public readonly int Floor;
        public readonly float SecondsLeft;

        public EarlyCloseApproved(int floor, float secondsLeft)
        {
            Floor = floor;
            SecondsLeft = secondsLeft;
        }
    }
}
