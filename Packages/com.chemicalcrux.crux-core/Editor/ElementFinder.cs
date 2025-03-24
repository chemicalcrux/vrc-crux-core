using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ChemicalCrux.CruxCore.Editor
{
    /// <summary>
    /// Fetches VisualTreeAssets based on either GUID+FileID or asset path.
    /// </summary>
    [PublicAPI]
    public static class ElementFinder
    {
        private const string Root = "Packages/com.chemicalcrux.crux-core/UI/";

        private static VisualTreeAsset GetFallbackAsset()
        {
            var fallback = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Packages/com.chemicalcrux.crux-core/UI/Missing Tree Asset.uxml");

            if (fallback == null)
            {
                Debug.LogWarning("The fallback UI document failed to load. Please report this as a bug!");
            }

            return fallback;
        }

        public static bool TryGetAssetRef(AssetReference assetReference, out VisualTreeAsset result)
        {
            return TryGetGuid(assetReference.guid, assetReference.fileID, out result);
        }

        public static bool TryGetGuid(string guid, long fileid, out VisualTreeAsset result)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);

            if (string.IsNullOrEmpty(path))
            {
                Debug.LogWarning($"Could not find a path for GUID {guid}. Please report this as a bug!");
                result = GetFallbackAsset();
                return false;
            }
            
            foreach (var asset in AssetDatabase.LoadAllAssetsAtPath(path))
            {
                if (AssetDatabase.TryGetGUIDAndLocalFileIdentifier(asset, out var assetGuid, out long assetFileid))
                {
                    if (guid == assetGuid && fileid == assetFileid)
                    {
                        if (asset is VisualTreeAsset visualTreeAsset)
                        {
                            result = visualTreeAsset;
                            return true;
                        }
                        else
                        {
                            Debug.LogWarning(
                                $"Found an asset with GUID {guid} and FileID {fileid}, but it's not a VisualTreeAsset! It's a {asset}. Please report this as a bug!");

                            result = GetFallbackAsset();
                            return false;
                        }
                    }
                }
            }

            Debug.LogWarning($"Found a path for GUID {guid}, but couldn't find FileID {fileid}");
            result = GetFallbackAsset();
            return false;
        }

        public static bool TryGetPath(string path, out VisualTreeAsset result)
        {
            result = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);

            if (!result)
            {
                Debug.LogWarning($"Failed to load a visual tree asset named {path}. Please report this as a bug!");
                result = GetFallbackAsset();
                return false;
            }

            return true;
        }
    }
}