using System.Collections.Generic;
using System.Linq;
using Crux.Core.Editor.Controls;
using Crux.Core.Editor.ExtensionMethods;
using Crux.Core.Runtime.Attributes;
using Crux.Core.Runtime.Diagnostics;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Crux.Core.Editor.PropertyDrawers
{
    [PublicAPI]
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
        /// <param name="children"></param>
        public static void CreatePropertyFields(SerializedProperty property, VisualElement target, bool children = true)
        {
            SerializedProperty end = null;
            var iterateOver = property.Copy();

            if (children)
            {
                end = iterateOver.GetEndProperty(true);
                var foldout = new Foldout()
                {
                    text = property.displayName
                };
                
                target.Add(foldout);
                target = foldout;
                iterateOver.NextVisible(true);
            }
            else
            {
                iterateOver.NextVisible(true);
            }

            Stack<VisualElement> targetStack = new();
            targetStack.Push(target);

            while (!SerializedProperty.EqualContents(iterateOver, end))
            {
                foreach (var _ in iterateOver.GetAttributes<EndRevealAreaAttribute>())
                {
                    if (targetStack.Count > 1)
                        targetStack.Pop();
                    else
                        CoreLog.LogError("Tried to close a RevealArea that did not exist.");
                }

                foreach (var attribute in iterateOver.GetAttributes<BeginRevealAreaAttribute>())
                {
                    string sourcePath = string.Join(".", iterateOver.propertyPath.Split(".").SkipLast(1).Append(attribute.Property));
                    RevealArea area = new RevealArea(sourcePath, attribute.Condition);
                    
                    var sourceProperty = iterateOver.serializedObject.FindProperty(sourcePath);

                    area.UpdateDelegate(sourceProperty.boolValue);
                    targetStack.Peek().Add(area);
                    targetStack.Push(area);
                }

                foreach (var attribute in iterateOver.GetAttributes<BeginEnumRevealAreaAttribute>())
                {
                    string sourcePath = string.Join(".", iterateOver.propertyPath.Split(".").SkipLast(1).Append(attribute.Property));
                    EnumRevealArea area = new EnumRevealArea(sourcePath, attribute.EnumType, attribute.FlagsUsage, attribute.EnumValues);

                    var sourceProperty = iterateOver.serializedObject.FindProperty(sourcePath);
                    
                    area.UpdateDelegate(sourceProperty.intValue);
                    
                    targetStack.Peek().Add(area);
                    targetStack.Push(area);
                }

                var field = new PropertyField(iterateOver);

                field.SetEnabled(iterateOver.propertyPath != "m_Script");

                targetStack.Peek().Add(field);
                targetStack.Peek().Bind(iterateOver.serializedObject);

                bool hitEnd = !iterateOver.NextVisible(false);

                if (hitEnd)
                    break;
            }
        }
    }
}