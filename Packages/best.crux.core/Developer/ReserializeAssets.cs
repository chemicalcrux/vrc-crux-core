using System.Linq;
using Crux.Core.Runtime;
using UnityEditor;

namespace Crux.Core.Developer
{
    public static class ReserializeAssets
    {
        public static void ReserializeDirectory(string path)
        {
            if (AssetDatabase.TryGetGUIDAndLocalFileIdentifier(Selection.activeObject, out string _, out long _))
            {
                var targets = AssetDatabase.FindAssets("", new[] { path })
                    .Select(static guid => AssetDatabase.GUIDToAssetPath(guid));
            
                AssetDatabase.ForceReserializeAssets(targets);
            }
        }
    
        [MenuItem("Assets/Reserialize", false, CoreConsts.ContextAssetOrder)]
        static void Reserialize()
        {
            var path = AssetDatabase.GetAssetPath(Selection.activeObject);
            ReserializeDirectory(path);
        }

        [MenuItem("Assets/Reserialize", true, CoreConsts.ContextAssetOrder)]
        static bool ReserializeValidation()
        {
            return Selection.activeObject && AssetDatabase.Contains(Selection.activeObject) &&
                   Selection.activeObject is DefaultAsset;
        }
    }
}