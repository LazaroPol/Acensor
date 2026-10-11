namespace Specimen.Core.Physics
{
    /// <summary>
    /// Matriz por defecto (PG-06): los muros bloquean a jugadores, enemigos y clones; los
    /// triggers solo detectan jugadores; los jugadores no se bloquean entre sí (coop).
    /// </summary>
    public static class DefaultCollisionMatrix
    {
        public static CollisionMatrix Create() => new CollisionMatrix()
            .Enable(CollisionLayer.Muro, CollisionLayer.Jugador)
            .Enable(CollisionLayer.Muro, CollisionLayer.Enemigo)
            .Enable(CollisionLayer.Muro, CollisionLayer.Clon)
            .Enable(CollisionLayer.Jugador, CollisionLayer.Enemigo)
            .Enable(CollisionLayer.Jugador, CollisionLayer.Clon)
            .Enable(CollisionLayer.Enemigo, CollisionLayer.Enemigo)
            .Enable(CollisionLayer.Enemigo, CollisionLayer.Clon)
            .Enable(CollisionLayer.Trigger, CollisionLayer.Jugador);
    }
}
