using Specimen.Core.Classes;

namespace Specimen.Core.Hubs
{
    public enum KitAvailability
    {
        Free,
        Mine,
        Unavailable
    }

    /// <summary>Kit de clase tal como se muestra en las estanterías de la Armería.</summary>
    public readonly struct KitEntry
    {
        public KitEntry(ClassKitId id, string name, string roleAndResource, KitAvailability availability)
        {
            Id = id;
            Name = name;
            RoleAndResource = roleAndResource;
            Availability = availability;
        }

        public ClassKitId Id { get; }
        public string Name { get; }
        public string RoleAndResource { get; }
        public KitAvailability Availability { get; }
    }
}
