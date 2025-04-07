using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Crux.Core.Editor.Controls
{
    public class RevealArea : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<RevealArea, UxmlTraits>
        {
            public override VisualElement Create(IUxmlAttributes bag, CreationContext cc)
            {
                var field = base.Create(bag, cc) as RevealArea;

                field!.styleSheets.Add(
                    AssetDatabase.LoadAssetAtPath<StyleSheet>(
                        "Packages/com.chemicalcrux.crux-core/UI/Stylesheets/RevealArea.uss"));

                var propertyField = new PropertyField();
                propertyField.bindingPath = field.Binding;
                propertyField.style.display = DisplayStyle.None;

                field.Add(propertyField);

                propertyField.RegisterValueChangeCallback(evt =>
                {
                    bool newValue = evt.changedProperty.boolValue;

                    if (newValue)
                    {
                        field.AddToClassList("revealed");
                        field.RemoveFromClassList("unrevealed");
                    }
                    else
                    {
                        field.RemoveFromClassList("revealed");
                        field.AddToClassList("unrevealed");
                    }
                });
                
                return field;
            }
        }

        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            private readonly UxmlStringAttributeDescription binding = new()
                { name = "binding", defaultValue = "" };

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                var ate = ve as RevealArea;
                
                ate!.Binding = binding.GetValueFromBag(bag, cc);
            }
        }
        
        public string Binding { get; private set; }
    }
}
