using ChemicalCrux.CruxCore.Editor.Controls;
using ChemicalCrux.CruxCore.Runtime;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace ChemicalCrux.CruxCore.Editor.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(TooltipRefAttribute))]
    internal class TooltipRefPropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var field = new AnnotatedPropertyField(property);

            var attributes = fieldInfo.GetCustomAttributes(typeof(TooltipRefAttribute), false);

            if (attributes.Length > 0)
            {
                var attr = attributes[0] as TooltipRefAttribute;
                field.SetTooltipRef(attr!.AssetRef);
            }

            field.Bind(property.serializedObject);

            return field;
        }
    }
}