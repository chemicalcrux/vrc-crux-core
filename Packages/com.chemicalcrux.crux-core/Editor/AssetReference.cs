using UnityEditor;
using UnityEngine;

namespace ChemicalCrux.CruxCore.Editor
{
    public class AssetReference
    {
        public readonly string guid;
        public readonly long fileID;

        public AssetReference(string guid, long fileID)
        {
            this.guid = guid;
            this.fileID = fileID;
        }

        public bool TryLoad<T>(out T result)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);

            if (string.IsNullOrEmpty(path))
            {
                Debug.LogWarning($"Couldn't find a path for GUID {guid}.");
                result = default;
                return false;
            }
            
            foreach (var asset in AssetDatabase.LoadAllAssetsAtPath(path))
            {
                if (AssetDatabase.TryGetGUIDAndLocalFileIdentifier(asset, out var assetGuid, out long assetFileid))
                {
                    if (guid == assetGuid && fileID == assetFileid)
                    {
                        if (asset is T specificAsset)
                        {
                            result = specificAsset;
                            return true;
                        }
                        
                        Debug.LogWarning(
                            $"Found an asset with GUID {guid} and FileID {fileID}, but it's not a {typeof(T).Name}! It's a {asset.GetType().Name}.");

                        result = default;
                        return false;
                    }
                }
            }

            Debug.LogWarning($"Found a path for GUID {guid}, but couldn't find FileID {fileID}.");

            result = default;
            return false;
        }
        
        public static bool TryParse (string serialized, out AssetReference result)
        {
            var parts = serialized.Split(",");

            if (parts.Length != 2 || !GUID.TryParse(parts[0], out var _) || !long.TryParse(parts[1], out var fileID))
            {
                Debug.LogWarning("An asset reference was invalid. Please report this as a bug!\nReference: " + serialized);
                result = new("", 0);
                return false;
            }

            result = new AssetReference(parts[0], fileID);
            return true;
        }
    }
}