using System.Collections.Generic;
using System.Linq;
using Crux.Core.Editor.Controls;
using Crux.Core.Editor.ExtensionMethods;
using Crux.Core.Runtime.Attributes;
using Crux.Core.Runtime.Diagnostics;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Crux.Core.Editor.PropertyDrawers
{
    [PublicAPI]
    [CustomPropertyDrawer(typeof(DrawGadgetsAttribute))]
    public class GadgetPropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement root = new VisualElement();
            CreatePropertyFields(property, root);
            return root;
        }

        /// <summary>
        /// Draws all top-level children of the given SerializedProperty.
        ///
        /// This handles the following attributes:
        ///
        /// <ul>
        /// <li><see cref="BeginRevealAreaAttribute"/></li>
        /// <li><see cref="BeginEnumRevealAreaAttribute"/></li>
        /// <li><see cref="EndRevealAreaAttribute"/></li>
        /// </ul>
        /// </summary>
        /// <param name="property"></param>
        /// <param name="target"></param>
        public static void CreatePropertyFields(SerializedProperty property, VisualElement target)
        {
            var iterateOver = property.Copy();
            var end = iterateOver.GetEndProperty(true);

            iterateOver.Next(true);

            Stack<VisualElement> targetStack = new();
            targetStack.Push(target);

            while (!SerializedProperty.EqualContents(iterateOver, end))
            {
                var skipTo = iterateOver.GetEndProperty(false);

                foreach (var attribute in iterateOver.GetAttributes<BeginRevealAreaAttribute>())
                {
                    string sourcePath = string.Join(".", iterateOver.propertyPath.Split(".").SkipLast(1));
                    sourcePath += "." + attribute.Property;
                    RevealArea area = new RevealArea(sourcePath);
                    targetStack.Peek().Add(area);
                    targetStack.Push(area);
                }

                foreach (var attribute in iterateOver.GetAttributes<BeginEnumRevealAreaAttribute>())
                {
                    string sourcePath = string.Join(".", iterateOver.propertyPath.Split(".").SkipLast(1));
                    sourcePath += "." + attribute.Property;
                    EnumRevealArea area = new EnumRevealArea(sourcePath, attribute.EnumType, attribute.EnumValue,
                        attribute.FlagsUsage);
                    targetStack.Peek().Add(area);
                    targetStack.Push(area);
                }

                foreach (var _ in iterateOver.GetAttributes<EndRevealAreaAttribute>())
                {
                    if (targetStack.Count > 1)
                        targetStack.Pop();
                    else
                        CoreLog.LogError("Tried to close a RevealArea that did not exist.");
                }

                var field = new PropertyField(iterateOver);

                targetStack.Peek().Add(field);
                targetStack.Peek().Bind(iterateOver.serializedObject);

                while (iterateOver.NextVisible(true) &&
                       !SerializedProperty.EqualContents(iterateOver, skipTo))
                {
                }
            }
        }
    }
}