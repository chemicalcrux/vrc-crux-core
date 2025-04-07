using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Crux.Core.Editor.Controls
{
    /// <summary>
    /// Pings an object when clicked.
    /// </summary>
    public class PingButton : Button
    {
        public new class UxmlFactory : UxmlFactory<PingButton, UxmlTraits>
        {
            public override VisualElement Create(IUxmlAttributes bag, CreationContext cc)
            {
                var button = base.Create(bag, cc) as PingButton;

                button!.clicked += () =>
                {
                    Object result = null;

                    if (!string.IsNullOrEmpty(button.AssetRef))
                    {
                        if (!AssetReference.TryParse(button.AssetRef, out var assetRef))
                        {
                            Debug.LogWarning("A ping button has an invalid asset reference. See the previous warning.");
                            return;
                        }

                        if (!assetRef.TryLoad(out result))
                        {
                            Debug.LogWarning("A ping button failed to load the asset it wants to show you.");
                            return;
                        }
                    }
                    else if (!string.IsNullOrEmpty(button.AssetPath))
                    {
                        result = AssetDatabase.LoadAssetAtPath<Object>(button.AssetPath);
                    }
                    else if (!string.IsNullOrEmpty(button.AssetQuery))
                    {
                        var matches = AssetDatabase.FindAssets(button.AssetQuery);

                        if (matches.Length > 0)
                        {
                            var path = AssetDatabase.GUIDToAssetPath(matches[0]);
                            result = AssetDatabase.LoadAssetAtPath<Object>(path);
                        }
                    }

                    if (result == null)
                    {
                        Debug.LogWarning("A ping button couldn't find an asset to ping!");
                    }
                    
                    EditorGUIUtility.PingObject(result);
                };
                
                return button;
            }
        }

        public new class UxmlTraits : Button.UxmlTraits
        {
            private readonly UxmlStringAttributeDescription assetRef = new()
                { name = "asset-ref", defaultValue = "" };
            
            private readonly UxmlStringAttributeDescription assetPath = new()
                { name = "asset-path", defaultValue = "" };
            
            private readonly UxmlStringAttributeDescription assetQuery = new()
                { name = "asset-query", defaultValue = "" };

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                var ate = ve as PingButton;

                ate!.AssetRef = assetRef.GetValueFromBag(bag, cc);
                ate!.AssetPath = assetPath.GetValueFromBag(bag, cc);
                ate!.AssetQuery = assetQuery.GetValueFromBag(bag, cc);
            }
        }

        public string AssetRef { get; set; }
        public string AssetPath { get; set; }
        public string AssetQuery { get; set; }
    }
}