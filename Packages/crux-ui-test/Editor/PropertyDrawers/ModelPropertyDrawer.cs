using ChemicalCrux.CruxCore.Editor.PropertyDrawers;
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

            if (managed is ModelV1)
            {
                 area.Add(new Label("This is a test of 'advanced' property drawers. You can run code!\n\nAnyway, here's the normal interface:"));
                 CreatePropertyFields(property, area);
            }

            return true;
        }
    }
}