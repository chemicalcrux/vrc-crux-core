using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Crux.Core.Runtime.Attributes
{
    /// <summary>
    /// Requests that a tooltip be provided.
    /// </summary>
    [PublicAPI]
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