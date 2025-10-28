using Crux.Core.Editor.PropertyDrawers;
using Crux.Core.Samples.InspectorGadgets.Runtime;
using UnityEditor;
using UnityEngine.UIElements;

namespace Crux.Core.Samples.InspectorGadgets.Editor
{
    [CustomEditor(typeof(ExampleAssetAttributes))]
    public class ExampleAssetAttributesEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();

            SerializedProperty prop = serializedObject.GetIterator();

            GadgetPropertyDrawer.CreatePropertyFields(prop, root, false);
            return root;
        }
    }
}