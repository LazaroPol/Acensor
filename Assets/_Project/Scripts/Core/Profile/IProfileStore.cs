namespace Specimen.Core.Profile
{
    /// <summary>Almacén de texto del perfil (fichero, nube…). Devuelve null si aún no hay perfil.</summary>
    public interface IProfileStore
    {
        string Read();
        void Write(string text);
    }
}
