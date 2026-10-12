using Specimen.Core.Composition;
using Specimen.Core.Iteration;
using Specimen.Core.Siege.Extraction;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Specimen.Runtime.Atico
{
    /// <summary>Helipuerto: el Osprey (bloque greybox) llega al sobrevivir el asedio; E a su lado embarca y despega.</summary>
    public sealed class OspreyPad : MonoBehaviour
    {
        private const float BoardRange = 4f;
        private OspreyExtraction _osprey;
        private IterationRun _run;
        private Transform _player;
        private Transform _model;

        public void Init(IterationRun run, Transform player)
        {
            _run = run;
            _player = player;
            _osprey = new OspreyExtraction(new[] { GameComposition.LocalPlayerId });
        }

        public bool Arrived => _osprey != null && _osprey.HasArrived;

        public void Arrive()
        {
            if (_osprey.HasArrived) return;
            _osprey.Arrive();
            var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            go.name = "Osprey";
            go.transform.localScale = new Vector3(6f, 2f, 3f);
            go.transform.position = new Vector3(AticoBootstrap.Size / 2f, 1f, AticoBootstrap.Size - 5f);
            _model = go.transform;
        }

        private void Update()
        {
            if (_osprey == null || !_osprey.HasArrived || _osprey.HasDeparted) return;
            bool near = (_player.position - _model.position).magnitude < BoardRange;
            if (!near || Keyboard.current == null || !Keyboard.current.eKey.wasPressedThisFrame) return;
            _osprey.Board(GameComposition.LocalPlayerId);
            _osprey.Depart();
            Destroy(_model.gameObject);
            _run.CompleteExtraction();
        }
    }
}
