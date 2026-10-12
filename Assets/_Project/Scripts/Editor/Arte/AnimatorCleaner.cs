using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace Specimen.Editor.Arte
{
    /// <summary>Vacía un AnimatorController existente (parámetros, estados, transiciones y blend trees) sin cambiar su GUID.</summary>
    public static class AnimatorCleaner
    {
        public static void Clear(AnimatorController controller, string path)
        {
            foreach (var parameter in controller.parameters) controller.RemoveParameter(parameter);
            var machine = controller.layers[0].stateMachine;
            foreach (var transition in machine.anyStateTransitions) machine.RemoveAnyStateTransition(transition);
            foreach (var child in machine.states) machine.RemoveState(child.state);
            foreach (var asset in AssetDatabase.LoadAllAssetsAtPath(path))
                if (asset is BlendTree) Object.DestroyImmediate(asset, true);
            EditorUtility.SetDirty(controller);
        }
    }
}
