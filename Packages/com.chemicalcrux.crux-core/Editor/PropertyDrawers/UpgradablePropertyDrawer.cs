using ChemicalCrux.CruxCore.Runtime.Upgrades;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;

namespace ChemicalCrux.CruxCore.Editor.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(UpgradableBase))]
    public class UpgradablePropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var element =
                AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
                    "Packages/com.chemicalcrux.crux-core/UI/Property Drawers/Upgradable.uxml");

            var root = element.Instantiate();
            var label = root.Q<Label>("Label");
            var area = root.Q("PropertyArea");
            var message = root.Q<Label>("Message");
            var upgradeButton = root.Q<Button>("Upgrade");

            label.text = property.displayName;

            var upgradable = property.managedReferenceValue as UpgradableBase;

            if (upgradable == null)
            {
                message.text = "The managed reference value is null or isn't an UpgradableBase! This isn't allowed.";
                message.style.display = DisplayStyle.Flex;
                return root;
            }

            var type = upgradable.GetType();

            upgradeButton.style.display = DisplayStyle.None;

            var propertyDrawerAttributes = type.GetCustomAttributes(typeof(UpgradablePropertyDrawerAttribute), true);

            bool hasPropertyDrawer = propertyDrawerAttributes.Length > 0;

            var targetObj = property.serializedObject.targetObject;
                
            int latest = UpgradableBase.GetLatestVersion(type);
            int version = upgradable.GetVersion();

            bool prefabInstance = PrefabUtility.IsPartOfPrefabInstance(targetObj);
            if (prefabInstance)
            {
                message.text =
                    "You aren't allowed to modify a prefab's upgradable data. Please use an override component instead.";
                message.style.display = DisplayStyle.Flex;
            }
            else
            {
                if (hasPropertyDrawer)
                {
                    var attr = propertyDrawerAttributes[0] as UpgradablePropertyDrawerAttribute;
                    var uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(attr!.Path);
                    uxml.CloneTree(area);
                }
                else
                {
                    var iterateOver = property.Copy();
                    var end = iterateOver.GetEndProperty(true);

                    iterateOver.Next(true);

                    while (!SerializedProperty.EqualContents(iterateOver, end))
                    {
                        var skipTo = iterateOver.GetEndProperty(false);

                        var field = new PropertyField(iterateOver);
                        
                        area.Add(field);
                        area.Bind(iterateOver.serializedObject);

                        while (iterateOver.NextVisible(true) && !SerializedProperty.EqualContents(iterateOver, skipTo))
                        {
                            
                        }
                    }
                }

                if (latest != version)
                {
                    upgradeButton.style.display = DisplayStyle.Flex;

                    upgradeButton.clicked += () =>
                    {
                        if (!upgradable.TryUpgradeToVersion(latest, out var upgraded))
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
            }

            using var handle = ListPool<string>.Get(out var slugParts);
            using var handle2 = ListPool<string>.Get(out var slugExplainerParts);
            
            var slugElement = root.Q<Label>("VersionNumber");

            UpgradableBase.GetLatestVersion(fieldInfo.FieldType);

            if (version >= 0)
            {
                slugParts.Add($"v{version}");
                slugExplainerParts.Add($"Version: {version}");
            }
            else
            {
                slugParts.Add("vX");
                slugExplainerParts.Add("No version number found! <b>This should never happen!</b>");
            }

            slugParts.Add(fieldInfo.FieldType.Name);
            slugExplainerParts.Add($"The base type is {fieldInfo.FieldType.Name}.");

            string tags = "";
            
            if (!hasPropertyDrawer)
            {
                tags += "D";
                slugExplainerParts.Add($"<b>D:</b> No custom property drawer was provided for this data.");
            }

            if (latest != version)
            {
                tags += "V";
                slugExplainerParts.Add($"<b>V:</b> The current version is {version}, which can be upgraded to version {latest}.");
            }

            if (prefabInstance)
            {
                tags += "P";
                slugExplainerParts.Add("<b>P:</b> This is part of a prefab instance, so it may not be modified.");
            }
            
            slugParts.Add(tags);

            slugElement.text = string.Join(" - ", slugParts);
            slugElement.tooltip = string.Join("\n\n", slugExplainerParts);

            return root;
        }
    }
}