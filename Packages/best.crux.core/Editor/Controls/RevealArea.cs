using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Crux.Core.Editor.Controls
{
    public class RevealArea : VisualElement
    {
        /// <summary>
        /// Constructs a RevealArea without a binding. This should only
        /// happen if it's being created by its UxmlFactory.
        /// </summary>
        public RevealArea()
        {
            
        }

        /// <summary>
        /// Constructs a RevealArea with a binding.
        /// </summary>
        /// <param name="bindingPath"></param>
        public RevealArea(string bindingPath)
        {
            Binding = bindingPath;
            Setup();
        }
        
        public new class UxmlFactory : UxmlFactory<RevealArea, UxmlTraits>
        {
            public override VisualElement Create(IUxmlAttributes bag, CreationContext cc)
            {
                Debug.Log("Created");
                
                var field = base.Create(bag, cc) as RevealArea;

                field.Setup();

                return field;
            }
        }

        private void Setup()
        {
            var sheet = AssetReference.ParseAndLoad<StyleSheet>(
                "023178233351247d18a7c9a032dab6c1,7433441132597879392");
            
                styleSheets.Add(sheet);

            var propertyField = new PropertyField();
            propertyField.bindingPath = Binding;
            propertyField.style.display = DisplayStyle.None;

            Add(propertyField);

            void UpdateSelf(bool newValue)
            {
                if (newValue)
                {
                    AddToClassList("revealed");
                    RemoveFromClassList("unrevealed");
                }
                else
                {
                    RemoveFromClassList("revealed");
                    AddToClassList("unrevealed");
                }
            }

            propertyField.RegisterValueChangeCallback(evt =>
            {
                bool newValue = evt.changedProperty.boolValue;

                UpdateSelf(newValue);
            });

            schedule.Execute(() =>
            {
                FieldInfo fieldInfo = propertyField.GetType().GetField("m_SerializedProperty",
                    BindingFlags.NonPublic | BindingFlags.Instance);

                if (fieldInfo == null)
                {
                    Debug.LogWarning("This shouldn't happen...");
                    return;
                }

                var serializedProperty = (SerializedProperty)fieldInfo.GetValue(propertyField);

                if (serializedProperty != null)
                    UpdateSelf(serializedProperty.boolValue);
            });
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