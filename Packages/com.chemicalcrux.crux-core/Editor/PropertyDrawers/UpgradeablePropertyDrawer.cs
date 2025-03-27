using System.ComponentModel;
using System.Linq;
using ChemicalCrux.CruxCore.Runtime;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace ChemicalCrux.CruxCore.Editor.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(UpgradeableBase))]
    public class UpgradeablePropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var upgradeable = property.managedReferenceValue as UpgradeableBase;
            
            if (upgradeable == null)
            {
                return new Label("The managed reference value is null or isn't an UpgradeableBase! This isn't allowed.");
            }
            
            var type = upgradeable.GetType();
            
            var element =
                AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
                    "Packages/com.chemicalcrux.crux-core/UI/Property Drawers/Upgradeable.uxml");

            var root = element.Instantiate();
            var area = root.Q("PropertyArea");
            var upgradeButton = root.Q<Button>("Upgrade");

            var versionAttributes = type.GetCustomAttributes(typeof(UpgradeableVersionAttribute), true);
            var propertyDrawerAttributes = type.GetCustomAttributes(typeof(UpgradeablePropertyDrawerAttribute), true);

            bool hasPropertyDrawer = propertyDrawerAttributes.Length > 0;
            
            if (hasPropertyDrawer)
            {
                var attr = propertyDrawerAttributes[0] as UpgradeablePropertyDrawerAttribute;
                var uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(attr!.path);
                uxml.CloneTree(area);
            }
            else
            {
                var iterateOver = property.Copy();
                
                while (iterateOver.NextVisible(true))
                {
                    area.Add(new PropertyField(iterateOver));
                    area.Bind(iterateOver.serializedObject);
                }
            }

            string slug = "";
            var version = root.Q<Label>("VersionNumber");

            UpgradeableBase.GetLatestVersion(fieldInfo.FieldType);
            if (versionAttributes.Length > 0)
            {
                var attr = versionAttributes[0] as UpgradeableVersionAttribute;

                slug += "v" + attr!.version;
            }

            slug += " - " + fieldInfo.FieldType.Name + " - ";
            if (!hasPropertyDrawer)
                slug += "P";

            version.text = slug;

            int latest = UpgradeableBase.GetLatestVersion(type);

            if (latest != upgradeable.GetVersion())
            {
                upgradeButton.style.display = DisplayStyle.Flex;

                upgradeButton.clicked += () =>
                {
                    if (!upgradeable.TryUpgradeToVersion(latest, out var upgraded))
                    {
                        Debug.LogWarning("Something went wrong when trying to upgrade...");
                        return;
                    }

                    property.managedReferenceValue = upgraded;

                    property.serializedObject.ApplyModifiedProperties();

                    var oldParent = root.parent;
                    oldParent.Remove(root);
                    oldParent.Add(CreatePropertyGUI(property));
                };
            }
            else
            {
                upgradeButton.style.display = DisplayStyle.None;
            }
            return root;
        }
    }
}