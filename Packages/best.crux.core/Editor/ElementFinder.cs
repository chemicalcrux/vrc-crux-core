using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Crux.Core.Editor
{
    /// <summary>
    /// Fetches VisualTreeAssets based on either GUID+FileID or asset path. It
    /// provides an "error" document if it cannot find the asset.
    /// </summary>
    [PublicAPI]
    public static class ElementFinder
    {
        private const string Root = "Packages/best.crux.core/UI/";

        private static VisualTreeAsset GetFallbackAsset()
        {
            var fallback = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Packages/best.crux.core/UI/Missing Tree Asset.uxml");

            if (fallback == null)
            {
                Debug.LogWarning("The fallback UI document failed to load. Please report this as a bug!");
            }

            return fallback;
        }

        public static bool TryGetAssetRef(AssetReference assetReference, out VisualTreeAsset result)
        {
            if (assetReference.TryLoad(out result)) return true;
            
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