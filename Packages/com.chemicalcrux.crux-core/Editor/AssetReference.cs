using System;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace ChemicalCrux.CruxCore.Editor
{
    /// <summary>
    /// Represents a specific Unity asset.
    /// </summary>
    [PublicAPI]
    public class AssetReference
    {
        private readonly string guid;
        private readonly long fileID;

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
                if (AssetDatabase.TryGetGUIDAndLocalFileIdentifier(asset, out var assetGuid, out long assetFileID))
                {
                    if (guid == assetGuid && fileID == assetFileID)
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

    /// <summary>
    /// A more specific AssetReference that checks if the reference is valid when constructed.
    /// Saves a bit of typing for known-good assets.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SafeAssetReference<T> : AssetReference where T : UnityEngine.Object
    {
        public SafeAssetReference(string guid, long fileID) : base(guid, fileID)
        {
            if (!TryLoad(out T _))
            {
                throw new Exception("Invalid asset reference!");
            }
        }
        
        public T Load()
        {
            // this definitely works, unless somebody has gone and deleted the asset.
            // i would simply not do that.
            TryLoad(out T loaded);
            return loaded;
        }
    }
}