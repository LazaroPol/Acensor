using Specimen.Core.Appearance;
using Specimen.Core.Classes;
using UnityEngine;

namespace Specimen.Editor.Arte
{
    /// <summary>Decorador «Armas»: pone en la mano derecha el arma del <see cref="ClassLoadouts"/> del kit.</summary>
    public sealed class WeaponDecorator : ICharacterDecorator
    {
        private readonly ModelLocator _models = new ModelLocator(ArtPaths.Weapons3P);

        public void Decorate(ClassKitId kit, GameObject character)
        {
            var loadout = ClassLoadouts.For(kit);
            var asset = _models.Load(loadout.Model);
            if (asset == null) return;
            var weapon = Object.Instantiate(asset);
            weapon.name = "Arma";
            var textures = new TextureLocator($"{ArtPaths.Weapons3P}/{loadout.Folder}", ArtPaths.Weapons3P);
            MaterialRemapper.Apply(weapon, textures, loadout.Folder);
            var bones = SkeletonMerger.BonesByName(character.transform);
            if (GripSolver.Place(weapon, bones, WeaponModels.LengthOf(loadout.Weapon))) return;
            Debug.LogWarning($"[SPECIMEN] {kit}: no encuentro la mano derecha; el arma queda en la raíz.");
            weapon.transform.SetParent(character.transform, false);
        }
    }
}
