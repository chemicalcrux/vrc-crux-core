using Crux.Core.Editor.Controls;
using Crux.Core.Runtime.Attributes;
using Crux.Core.Runtime.Upgrades;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;
using TooltipRefAttribute = Crux.Core.Runtime.Attributes.TooltipRefAttribute;

namespace Crux.Core.Editor.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(UpgradableBase))]
    public class UpgradablePropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            AssetReference.TryParse("cea4d3f34a23e43389d3662cd8163f15,9197481963319205126", out var propertyDrawerRef);
            propertyDrawerRef.TryLoad(out VisualTreeAsset element);

            var root = element.Instantiate();
            var label = root.Q<Label>("Label");
            var annotations = root.Q("Annotations");
            var area = root.Q("PropertyArea");
            var message = root.Q<Label>("Message");
            var upgradeButton = root.Q<Button>("Upgrade");

            label.text = property.displayName;

            if (property.managedReferenceValue == null)
            {
                return new Label("This upgradable data is invalid! You may need to reset the entire component.");
            }

            var rootTooltipAttributes = property.managedReferenceValue.GetType()
                .GetCustomAttributes(typeof(TooltipRefAttribute), true);

            if (rootTooltipAttributes.Length > 0)
            {
                var attr = rootTooltipAttributes[0] as TooltipRefAttribute;

                var button = CoreVisualElements.TooltipButtonRef.Load().Instantiate();
                var tooltipButton = button.Q<TooltipButton>();
                tooltipButton.TooltipRef = attr!.AssetRef;

                annotations.Add(button);
            }

            var rootDocAttributes = property.managedReferenceValue.GetType()
                .GetCustomAttributes(typeof(DocRefAttribute), true);

            if (rootDocAttributes.Length > 0)
            {
                var attr = rootDocAttributes[0] as DocRefAttribute;

                var button = CoreVisualElements.DocButtonRef.Load().Instantiate();
                var docButton = button.Q<DocButton>();
                docButton.DocManualRef = attr!.ManualRef;
                docButton.DocPageRef = attr!.PageRef;

                annotations.Add(button);
            }

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
            bool hasPropertyDrawerOverride = false;

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

                    if (AssetReference.TryParse(attr!.AssetRef, out var assetRef))
                    {
                        if (ElementFinder.TryGetAssetRef(assetRef, out var uxml))
                        {
                            uxml.CloneTree(area);
                        }
                    }
                }
                else
                {
                    if (CreateMainInterface(property.Copy(), area))
                    {
                        hasPropertyDrawerOverride = true;
                    }
                    else
                    {
                        GadgetPropertyDrawer.CreatePropertyFields(property, area);
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

                        var replacement = new PropertyField(property);
                        oldParent.Add(replacement);
                        replacement.Bind(property.serializedObject);
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

            if (hasPropertyDrawer)
            {
                tags += "D";
                slugExplainerParts.Add($"<b>D:</b> A simple custom property drawer was provided for this data.");
            }

            if (hasPropertyDrawerOverride)
            {
                tags += "O";
                slugExplainerParts.Add($"<b>O:</b> An advanced custom property drawer was provided for this data.");
            }

            if (latest != version)
            {
                tags += "V";
                slugExplainerParts.Add(
                    $"<b>V:</b> The current version is {version}, which can be upgraded to version {latest}.");
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

        /// <summary>
        /// Override this method to control how the property itself gets rendered.
        /// </summary>
        /// <param name="property"></param>
        /// <param name="area"></param>
        /// <returns>Whether an interface was drawn. If false, the default will be drawn.</returns>
        protected virtual bool CreateMainInterface(SerializedProperty property, VisualElement area)
        {
            return false;
        }
    }
}