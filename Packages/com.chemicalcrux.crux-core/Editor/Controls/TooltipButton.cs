using Crux.Core.Editor.Windows;
using UnityEngine;
using UnityEngine.UIElements;

namespace Crux.Core.Editor.Controls
{
    /// <summary>
    /// Displays a tooltip window when clicked.
    /// </summary>
    public class TooltipButton : Button
    {
        public new class UxmlFactory : UxmlFactory<TooltipButton, UxmlTraits>
        {
            public override VisualElement Create(IUxmlAttributes bag, CreationContext cc)
            {
                var button = base.Create(bag, cc) as TooltipButton;

                button!.clicked += () =>
                {
                    if (!AssetReference.TryParse(button.TooltipRef, out var assetRef))
                    {
                        Debug.LogWarning("A tooltip button has an invalid tooltip reference. See the above warning.");
                    }

                    if (!ElementFinder.TryGetAssetRef(assetRef, out var uxml))
                    {
                        Debug.LogWarning("A tooltip button has an invalid tooltip reference. See the above warning.");
                    }
                    
                    UnityEditor.PopupWindow.Show(button.worldBound, new TooltipWindow(uxml));
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
                var ate = ve as TooltipButton;

                ate!.TooltipRef = tooltipRef.GetValueFromBag(bag, cc);
            }
        }

        public string TooltipRef { get; set; }
    }
}