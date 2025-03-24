using ChemicalCrux.CruxCore.Editor.Windows;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ChemicalCrux.CruxCore.Editor.Controls
{
    public class DocPageButton : Button
    {
        private EditorWindow window;
        
        public new class UxmlFactory : UxmlFactory<DocPageButton, UxmlTraits>
        {
            public override VisualElement Create(IUxmlAttributes bag, CreationContext cc)
            {
                var button = base.Create(bag, cc) as DocPageButton;

                button!.clicked += () =>
                {
                    if (!AssetReference.TryParse(button.TooltipRef, out var assetRef))
                    {
                        Debug.LogWarning("A documentation page button has an invalid documentation page reference. See the above warning.");
                        return;
                    }

                    if (!ElementFinder.TryGetAssetRef(assetRef, out var uxml))
                    {
                        Debug.LogWarning("A documentation page button has an invalid documentation page reference. See the above warning.");
                        return;
                    }
                    
                };

                return button;
            }
        }

        public new class UxmlTraits : Button.UxmlTraits
        {
            // Unity 2022.3 does not support asset references here, so the backup plan is to just store the
            // GUID and FileID of the asset in a string.
            
            private readonly UxmlStringAttributeDescription tooltipRef = new()
                { name = "tooltip-ref", defaultValue = "" };
            
            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                var ate = ve as DocPageButton;

                ate!.TooltipRef = tooltipRef.GetValueFromBag(bag, cc);
            }
        }

        public string TooltipRef { get; set; }
    }
}