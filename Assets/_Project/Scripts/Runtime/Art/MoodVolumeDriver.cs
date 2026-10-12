using Specimen.Core.Art;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Specimen.Runtime.Art
{
    /// <summary>
    /// Aplica el ambiente actual (<see cref="MoodTransition"/>) a un Volume global de URP
    /// (saturación, exposición, temperatura), a la niebla/luz ambiente y al grosor global del contorno toon.
    /// </summary>
    [RequireComponent(typeof(Volume))]
    public sealed class MoodVolumeDriver : MonoBehaviour
    {
        private static readonly int OutlineWidthId = Shader.PropertyToID("_SpecimenOutlineWidth");

        private MoodTransition _transition;
        private ColorAdjustments _color;
        private WhiteBalance _white;

        public void Init(MoodTransition transition) => _transition = transition;

        private void Awake()
        {
            var volume = GetComponent<Volume>();
            volume.isGlobal = true;
            volume.profile = ScriptableObject.CreateInstance<VolumeProfile>();
            _color = volume.profile.Add<ColorAdjustments>(true);
            _white = volume.profile.Add<WhiteBalance>(true);
            RenderSettings.fog = true;
            RenderSettings.fogMode = FogMode.ExponentialSquared;
            RenderSettings.ambientMode = AmbientMode.Flat;
        }

        private void Update()
        {
            if (_transition == null) return;
            _transition.Tick(Time.deltaTime);
            Apply(_transition.Current);
        }

        private void Apply(MoodProfile p)
        {
            _color.saturation.Override(p.Saturation);
            _color.postExposure.Override(p.Exposure);
            _white.temperature.Override(p.Temperature);
            RenderSettings.fogDensity = p.FogDensity;
            RenderSettings.fogColor = ToColor(p.FogColor);
            RenderSettings.ambientLight = ToColor(p.Ambient);
            Shader.SetGlobalFloat(OutlineWidthId, p.OutlineWidth);
        }

        private static Color ToColor(MoodColor c) => new Color(c.R, c.G, c.B);
    }
}
