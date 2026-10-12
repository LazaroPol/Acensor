namespace Specimen.Core.Crafting.Authoring
{
    /// <summary>Problema encontrado al validar los datos de oficios (tipo de asset, id y motivo).</summary>
    public sealed class DataIssue
    {
        public DataIssue(string kind, string id, string message)
        {
            Kind = kind;
            Id = string.IsNullOrWhiteSpace(id) ? "(sin id)" : id;
            Message = message;
        }

        public string Kind { get; }
        public string Id { get; }
        public string Message { get; }

        public override string ToString() => $"[{Kind}] {Id}: {Message}";
    }
}
