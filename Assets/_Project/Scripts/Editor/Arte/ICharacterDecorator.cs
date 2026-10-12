using Specimen.Core.Classes;
using UnityEngine;

namespace Specimen.Editor.Arte
{
    /// <summary>Añade algo a un personaje de clase ya montado (arma, equipo…), antes de guardarlo como prefab.</summary>
    public interface ICharacterDecorator
    {
        void Decorate(ClassKitId kit, GameObject character);
    }
}
