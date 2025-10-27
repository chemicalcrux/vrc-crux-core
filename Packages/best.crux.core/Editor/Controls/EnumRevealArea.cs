using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Crux.Core.Runtime.Attributes;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

#pragma warning disable CS8524 // The switch expression does not handle some values of its input type (it is not exhaustive) involving an unnamed enum value.

namespace Crux.Core.Editor.Controls
{
    [PublicAPI]
    public class EnumRevealArea : VisualElement
    {
        public EnumRevealArea()
        {
        }

        public EnumRevealArea(string bindingPath, Type enumType, BeginEnumRevealAreaAttribute.EnumFlagKind mode,
            object[] enumValues)
        {
            Binding = bindingPath;
            EnumType = enumType;
            EnumNames = string.Join(",", enumValues.Select(value => Enum.GetName(enumType, value)));

            Debug.Log(EnumNames);
            FlagsMode = mode switch
            {
                BeginEnumRevealAreaAttribute.EnumFlagKind.Off => EnumRevealFlagsMode.Off,
                BeginEnumRevealAreaAttribute.EnumFlagKind.Any => EnumRevealFlagsMode.Any,
                BeginEnumRevealAreaAttribute.EnumFlagKind.All => EnumRevealFlagsMode.All,
                BeginEnumRevealAreaAttribute.EnumFlagKind.None => EnumRevealFlagsMode.None,
                BeginEnumRevealAreaAttribute.EnumFlagKind.NotAll => EnumRevealFlagsMode.NotAll,
            };

            Setup();
        }

        public enum EnumRevealFlagsMode
        {
            Off = 0,
            Any = 1,
            All = 2,
            None = 3,
            NotAll = 4
        }

        public new class UxmlFactory : UxmlFactory<EnumRevealArea, UxmlTraits>
        {
            public override VisualElement Create(IUxmlAttributes bag, CreationContext cc)
            {
                var field = base.Create(bag, cc) as EnumRevealArea;

                field!.Setup();

                return field;
            }
        }

        private void Setup()
        {
            styleSheets.Add(
                AssetReference.ParseAndLoad<StyleSheet>("000fc17682fdd4bc391434199e6b6d4d,7433441132597879392"));

            var propertyField = new PropertyField
            {
                bindingPath = Binding,
                style =
                {
                    display = DisplayStyle.None
                }
            };

            Add(propertyField);

            if (EnumType != null)
            {
                try
                {
                    acceptedValues.AddRange(EnumNames.Split(",")
                        .Select(enumName => (int)Enum.Parse(EnumType, enumName)));
                }
                catch
                {
                    // ignored
                }

                propertyField.RegisterValueChangeCallback(evt =>
                {
                    // this gives the actual enum value!
                    int newValue = evt.changedProperty.intValue;

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
                        UpdateDelegate(serializedProperty.intValue);
                });
            }
        }

        public void UpdateDelegate(int newValue)
        {
            bool flag = FlagsMode != EnumRevealFlagsMode.Off;
            bool accept = FlagsMode switch
            {
                EnumRevealFlagsMode.Off => false,
                EnumRevealFlagsMode.Any => false,
                EnumRevealFlagsMode.All => true,
                EnumRevealFlagsMode.None => true,
                EnumRevealFlagsMode.NotAll => false
            };

            if (flag)
            {
                foreach (var val in acceptedValues)
                {
                    bool match = (newValue & val) != 0;

                    accept = FlagsMode switch
                    {
                        EnumRevealFlagsMode.Off => accept,
                        EnumRevealFlagsMode.Any => accept || match,
                        EnumRevealFlagsMode.All => accept && match,
                        EnumRevealFlagsMode.None => accept && !match,
                        EnumRevealFlagsMode.NotAll => accept || !match
                    };
                }
            }
            else
            {
                accept = acceptedValues.Contains(newValue);
            }

            Debug.Log("Property: " + Binding);
            Debug.Log("Value: " + newValue);
            Debug.Log("New state: " + accept);
            Debug.Log("Type: " + EnumType);
            Debug.Log(string.Join(",", acceptedValues));
            if (accept)
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

            private readonly UxmlTypeAttributeDescription<Enum> enumType = new()
                { name = "enum-type" };

            private readonly UxmlStringAttributeDescription enumNames = new()
                { name = "enum-names", defaultValue = "" };

            private readonly UxmlEnumAttributeDescription<EnumRevealFlagsMode> flagsMode = new()
                { name = "flags-mode", defaultValue = EnumRevealFlagsMode.Off };

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                var ate = ve as EnumRevealArea;

                ate!.Binding = binding.GetValueFromBag(bag, cc);
                ate!.EnumType = enumType.GetValueFromBag(bag, cc);
                ate!.EnumNames = enumNames.GetValueFromBag(bag, cc);
                ate!.FlagsMode = flagsMode.GetValueFromBag(bag, cc);
            }
        }

        public string Binding { get; private set; }
        public Type EnumType { get; private set; }
        public string EnumNames { get; private set; }
        public EnumRevealFlagsMode FlagsMode { get; private set; }

        private List<int> acceptedValues = new();
    }
}