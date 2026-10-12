using Specimen.Core.Gathering;
using Specimen.Core.Items;
using Specimen.Core.Progression;
using UnityEngine;

namespace Specimen.Runtime.Sandbox.World
{
    /// <summary>
    /// Punto de recolección (árbol, roca, veta, hierba): E para recoger materiales a la bolsa y ganar
    /// experiencia; tras <see cref="GatheringRules.UsesPerNode"/> usos se agota y vuelve a crecer.
    /// </summary>
    public sealed class GatherNode : MonoBehaviour, IInteractable
    {
        private SandboxContext _context;
        private GameObject _model;
        private int _uses;
        private float _regrow;

        public GatherKind Kind { get; private set; }
        public string Prompt => $"{Verb} {GatheringRules.NameOf(Kind)} ({_uses}/{GatheringRules.UsesPerNode})";
        public Vector3 Position => transform.position;
        public float Range => Kind == GatherKind.Hierba ? 1.6f : 2.4f;
        public bool CanInteract => _uses > 0;

        public void Init(GatherKind kind, SandboxContext context, bool ash)
        {
            Kind = kind;
            _context = context;
            _uses = GatheringRules.UsesPerNode;
            _model = NatureFactory.Build(kind, transform, transform.position.x * 0.37f + transform.position.z, ash);
            context.Interactions?.Register(this);
        }

        public void Interact(SandboxPlayer player)
        {
            if (_uses <= 0) return;
            foreach (var drop in GatheringRules.Gather(Kind, _context.Random))
            {
                int added = _context.Inventory.Add(ItemInstance.CreateNew(drop.Item), drop.Quantity);
                _context.Feed.Add(added > 0 ? $"+ {drop.Item.Name} x{added}" : "Inventario lleno.");
            }
            _context.GrantXp(XpRewards.Gather, GatheringRules.NameOf(Kind));
            if (--_uses > 0) return;
            _model.SetActive(false);
            if (Kind == GatherKind.Arbol) Stump();
            _regrow = GatheringRules.RegrowSeconds;
        }

        private void Stump()
        {
            var stump = WorldMaterials.Shape(PrimitiveType.Cylinder, transform, new Vector3(0f, 0.2f, 0f),
                new Vector3(0.5f, 0.2f, 0.5f), new Color(0.33f, 0.22f, 0.14f));
            stump.name = "Tocón";
        }

        private void Update()
        {
            if (_uses > 0 || (_regrow -= Time.deltaTime) > 0f) return;
            _uses = GatheringRules.UsesPerNode;
            _model.SetActive(true);
            var stump = transform.Find("Tocón");
            if (stump != null) Destroy(stump.gameObject);
        }

        private string Verb => Kind switch
        {
            GatherKind.Arbol => "Talar",
            GatherKind.Hierba => "Recoger",
            _ => "Picar"
        };
    }
}
