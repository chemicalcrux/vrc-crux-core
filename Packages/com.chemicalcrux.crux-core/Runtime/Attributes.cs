using System;
using UnityEngine;

namespace Crux.Core.Runtime
{
    /// <summary>
    /// Requests a link to a manual (or a page in a manual).
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Class | AttributeTargets.Struct)]
    public class DocRefAttribute : PropertyAttribute
    {
        public DocRefAttribute(string manualRef = "", string pageRef = "")
        {
            ManualRef = manualRef;
            PageRef = pageRef;
        }

        public string ManualRef { get; }
        public string PageRef { get; }
    }

    /// <summary>
    /// Requests that a tooltip be provided.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Class | AttributeTargets.Struct)]
    public class TooltipRefAttribute : PropertyAttribute
    {
        public TooltipRefAttribute(string assetRef)
        {
            AssetRef = assetRef;
        }

        public string AssetRef { get; }
    }
}