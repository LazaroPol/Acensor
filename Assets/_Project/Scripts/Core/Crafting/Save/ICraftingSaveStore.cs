namespace Specimen.Core.Crafting.Save
{
    /// <summary>Almacén de texto del guardado de oficios. <see cref="Read"/> devuelve null si no hay guardado.</summary>
    public interface ICraftingSaveStore
    {
        string Read();
        void Write(string text);
        /// <summary>Conserva una copia (<c>.bak</c>) de un texto que no se pudo cargar.</summary>
        void WriteBackup(string text);
    }
}
