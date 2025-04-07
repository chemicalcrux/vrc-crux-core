using Crux.Core.Runtime;
using Crux.Core.Runtime.Upgrades;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Crux.Core.Editor.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(OverrideItem<>))]
    public class OverrideItemPropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
                "Packages/best.crux.core/UI/Property Drawers/Override Item.uxml");

            var root = uxml.Instantiate();

            var activeToggle = root.Q<Toggle>("Active");
            var valueField = root.Q<PropertyField>("Value");

            activeToggle.RegisterCallback<ChangeEvent<bool>, PropertyField>(static (evt, valueField) =>
            {
                valueField.SetEnabled(evt.newValue);
            }, valueField);

            valueField.SetEnabled(activeToggle.value);
            valueField.label = property.displayName;
            
            return root;
        }
    }
}