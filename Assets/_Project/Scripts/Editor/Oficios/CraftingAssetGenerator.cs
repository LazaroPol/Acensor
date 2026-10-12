using System;
using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Crafting.Authoring;
using Specimen.Editor.Arte;
using Specimen.Runtime.Crafting.Authoring;
using UnityEditor;
using UnityEngine;

namespace Specimen.Editor.Oficios
{
    /// <summary>
    /// Crea los assets de oficios desde un <see cref="CraftingDataSet"/> y los añade a la base de datos.
    /// Un asset que ya existe no se pisa: tras la generación inicial, los datos editados mandan.
    /// </summary>
    public static class CraftingAssetGenerator
    {
        public static int Generate(CraftingDataSet data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            var created = 0;
            ArtFolders.Ensure(CraftingPaths.Root);
            var db = LoadOrCreateDatabase();
            AssetDatabase.StartAssetEditing();
            try
            {
                Merge(db.items, Write<ItemDefinitionAsset, ItemData>(CraftingPaths.Items, data.Items, d => d.id, ref created));
                Merge(db.recipes, Write<RecipeDefinitionAsset, RecipeData>(CraftingPaths.Recipes, data.Recipes, d => d.id, ref created));
                Merge(db.knowledge, Write<KnowledgeAsset, KnowledgeData>(CraftingPaths.Knowledge, data.Knowledge, d => d.id, ref created));
                Merge(db.parts, Write<StationPartAsset, StationPartData>(CraftingPaths.Parts, data.Parts, d => d.id, ref created));
                Merge(db.mods, Write<ModDefinitionAsset, ModData>(CraftingPaths.Mods, data.Mods, d => d.id, ref created));
                if (db.curve == null) db.curve = LoadOrCreate<ProfessionCurveAsset, CurveData>(CraftingPaths.Curve, data.Curve, ref created);
                if (db.quality == null) db.quality = LoadOrCreate<QualityRulesAsset, QualityData>(CraftingPaths.Quality, data.Quality, ref created);
            }
            finally
            {
                AssetDatabase.StopAssetEditing();
            }
            EditorUtility.SetDirty(db);
            AssetDatabase.SaveAssets();
            return created;
        }

        private static CraftingDatabase LoadOrCreateDatabase()
        {
            var db = AssetDatabase.LoadAssetAtPath<CraftingDatabase>(CraftingPaths.Database);
            if (db != null) return db;
            db = ScriptableObject.CreateInstance<CraftingDatabase>();
            AssetDatabase.CreateAsset(db, CraftingPaths.Database);
            return db;
        }

        private static List<TAsset> Write<TAsset, TData>(string folder, IEnumerable<TData> data,
            Func<TData, string> id, ref int created)
            where TAsset : CraftingAsset<TData> where TData : class, new()
        {
            ArtFolders.Ensure(folder);
            var assets = new List<TAsset>();
            foreach (var entry in data)
                assets.Add(LoadOrCreate<TAsset, TData>(CraftingPaths.AssetIn(folder, id(entry)), entry, ref created));
            return assets;
        }

        private static TAsset LoadOrCreate<TAsset, TData>(string path, TData data, ref int created)
            where TAsset : CraftingAsset<TData> where TData : class, new()
        {
            var asset = AssetDatabase.LoadAssetAtPath<TAsset>(path);
            if (asset != null) return asset;
            asset = ScriptableObject.CreateInstance<TAsset>();
            asset.Data = data;
            AssetDatabase.CreateAsset(asset, path);
            created++;
            return asset;
        }

        private static void Merge<T>(List<T> target, IEnumerable<T> assets) where T : UnityEngine.Object
        {
            target.RemoveAll(a => a == null);
            target.AddRange(assets.Where(a => !target.Contains(a)));
        }
    }
}
