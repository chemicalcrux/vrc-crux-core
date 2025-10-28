using Crux.Core.Editor.PropertyDrawers;
using Crux.Core.Samples.UpgradableData.Runtime;
using UnityEditor;
using UnityEngine.UIElements;

namespace Crux.Core.Samples.UpgradableData.Editor
{
    [CustomPropertyDrawer(typeof(ModelV1))]
    public class ModelPropertyDrawer : UpgradablePropertyDrawer
    {
        protected override bool CreateMainInterface(SerializedProperty property, VisualElement area)
        {
            area.Add(new Label("This is a test of 'advanced' property drawers. You can run code!\n\nThis should only appear for ModelV1.\n\nAnyway, here's the normal interface:"));
            GadgetPropertyDrawer.CreatePropertyFields(property, area);

            return true;
        }
    }

    [CustomPropertyDrawer(typeof(ModelV3.Thing))]
    public class ModelGadgetDrawer : GadgetPropertyDrawer
    {
        
    }
}