using System.Reflection;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Crux.Core.Editor.Controls
{
    [PublicAPI]
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
        /// <param name="boolValue"></param>
        public RevealArea(string bindingPath, bool boolValue = true)
        {
            Binding = bindingPath;
            BoolValue = boolValue;
            Setup();
        }
        
        public new class UxmlFactory : UxmlFactory<RevealArea, UxmlTraits>
        {
            public override VisualElement Create(IUxmlAttributes bag, CreationContext cc)
            {
                Debug.Log("Created");
                
                var field = base.Create(bag, cc) as RevealArea;

                field!.Setup();

                return field;
            }
        }

        private void Setup()
        {
            var sheet = AssetReference.ParseAndLoad<StyleSheet>(
                "023178233351247d18a7c9a032dab6c1,7433441132597879392");
            
                styleSheets.Add(sheet);

            var propertyField = new PropertyField
            {
                bindingPath = Binding,
                style =
                {
                    display = DisplayStyle.None
                }
            };

            Add(propertyField);

            propertyField.RegisterValueChangeCallback(evt =>
            {
                bool newValue = evt.changedProperty.boolValue;

                UpdateDelegate(newValue);
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
                    UpdateDelegate(serializedProperty.boolValue);
            });
        }
        
        public void UpdateDelegate(bool newValue)
        {
            if (newValue == BoolValue)
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

        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            private readonly UxmlStringAttributeDescription binding = new()
                { name = "binding", defaultValue = "" };

            private readonly UxmlBoolAttributeDescription boolValue = new()
                { name = "bool-value", defaultValue = true };

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                var ate = ve as RevealArea;

                ate!.Binding = binding.GetValueFromBag(bag, cc);
                ate.BoolValue = boolValue.GetValueFromBag(bag, cc);
            }
        }

        public string Binding { get; private set; }
        public bool BoolValue { get; private set; }
    }
}