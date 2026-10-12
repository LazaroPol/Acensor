using Specimen.Core.Animation;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace Specimen.Editor.Arte
{
    /// <summary>
    /// Genera un AnimatorController a partir de un <see cref="AnimationSet"/>: blend 1D de locomoción
    /// sobre «Velocidad», acciones disparadas por trigger desde cualquier estado y «Muerto» como bool.
    /// </summary>
    public static class AnimatorBuilder
    {
        public static AnimatorController Build(AnimationSet set, ClipIndex clips)
        {
            ArtFolders.Ensure(ArtPaths.Animation);
            string path = $"{ArtPaths.Animation}/{set.Name}.controller";
            // Se reconstruye dentro del mismo asset: borrarlo y recrearlo deja las referencias de los
            // prefabs apuntando a un objeto que ya no existe («Missing») y los personajes quedan en T-pose.
            var controller = AssetDatabase.LoadAssetAtPath<AnimatorController>(path);
            if (controller == null) controller = AnimatorController.CreateAnimatorControllerAtPath(path);
            else AnimatorCleaner.Clear(controller, path);
            AddParameters(controller);
            var machine = controller.layers[0].stateMachine;
            var locomotion = BuildLocomotion(controller, set, clips);
            machine.defaultState = locomotion;
            foreach (AnimSlot slot in System.Enum.GetValues(typeof(AnimSlot)))
                if (AnimParams.TriggerOf(slot) != null) AddAction(machine, locomotion, slot, Clip(set, clips, slot));
            AddDeath(machine, locomotion, Clip(set, clips, AnimSlot.Muerte));
            EditorUtility.SetDirty(controller);
            return controller;
        }

        private static void AddParameters(AnimatorController controller)
        {
            controller.AddParameter(AnimParams.Velocidad, AnimatorControllerParameterType.Float);
            controller.AddParameter(AnimParams.Muerto, AnimatorControllerParameterType.Bool);
            foreach (AnimSlot slot in System.Enum.GetValues(typeof(AnimSlot)))
            {
                string trigger = AnimParams.TriggerOf(slot);
                if (trigger != null) controller.AddParameter(trigger, AnimatorControllerParameterType.Trigger);
            }
        }

        private static AnimatorState BuildLocomotion(AnimatorController controller, AnimationSet set, ClipIndex clips)
        {
            var state = controller.CreateBlendTreeInController("Locomocion", out BlendTree tree, 0);
            tree.blendType = BlendTreeType.Simple1D;
            tree.blendParameter = AnimParams.Velocidad;
            tree.useAutomaticThresholds = false;
            AddChild(tree, Clip(set, clips, AnimSlot.Reposo), 0f);
            AddChild(tree, Clip(set, clips, AnimSlot.Andar), LocomotionBlend.Walk);
            AddChild(tree, Clip(set, clips, AnimSlot.Trotar), LocomotionBlend.Jog);
            AddChild(tree, Clip(set, clips, AnimSlot.Esprintar), LocomotionBlend.Sprint);
            return state;
        }

        private static void AddChild(BlendTree tree, AnimationClip clip, float threshold)
        {
            if (clip != null) tree.AddChild(clip, threshold);
        }

        private static void AddAction(AnimatorStateMachine machine, AnimatorState back, AnimSlot slot, AnimationClip clip)
        {
            if (clip == null) return;
            var state = machine.AddState(slot.ToString());
            state.motion = clip;
            var enter = machine.AddAnyStateTransition(state);
            enter.AddCondition(AnimatorConditionMode.If, 0f, AnimParams.TriggerOf(slot));
            enter.AddCondition(AnimatorConditionMode.IfNot, 0f, AnimParams.Muerto);
            enter.hasExitTime = false;
            enter.duration = 0.08f;
            enter.canTransitionToSelf = true;
            var exit = state.AddTransition(back);
            exit.hasExitTime = true;
            exit.exitTime = 0.85f;
            exit.duration = 0.15f;
        }

        private static void AddDeath(AnimatorStateMachine machine, AnimatorState back, AnimationClip clip)
        {
            var state = machine.AddState(AnimSlot.Muerte.ToString());
            state.motion = clip;
            var enter = machine.AddAnyStateTransition(state);
            enter.AddCondition(AnimatorConditionMode.If, 0f, AnimParams.Muerto);
            enter.hasExitTime = false;
            enter.duration = 0.1f;
            enter.canTransitionToSelf = false;
            var revive = state.AddTransition(back);
            revive.AddCondition(AnimatorConditionMode.IfNot, 0f, AnimParams.Muerto);
            revive.hasExitTime = false;
            revive.duration = 0.2f;
        }

        private static AnimationClip Clip(AnimationSet set, ClipIndex clips, AnimSlot slot)
        {
            var clip = clips.Find(set.CandidatesFor(slot));
            if (clip == null) Debug.LogWarning($"[SPECIMEN] {set.Name}: sin clip para {slot}.");
            return clip;
        }
    }
}
