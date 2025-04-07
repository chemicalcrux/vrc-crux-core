using Crux.Core.Editor.PropertyDrawers;
using ChemicalCrux.CruxCoreTest.Runtime.Upgrading;
using UnityEditor;
using UnityEngine.UIElements;

namespace ChemicalCrux.CruxCoreTest.Editor.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(ModelV1))]
    public class ModelPropertyDrawer : UpgradablePropertyDrawer
    {
        protected override bool CreateMainInterface(SerializedProperty property, VisualElement area)
        {
            var managed = property.managedReferenceValue;

            area.Add(new Label("This is a test of 'advanced' property drawers. You can run code!\n\nThis should only appear for ModelV1.\n\nAnyway, here's the normal interface:"));
            CreatePropertyFields(property, area);

            return true;
        }
    }
}