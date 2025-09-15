using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

#pragma warning disable CS8524 // The switch expression does not handle some values of its input type (it is not exhaustive) involving an unnamed enum value.

namespace Crux.Core.Editor.Controls
{
    public class EnumRevealArea : VisualElement
    {
        internal enum EnumRevealFlagsMode
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

                field!.styleSheets.Add(
                    AssetReference.ParseAndLoad<StyleSheet>("000fc17682fdd4bc391434199e6b6d4d,7433441132597879392"));

                var propertyField = new PropertyField
                {
                    bindingPath = field.Binding,
                    style =
                    {
                        display = DisplayStyle.None
                    }
                };

                field.Add(propertyField);

                if (field.EnumType != null)
                {
                    List<int> acceptedValues = null;

                    try
                    {
                        acceptedValues = field.EnumNames.Split(",")
                            .Select(name => (int)Enum.Parse(field.EnumType, name))
                            .ToList();
                    }
                    catch
                    {
                        // ignored
                    }

                    acceptedValues ??= new List<int>();

                    void UpdateDelegate(int newValue)
                    {
                        bool flag = field.FlagsMode != EnumRevealFlagsMode.Off;
                        bool accept = field.FlagsMode switch
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

                                accept = field.FlagsMode switch
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

                        if (accept)
                        {
                            field.AddToClassList("revealed");
                            field.RemoveFromClassList("unrevealed");
                        }
                        else
                        {
                            field.RemoveFromClassList("revealed");
                            field.AddToClassList("unrevealed");
                        }
                    }

                    propertyField.RegisterValueChangeCallback(evt =>
                    {
                        // this gives the actual enum value!
                        int newValue = evt.changedProperty.intValue;

                        UpdateDelegate(newValue);
                    });

                    field.schedule.Execute(() =>
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

                return field;
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
        internal EnumRevealFlagsMode FlagsMode { get; private set; }
    }
}