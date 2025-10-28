using Crux.Core.Editor.Windows;
using UnityEngine;
using UnityEngine.UIElements;

namespace Crux.Core.Editor.Controls
{
    /// <summary>
    /// Displays a tooltip window when clicked.
    /// </summary>
    public class TooltipInlineButton : Button
    {
        public new class UxmlFactory : UxmlFactory<TooltipInlineButton, UxmlTraits>
        {
            public override VisualElement Create(IUxmlAttributes bag, CreationContext cc)
            {
                var button = base.Create(bag, cc) as TooltipInlineButton;

                button!.clicked += () =>
                {
                    UnityEditor.PopupWindow.Show(button.worldBound, new TooltipInlineWindow(new TooltipInlineWindow.TooltipInlineData
                    {
                        text = button.TooltipText
                    }));
                };

                return button;
            }
        }

        public new class UxmlTraits : Button.UxmlTraits
        {
            // Unity 2022.3 does not support asset references here, so the backup plan is to just store the
            // GUID and FileID of the asset in a string.
            
            private readonly UxmlStringAttributeDescription tooltipText = new()
                { name = "tooltip-text", defaultValue = "" };
            
            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                var ate = ve as TooltipInlineButton;

                ate!.TooltipText = tooltipText.GetValueFromBag(bag, cc);
            }
        }

        public string TooltipText { get; set; }
    }
}