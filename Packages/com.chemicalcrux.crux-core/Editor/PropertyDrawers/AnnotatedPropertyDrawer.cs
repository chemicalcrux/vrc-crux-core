using ChemicalCrux.CruxCore.Editor.Controls;
using ChemicalCrux.CruxCore.Editor.ExtensionMethods;
using ChemicalCrux.CruxCore.Runtime;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace ChemicalCrux.CruxCore.Editor.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(DocRefAttribute))]
    [CustomPropertyDrawer(typeof(TooltipRefAttribute))]
    public class AnnotatedPropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            Debug.Log(property);
            var field = new AnnotatedPropertyField(property);

            var attributes = fieldInfo.GetCustomAttributes(typeof(DocRefAttribute), false);

            if (attributes.Length > 0)
            {
                var attr = attributes[0] as DocRefAttribute;
                field.SetDocManualRef(attr!.ManualRef);
                field.SetDocPageRef(attr!.PageRef);
            }

            attributes = fieldInfo.GetCustomAttributes(typeof(TooltipRefAttribute), false);

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