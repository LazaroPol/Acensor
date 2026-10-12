using UnityEngine;

namespace Specimen.Runtime.Sandbox
{
    /// <summary>Algo que se usa con E estando cerca: banco de profesión, árbol, roca, hierba…</summary>
    public interface IInteractable
    {
        string Prompt { get; }
        Vector3 Position { get; }
        float Range { get; }
        bool CanInteract { get; }
        void Interact(SandboxPlayer player);
    }
}
