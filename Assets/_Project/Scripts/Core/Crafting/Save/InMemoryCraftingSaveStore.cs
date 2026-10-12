namespace Specimen.Core.Crafting.Save
{
    /// <summary>Almacén en memoria, para tests y para el prototipo.</summary>
    public sealed class InMemoryCraftingSaveStore : ICraftingSaveStore
    {
        public string Text { get; set; }
        public string Backup { get; private set; }

        public string Read() => Text;
        public void Write(string text) => Text = text;
        public void WriteBackup(string text) => Backup = text;
    }
}
