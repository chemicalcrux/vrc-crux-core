using Crux.Core.Editor.PropertyDrawers;
using Crux.Core.Samples.InspectorGadgets.Runtime;
using UnityEditor;

namespace Crux.Core.Samples.InspectorGadgets.Editor
{
    [CustomPropertyDrawer(typeof(ExampleAsset.Inner1))]
    public class ExampleAssetPropertyDrawers : GadgetPropertyDrawer
    {
        
    }
}