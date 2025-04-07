using Crux.Core.Editor.Controls;
using Crux.Core.Runtime;
using Crux.Core.Runtime.Upgrades;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Crux.Core.Editor.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(OverrideReference<,>))]
    public class OverrideReferencePropertyDrawer : PropertyDrawer
    {
        enum Case
        {
            NonPrefab,
            NearPrefab,
            FarPrefab
        }
        
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var targetObj = property.serializedObject.targetObject;

            Case result;
            
            if (PrefabUtility.IsPartOfPrefabInstance(targetObj))
            {
                var original = PrefabUtility.GetCorrespondingObjectFromOriginalSource(targetObj);
                var nearest = PrefabUtility.GetCorrespondingObjectFromSource(targetObj);

                if (nearest == original)
                {
                    result = Case.NearPrefab;
                }
                else
                {
                    result = Case.FarPrefab;
                }
            }
            else
            {
                result = Case.NonPrefab;
            }

            var uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
                "Packages/best.crux.core/UI/Property Drawers/Override Reference.uxml");

            var instantiated = uxml.Instantiate();
            
            if (result == Case.NonPrefab)
            {
                instantiated.Q<Label>("Message").style.display = DisplayStyle.None;
            }
            else if (result == Case.NearPrefab)
            {
                instantiated.Q<Label>("Message").style.display = DisplayStyle.None;
            }
            else if (result == Case.FarPrefab)
            {
                instantiated.Q<Label>("Message").text = "This is NOT the outermost layer of a prefab. You can't add an override here.";
                instantiated.Q<PropertyField>("Reference").style.display = DisplayStyle.None;
            }
            
            return instantiated;
        }
    }
}