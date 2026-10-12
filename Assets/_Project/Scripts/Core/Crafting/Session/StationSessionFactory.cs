using Specimen.Core.Common;
using Specimen.Core.Crafting.Authoring;
using Specimen.Core.Crafting.Definitions;
using Specimen.Core.Crafting.Queue;
using Specimen.Core.Crafting.Stations;
using Specimen.Core.Randomness;

namespace Specimen.Core.Crafting.Session
{
    /// <summary>Crea estaciones listas para usar a partir del catálogo de oficios y el perfil del jugador.</summary>
    public sealed class StationSessionFactory
    {
        private readonly ICraftingCatalog _catalog;
        private readonly CraftingProfile _profile;
        private readonly IQualityRoller _roller;
        private readonly RecipeAvailability _availability;
        private readonly StationServices _services;

        public StationSessionFactory(ICraftingCatalog catalog, CraftingProfile profile, IRandom random)
        {
            _catalog = Guard.NotNull(catalog, nameof(catalog));
            _profile = Guard.NotNull(profile, nameof(profile));
            _roller = new QualityRoller(catalog.Quality, Guard.NotNull(random, nameof(random)));
            _availability = new RecipeAvailability(catalog.Recipes);
            _services = new StationServices(catalog.Parts);
        }

        public StationSession Create(StationType type)
        {
            var station = new Workstation(type);
            var queue = new CraftQueue(station, _roller, _catalog.Quality, milestones: _catalog.Milestones);
            return new StationSession(station, queue, _availability, _profile, _services);
        }
    }
}
