using Specimen.Core.Classes;
using Specimen.Core.Composition;
using Specimen.Core.Profile;
using Specimen.Core.Stats;

namespace Specimen.Runtime.Player
{
    /// <summary>Hoja de atributos de un clon nuevo: la base del kit más las mutaciones de ADN compradas en el Laboratorio.</summary>
    public static class KitStats
    {
        public static StatSheet For(ClassKit kit, IServiceResolver services)
        {
            var sheet = kit.CreateStatSheet();
            if (services != null && services.TryResolve<PlayerProfile>(out var profile)) profile.ApplyTo(sheet);
            return sheet;
        }
    }
}
