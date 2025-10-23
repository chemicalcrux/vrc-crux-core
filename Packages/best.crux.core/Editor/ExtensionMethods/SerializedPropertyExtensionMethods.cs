using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Crux.Core.Runtime.Diagnostics;
using UnityEditor;

namespace Crux.Core.Editor.ExtensionMethods
{
    public static class SerializedPropertyExtensionMethods
    {
        private const BindingFlags AllBindingFlags = (BindingFlags)(-1);

        // adapted from here:
        // https://gist.github.com/starikcetin/583a3b86c22efae35b5a86e9ae23f2f0

        public static bool TryGetAttribute<TAttribute>(this SerializedProperty serializedProperty, out TAttribute attribute, bool inherit = false) where TAttribute : Attribute
        {
            if (serializedProperty.TryGetMemberInfo(out MemberInfo info))
            {
                attribute = info.GetCustomAttribute<TAttribute>();
                return attribute != null;
            }

            attribute = default;
            return false;
        }
        
        /// <summary>
        /// Returns attributes of type <typeparamref name="TAttribute"/> on <paramref name="serializedProperty"/>.
        /// </summary>
        public static TAttribute[] GetAttributes<TAttribute>(this SerializedProperty serializedProperty, bool inherit = false)
            where TAttribute : Attribute
        {
            if (serializedProperty.TryGetMemberInfo(out MemberInfo info))
                return (TAttribute[]) info.GetCustomAttributes(typeof(TAttribute), inherit);
            return Array.Empty<TAttribute>();
        }

        public static bool TryGetMemberInfo(this SerializedProperty serializedProperty, out MemberInfo memberInfo)
        {
            if (serializedProperty == null)
            {
                CoreLog.LogError("SerializedProperty was null.");
                
                memberInfo = default;
                return false;
            }

            object targetObject = serializedProperty.serializedObject.targetObject;
            var targetObjectType = serializedProperty.serializedObject.targetObject.GetType();

            List<string> segments = serializedProperty.propertyPath.Split('.').ToList();

            // we need to dig *through* the property tree until we get to the right place
            foreach (var pathSegment in segments.SkipLast(1))
            {
                var fieldInfo = targetObjectType.GetField(pathSegment, AllBindingFlags);

                if (fieldInfo != null)
                {
                    targetObject = fieldInfo.GetValue(targetObject);
                    targetObjectType = targetObject.GetType();
                    continue;
                }

                var propertyInfo = targetObjectType.GetProperty(pathSegment, AllBindingFlags);

                if (propertyInfo != null)
                {
                    targetObject = propertyInfo.GetValue(targetObject);
                    targetObjectType = targetObject.GetType();
                    continue;
                }

                CoreLog.LogError("Couldn't find a field or property for this path segment: " + pathSegment);
                
                memberInfo = default;
                return false;
            }

            {
                var fieldInfo = targetObjectType.GetField(segments[^1], AllBindingFlags);

                if (fieldInfo != null)
                {
                    memberInfo = fieldInfo;
                    return true;
                }

                var propertyInfo = targetObjectType.GetProperty(segments[^1], AllBindingFlags);

                if (propertyInfo != null)
                {
                    memberInfo = propertyInfo;
                    return true;
                }
            }

            memberInfo = default;
            return false;
        }
    }
}