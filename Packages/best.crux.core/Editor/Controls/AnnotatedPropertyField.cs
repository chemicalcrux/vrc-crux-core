using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Crux.Core.Editor.Controls
{
    /// <summary>
    /// Allows for a <see cref="PropertyField"/> to include some extra buttons, such as
    /// <see cref="DocButton"/> and <see cref="TooltipButton"/>.
    /// </summary>
    public class AnnotatedPropertyField : PropertyField
    {
        private int buttons = 0;
        private float expected;


        public AnnotatedPropertyField()
        {
        }

        public AnnotatedPropertyField(SerializedProperty property) : base(property)
        {
            RegisterCallback<GeometryChangedEvent, AnnotatedPropertyField>(InsertButton, this);
        }

        public void SetTooltipRef(string assetRef)
        {
            this.TooltipRef = assetRef;
        }

        public void SetDocManualRef(string assetRef)
        {
            this.DocManualRef = assetRef;
        }

        public void SetDocPageRef(string assetRef)
        {
            this.DocPageRef = assetRef;
        }

        public new class UxmlFactory : UxmlFactory<AnnotatedPropertyField, UxmlTraits>
        {
            public override VisualElement Create(IUxmlAttributes bag, CreationContext cc)
            {
                var field = base.Create(bag, cc) as AnnotatedPropertyField;

                field!.RegisterCallback<GeometryChangedEvent, AnnotatedPropertyField>(InsertButton, field);

                return field;
            }
        }

        private static void InsertButton(GeometryChangedEvent _, AnnotatedPropertyField field)
        {
            var sibling = field.Q(classes: "unity-toggle__text") ?? field.Q(classes: "unity-base-field__label");

            if (sibling == null)
            {
                // Debug.LogWarning("Couldn't find a place to insert the button for " + field + "!");
                return;
            }

            if (!string.IsNullOrEmpty(field.TooltipRef))
                InsertTooltipButton(field, sibling);

            if (!string.IsNullOrEmpty(field.DocManualRef) || !string.IsNullOrEmpty(field.DocPageRef))
                InsertDocButton(field, sibling);

            field.UnregisterCallback<GeometryChangedEvent, AnnotatedPropertyField>(InsertButton);

            sibling.style.marginRight = sibling.style.marginRight.value.value - field.buttons * 15;
            sibling.style.marginLeft = 2;
        }

        private static void InsertTooltipButton(AnnotatedPropertyField field, VisualElement sibling)
        {
            var uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
                "Packages/best.crux.core/UI/Templates/Tooltip Button.uxml");
            var tree = uxml.Instantiate();
            var button = tree.Q<TooltipButton>();
            button.TooltipRef = field.TooltipRef;

            sibling.parent.hierarchy.Insert(sibling.parent.IndexOf(sibling), button);
            ++field.buttons;
        }

        private static void InsertDocButton(AnnotatedPropertyField field, VisualElement sibling)
        {
            var uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
                "Packages/best.crux.core/UI/Templates/Doc Button.uxml");
            var tree = uxml.Instantiate();
            var button = tree.Q<DocButton>();

            button.DocManualRef = field.DocManualRef;
            button.DocPageRef = field.DocPageRef;

            sibling.parent.hierarchy.Insert(sibling.parent.IndexOf(sibling), button);
            ++field.buttons;
        }

        public new class UxmlTraits : PropertyField.UxmlTraits
        {
            private readonly UxmlStringAttributeDescription tooltipRef = new()
                { name = "tooltip-ref", defaultValue = "" };

            private readonly UxmlStringAttributeDescription docManualRef = new()
                { name = "doc-manual-ref", defaultValue = "" };

            private readonly UxmlStringAttributeDescription docPageRef = new()
                { name = "doc-page-ref", defaultValue = "" };

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                var ate = ve as AnnotatedPropertyField;

                ate!.TooltipRef = tooltipRef.GetValueFromBag(bag, cc);
                ate!.DocManualRef = docManualRef.GetValueFromBag(bag, cc);
                ate!.DocPageRef = docPageRef.GetValueFromBag(bag, cc);
            }
        }

        private string TooltipRef { get; set; }
        private string DocManualRef { get; set; }
        private string DocPageRef { get; set; }
    }
}