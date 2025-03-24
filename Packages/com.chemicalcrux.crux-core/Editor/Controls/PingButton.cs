using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ChemicalCrux.CruxCore.Editor.Controls
{
    public class PingButton : Button
    {
        public new class UxmlFactory : UxmlFactory<PingButton, UxmlTraits>
        {
            public override VisualElement Create(IUxmlAttributes bag, CreationContext cc)
            {
                var button = base.Create(bag, cc) as PingButton;

                button!.clicked += () =>
                {
                    Debug.Log("Clicked.");

                    Object result = null;

                    if (!string.IsNullOrEmpty(button.Guid))
                    {
                        result = AssetDatabase.LoadAssetAtPath<Object>(AssetDatabase.GUIDToAssetPath(button.Guid));
                    }
                    else if (!string.IsNullOrEmpty(button.AssetQuery))
                    {
                        result = AssetDatabase.LoadAssetAtPath<Object>(button.AssetQuery);
                    }
                    
                    EditorGUIUtility.PingObject(result);
                };
                
                return button;
            }
        }

        public new class UxmlTraits : Button.UxmlTraits
        {
            private readonly UxmlStringAttributeDescription guid = new()
                { name = "guid", defaultValue = "" };
            
            private readonly UxmlStringAttributeDescription assetQuery = new()
                { name = "asset-query", defaultValue = "" };

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                var ate = ve as PingButton;

                ate!.Guid = guid.GetValueFromBag(bag, cc);
                ate!.AssetQuery = assetQuery.GetValueFromBag(bag, cc);
            }
        }

        public string Guid { get; set; }
        public string AssetQuery { get; set; }
    }
}