namespace Specimen.Core.Profile
{
    /// <summary>Almacén en memoria, para tests y para el prototipo.</summary>
    public sealed class InMemoryProfileStore : IProfileStore
    {
        public string Text { get; set; }

        public string Read() => Text;

        public void Write(string text) => Text = text;
    }
}
