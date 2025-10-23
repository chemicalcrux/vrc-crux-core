using System.Linq;
using Crux.Core.Runtime;
using UnityEditor;
using UnityEngine;

public static class ReserializeAssets
{
    [MenuItem("Assets/Reserialize", false, CoreConsts.ContextAssetOrder)]
    static void Reserialize()
    {
        var path = AssetDatabase.GetAssetPath(Selection.activeObject);

        Debug.Log(path);
        if (AssetDatabase.TryGetGUIDAndLocalFileIdentifier(Selection.activeObject, out var guid, out long fileID))
        {
            var targets = AssetDatabase.FindAssets("", new[] { path })
                         .Select(static guid => AssetDatabase.GUIDToAssetPath(guid));
            
            AssetDatabase.ForceReserializeAssets(targets);
        }
    }

    [MenuItem("Assets/Reserialize", true, CoreConsts.ContextAssetOrder)]
    static bool ReserializeValidation()
    {
        return Selection.activeObject && AssetDatabase.Contains(Selection.activeObject) &&
               Selection.activeObject is DefaultAsset;
    }
}