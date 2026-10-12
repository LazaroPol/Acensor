using System.Collections.Generic;
using UnityEngine;

namespace Specimen.Editor.Arte
{
    /// <summary>
    /// Convención del juego: los personajes miran a +Z. Algunos FBX de Blender llegan mirando a -Z y,
    /// al girar el personaje hacia donde camina, parece que anda hacia atrás. Se detecta con los pies de la
    /// pose de reposo (tobillo → punta) y, si miran hacia atrás, se gira el modelo 180°.
    /// </summary>
    public static class FacingFixer
    {
        public static bool FaceForward(GameObject model)
        {
            var bones = SkeletonMerger.BonesByName(model.transform);
            var toes = Toward(bones, "foot_l", "ball_l") + Toward(bones, "foot_r", "ball_r");
            toes.y = 0f;
            if (toes.sqrMagnitude < 1e-8f) return false;
            var forward = model.transform.parent != null ? model.transform.parent.forward : Vector3.forward;
            if (Vector3.Dot(toes, forward) >= 0f) return false;
            model.transform.RotateAround(model.transform.position, Vector3.up, 180f);
            return true;
        }

        private static Vector3 Toward(Dictionary<string, Transform> bones, string from, string to)
            => bones.TryGetValue(from, out var a) && bones.TryGetValue(to, out var b) ? b.position - a.position : Vector3.zero;
    }
}
