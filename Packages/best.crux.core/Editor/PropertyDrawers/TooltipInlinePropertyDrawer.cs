using Crux.Core.Editor.Controls;
using Crux.Core.Runtime.Attributes;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Crux.Core.Editor.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(TooltipInlineAttribute))]
    internal class TooltipInlinePropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var field = new AnnotatedPropertyField(property);

            var attributes = fieldInfo.GetCustomAttributes(typeof(TooltipInlineAttribute), false);

            Debug.Log(attributes.Length);
            if (attributes.Length > 0)
            {
                var attr = attributes[0] as TooltipInlineAttribute;
                field.TooltipInlineText = attr!.Text;
            }
            
            field.Bind(property.serializedObject);

            return field;
        }
    }
}