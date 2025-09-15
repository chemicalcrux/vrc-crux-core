using Crux.Core.Editor;
using Crux.Core.Samples.InspectorGadgets.Runtime;
using UnityEditor;
using UnityEngine.UIElements;

namespace Crux.Core.Samples.InspectorGadgets.Editor
{
    [CustomEditor(typeof(ExampleAsset))]
    public class ExampleAssetEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            AssetReference.TryParse("12fa89148c6e54dbbae8a35739839a55,9197481963319205126", out var assetRef);
            assetRef.TryLoad(out VisualTreeAsset uxml);
            return uxml.Instantiate();
        }
    }
}
