using ChemicalCrux.CruxCoreTest.Runtime;
using UnityEditor;
using UnityEngine.UIElements;

namespace ChemicalCrux.CruxCoreTest.Editor.Editors
{
    [CustomEditor(typeof(ExampleAsset))]
    public class ExampleAssetEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
                "Packages/crux-ui-test/UI/Editors/Example Asset.uxml");

            return uxml.Instantiate();
        }
    }
}
