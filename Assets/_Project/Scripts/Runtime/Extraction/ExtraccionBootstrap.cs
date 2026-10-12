using Specimen.Core.Composition;
using Specimen.Core.Dna;
using Specimen.Core.Extraction;
using Specimen.Core.Iteration;
using Specimen.Core.Iteration.Ending;
using Specimen.Core.Loot;
using Specimen.Core.Profile;
using Specimen.Runtime.Bootstrap;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Specimen.Runtime.Extraction
{
    /// <summary>
    /// Escena «Extraccion»: cinemática a bordo del Osprey (felicitación, muestra de sangre, desmayo). Al empezar
    /// abona los puntos de ADN de la iteración al perfil; al terminar lleva al Laboratorio. La escena puede estar vacía.
    /// </summary>
    public sealed class ExtraccionBootstrap : MonoBehaviour
    {
        private BloodDrawSequence _sequence;
        private SceneFlow _flow;
        private int _points;
        private bool _left;

        /// <summary>Lo llama <c>SceneBootstrapper</c> en cada carga de escena; solo actúa en la suya.</summary>
        public static void Ensure()
        {
            if (SceneManager.GetActiveScene().name != nameof(SceneId.Extraccion)) return;
            if (FindAnyObjectByType<ExtraccionBootstrap>() != null) return;
            new GameObject(nameof(ExtraccionBootstrap)).AddComponent<ExtraccionBootstrap>();
        }

        private void Start()
        {
            var s = GameBootstrap.Services;
            _flow = s.Resolve<SceneFlow>();
            var run = s.Resolve<IterationRun>();
            var haul = s.Resolve<RunHaul>();
            _points = DnaReward.For(run.Outcome ?? IterationOutcome.Victory, run.Floor, haul.Items);
            s.Resolve<PlayerProfile>().Reward(_points);
            haul.Clear();
            _sequence = BloodDrawSequence.CreateDefault();
            EnsureCamera();
        }

        private static void EnsureCamera()
        {
            if (Camera.main != null) return;
            var camera = new GameObject("Main Camera") { tag = "MainCamera" }.AddComponent<Camera>();
            camera.clearFlags = CameraClearFlags.SolidColor;
            camera.backgroundColor = new Color(0.08f, 0.1f, 0.12f);
        }

        private void Update()
        {
            if (_sequence == null || _left) return;
            _sequence.Tick(Time.deltaTime);
            if (!_sequence.IsFinished) return;
            _left = true;
            _flow.GoTo(SceneId.Laboratorio);
        }

        private void OnGUI()
        {
            if (_sequence == null) return;
            var step = _sequence.Current;
            GUI.color = Overlay(step.Tone, _sequence.Progress);
            GUI.DrawTexture(new Rect(0f, 0f, Screen.width, Screen.height), Texture2D.whiteTexture);
            GUI.color = step.Tone == BloodDrawTone.Blackout ? new Color(1f, 1f, 1f, 1f - _sequence.Progress) : Color.white;
            var style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontSize = 22 };
            GUI.Label(new Rect(0f, Screen.height / 2f - 40f, Screen.width, 40f), step.Text, style);
            GUI.Label(new Rect(0f, Screen.height / 2f + 10f, Screen.width, 30f), $"+{_points} puntos de ADN", style);
            GUI.color = Color.white;
        }

        private static Color Overlay(BloodDrawTone tone, float progress)
        {
            switch (tone)
            {
                case BloodDrawTone.Blood: return new Color(0.6f, 0f, 0f, 0.25f + 0.35f * Mathf.Sin(progress * Mathf.PI));
                case BloodDrawTone.Blackout: return new Color(0f, 0f, 0f, progress);
                default: return new Color(0.5f, 0.7f, 0.9f, 0.12f);
            }
        }
    }
}
