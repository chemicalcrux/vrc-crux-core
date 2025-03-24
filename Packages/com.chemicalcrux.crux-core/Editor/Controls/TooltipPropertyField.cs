using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace ChemicalCrux.CruxCore.Editor.Controls
{
    public class TooltipPropertyField : PropertyField
    {
        public new class UxmlFactory : UxmlFactory<TooltipPropertyField, UxmlTraits>
        {
            public override VisualElement Create(IUxmlAttributes bag, CreationContext cc)
            {
                var field = base.Create(bag, cc) as TooltipPropertyField;

                field!.RegisterCallback<GeometryChangedEvent, TooltipPropertyField>(InsertButton, field);

                return field;
            }
        }

        private static void InsertButton(GeometryChangedEvent _, TooltipPropertyField field)
        {
            var sibling = field.Q(classes: "unity-toggle__text") ?? field.Q(classes: "unity-base-field__label");

            if (sibling == null)
            {
                // Debug.LogWarning("Couldn't find a place to insert the button for " + field + "!");
                return;
            }

            var uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
                "Packages/com.chemicalcrux.crux-core/UI/Templates/Tooltip Button.uxml");
            var tree = uxml.Instantiate();
            var button = tree.Q<TooltipButton>();
            button.TooltipRef = field.TooltipDocument;

            sibling.parent.hierarchy.Insert(sibling.parent.IndexOf(sibling), button);


            field.UnregisterCallback<GeometryChangedEvent, TooltipPropertyField>(InsertButton);
        }

        public new class UxmlTraits : PropertyField.UxmlTraits
        {
            private readonly UxmlStringAttributeDescription tooltipDocument = new() { name = "tooltip-document", defaultValue = ""};

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                var ate = ve as TooltipPropertyField;

                ate!.TooltipDocument = tooltipDocument.GetValueFromBag(bag, cc);
            }
        }

        private string TooltipDocument { get; set; }
    }
}