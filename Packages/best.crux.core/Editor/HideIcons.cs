using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Crux.Core.Runtime.Attributes;
using UnityEditor;
using UnityEngine;

namespace Crux.Core.Editor
{
    /// <summary>
    /// Unity insists on showing icons in the scene view for any component with a custom icon,
    /// which often just causes visual clutter. There is no way to signal to Unity that the
    /// icon should default to being disabled!
    ///
    /// The solution: look for types with [HideIcon] and manually disable their icons.
    ///
    /// This is a bit harder than it sounds, because Unity won't let you enable or disable icons
    /// if it isn't already keeping track of the icon/gizmo state of a specific type.
    /// </summary>
    public static class HideIcons
    {
        [InitializeOnLoadMethod]
        static void FixIcons()
        {
#if CRUX_DEV
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
#endif

            List<Type> targets = new();

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies().Where(asm => !asm.IsDynamic))
            {
                // we can ignore any assemblies that couldn't use the attribute!
                if (assembly.GetReferencedAssemblies().All(asm => asm.Name != "Crux.Core.Runtime") && assembly.GetName().Name != "Crux.Core.Runtime")
                    continue;

                foreach (var type in assembly.ExportedTypes)
                {
                    if (type.CustomAttributes.Any(attribute => attribute.AttributeType == typeof(HideIconAttribute)))
                    {
                        // until you open the AnnotationWindow (the dropdown that lets you
                        // toggle gizmos and icons), no gizmo info exists at all. we can use
                        // this to test if a type is brand-new.
                        if (!GizmoUtility.TryGetGizmoInfo(type, out var _))
                        {
                            targets.Add(type);
                        }
                    }
                }
            }

#if CRUX_DEV
            stopwatch.Stop();
            Debug.Log($"Time to scan for targets: {stopwatch.Elapsed.Milliseconds}ms");
#endif

            if (targets.Count == 0)
                return;

#if CRUX_DEV
            stopwatch = System.Diagnostics.Stopwatch.StartNew();

#endif
            RefreshAnnotations();

            foreach (var target in targets)
            {
                GizmoUtility.SetIconEnabled(target, false);
            }

#if CRUX_DEV
            stopwatch.Stop();
            Debug.Log($"Time to fix icons: {stopwatch.Elapsed.Milliseconds}ms");
#endif
        }

        static void RefreshAnnotations()
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies().Where(asm => !asm.IsDynamic))
            {
                var annotationUtilityType = assembly.GetType("UnityEditor.AnnotationUtility");

                if (annotationUtilityType != null)
                {
                    Debug.Log(annotationUtilityType);

                    var getMethod = annotationUtilityType.GetMethod("GetAnnotations",
                        BindingFlags.NonPublic | BindingFlags.Static);

                    var setMethod = annotationUtilityType.GetMethod("SetIconEnabled",
                        BindingFlags.NonPublic | BindingFlags.Static);

                    if (setMethod == null || getMethod == null)
                    {
                        Debug.LogError("Couldn't find the methods needed to fix icons. Giving up.");
                        return;
                    }

                    // this makes sure that annotation data exists for any type with a custom icon!
                    getMethod.Invoke(null, new object[] { });

                    break;
                }
            }
        }
    }
}