namespace Specimen.Core.Composition
{
    /// <summary>Carga física de una escena. En Unity lo implementa un adaptador sobre SceneManager.</summary>
    public interface ISceneLoader
    {
        void Load(SceneId scene);
    }
}
