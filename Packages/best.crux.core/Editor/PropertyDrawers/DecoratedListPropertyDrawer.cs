using Crux.Core.Editor.Controls;
using Crux.Core.Editor.ExtensionMethods;
using Crux.Core.Runtime;
using Crux.Core.Runtime.Attributes;
using UnityEditor;
using UnityEngine.UIElements;

namespace Crux.Core.Editor.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(DecoratedList<>))]
    public class DecoratedListPropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var inner = property.FindPropertyRelative("list");
            var field = new AnnotatedPropertyField(inner);

            if (fieldInfo.TryGetAttribute(out DocRefInertAttribute docRefAttribute))
            {
                field.SetDocManualRef(docRefAttribute.ManualRef);
                field.SetDocPageRef(docRefAttribute.PageRef);
            }

            if (fieldInfo.TryGetAttribute(out TooltipRefInertAttribute tooltipRefAttribute))
            {
                field.SetTooltipRef(tooltipRefAttribute.AssetRef);
            }

            field.label = property.displayName;

            return field;
        }
    }
}