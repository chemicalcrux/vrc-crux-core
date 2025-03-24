using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ChemicalCrux.CruxCore.Editor.Documentation
{
    [CreateAssetMenu]
    public class DocPage : ScriptableObject
    {
        public string title;
        public VisualTreeAsset document;

        [ContextMenu("Add sub-asset")]
        private void AddSubAsset()
        {
            VisualTreeAsset asset =
                Instantiate(AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
                    "Packages/com.chemicalcrux.crux-core/UI/Base DocPage.uxml"));
            
            Undo.RegisterCreatedObjectUndo(asset, "Add sub-asset");
            
            asset.name = title + " Document";
            
            AssetDatabase.AddObjectToAsset(asset, this);
            AssetDatabase.SaveAssetIfDirty(this);
            var so = new SerializedObject(this);
            so.FindProperty(nameof(document)).objectReferenceValue = asset;
            so.ApplyModifiedProperties();

        }

        [ContextMenu("Destroy sub-asset")]
        private void DestroySubAsset()
        {
            foreach (var asset in AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(this)))
            {
                if (AssetDatabase.IsSubAsset(asset))
                {
                    DestroyImmediate(asset, true);
                }
            }

            AssetDatabase.SaveAssetIfDirty(this);
        }
    }
    
}
