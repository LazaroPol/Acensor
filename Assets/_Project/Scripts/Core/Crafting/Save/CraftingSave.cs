using Specimen.Core.Common;

namespace Specimen.Core.Crafting.Save
{
    /// <summary>
    /// Guarda y carga el progreso de oficios en un <see cref="ICraftingSaveStore"/>. Un guardado corrupto
    /// lanza <see cref="CraftingSaveFormatException"/>, se copia como respaldo y nunca se sobrescribe al cargar.
    /// </summary>
    public sealed class CraftingSave
    {
        private readonly ICraftingSaveStore _store;

        public CraftingSave(ICraftingSaveStore store) => _store = Guard.NotNull(store, nameof(store));

        /// <summary>Serializa antes de escribir: si los datos no son guardables, el guardado anterior queda intacto.</summary>
        public void Save(CraftingSaveData data) => _store.Write(CraftingSaveWriter.Write(Guard.NotNull(data, nameof(data))));

        /// <summary>Devuelve el progreso guardado, o uno vacío si aún no hay guardado.</summary>
        public CraftingSaveData Load()
        {
            var text = _store.Read();
            if (text == null) return new CraftingSaveData();
            try { return CraftingSaveReader.Read(text); }
            catch (CraftingSaveFormatException)
            {
                _store.WriteBackup(text);
                throw;
            }
        }
    }
}
