using System.Collections.Generic;
using Specimen.Core.Animation;
using UnityEngine;

namespace Specimen.Runtime.Characters
{
    /// <summary>
    /// Puente código → Animator: traduce velocidad (m/s) al blend de locomoción y ranuras lógicas a
    /// triggers. Si el modelo es greybox (sin Animator) no hace nada.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class CharacterAnimator : MonoBehaviour
    {
        private const float SpeedDamping = 0.1f;

        private readonly HashSet<int> _parameters = new HashSet<int>();
        private Animator _animator;
        private LocomotionBlend _blend = new LocomotionBlend();

        public bool HasAnimator => _animator != null && _animator.runtimeAnimatorController != null;

        public void Init(LocomotionBlend blend) => _blend = blend ?? _blend;

        /// <summary>Vuelve a buscar el Animator (p. ej. tras cambiar el modelo visual).</summary>
        public void Rebind()
        {
            _animator = GetComponentInChildren<Animator>();
            _parameters.Clear();
            if (!HasAnimator) return;
            _animator.applyRootMotion = false;
            foreach (var parameter in _animator.parameters) _parameters.Add(parameter.nameHash);
        }

        private void Awake() => Rebind();

        public void SetSpeed(float metersPerSecond)
        {
            int hash = Animator.StringToHash(AnimParams.Velocidad);
            if (HasAnimator && _parameters.Contains(hash))
                _animator.SetFloat(hash, _blend.Normalize(metersPerSecond), SpeedDamping, Time.deltaTime);
        }

        public void Play(AnimSlot slot)
        {
            string trigger = AnimParams.TriggerOf(slot);
            if (trigger == null) return;
            int hash = Animator.StringToHash(trigger);
            if (HasAnimator && _parameters.Contains(hash)) _animator.SetTrigger(hash);
        }

        public void SetDead(bool dead)
        {
            int hash = Animator.StringToHash(AnimParams.Muerto);
            if (HasAnimator && _parameters.Contains(hash)) _animator.SetBool(hash, dead);
        }
    }
}
