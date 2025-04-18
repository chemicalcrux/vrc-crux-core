using Crux.Core.Editor.Controls;
using Crux.Core.Runtime;
using Crux.Core.Runtime.Attributes;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Crux.Core.Editor.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(DocRefAttribute))]
    internal class DocRefPropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var field = new AnnotatedPropertyField(property);

            var attributes = fieldInfo.GetCustomAttributes(typeof(DocRefAttribute), false);

            if (attributes.Length > 0)
            {
                var attr = attributes[0] as DocRefAttribute;
                field.SetDocManualRef(attr!.ManualRef);
                field.SetDocPageRef(attr!.PageRef);
            }
            
            field.Bind(property.serializedObject);

            return field;
        }
    }
}