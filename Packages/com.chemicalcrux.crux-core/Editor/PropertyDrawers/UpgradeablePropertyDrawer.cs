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
            var element =
                AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
                    "Packages/com.chemicalcrux.crux-core/UI/Property Drawers/Upgradeable.uxml");

            var root = element.Instantiate();
            var label = root.Q<Label>("Label");
            var area = root.Q("PropertyArea");
            var message = root.Q<Label>("Message");
            var upgradeButton = root.Q<Button>("Upgrade");

            label.text = property.displayName;

            var upgradable = property.managedReferenceValue as UpgradeableBase;

            if (upgradable == null)
            {
                message.text = "The managed reference value is null or isn't an UpgradeableBase! This isn't allowed.";
                message.style.display = DisplayStyle.Flex;
                return root;
            }

            var type = upgradable.GetType();

            upgradeButton.style.display = DisplayStyle.None;

            var versionAttributes = type.GetCustomAttributes(typeof(UpgradeableVersionAttribute), true);
            var propertyDrawerAttributes = type.GetCustomAttributes(typeof(UpgradeablePropertyDrawerAttribute), true);

            bool hasPropertyDrawer = propertyDrawerAttributes.Length > 0;

            var targetObj = property.serializedObject.targetObject;

            if (PrefabUtility.IsPartOfPrefabInstance(targetObj))
            {
                message.text =
                    "You aren't allowed to modify a prefab's upgradeable data. Please use an override component instead.";
                message.style.display = DisplayStyle.Flex;
            }
            else
            {
                if (hasPropertyDrawer)
                {
                    var attr = propertyDrawerAttributes[0] as UpgradeablePropertyDrawerAttribute;
                    var uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(attr!.path);
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
                        area.Add(new PropertyField(iterateOver));
                        area.Bind(iterateOver.serializedObject);

                        while (iterateOver.NextVisible(true) && !SerializedProperty.EqualContents(iterateOver, skipTo))
                        {
                        }
                    }
                }
                int latest = UpgradeableBase.GetLatestVersion(type);

                if (latest != upgradable.GetVersion())
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
            return root;
        }
    }
}