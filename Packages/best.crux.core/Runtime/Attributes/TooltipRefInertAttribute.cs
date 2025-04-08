using System;
using JetBrains.Annotations;

namespace Crux.Core.Runtime.Attributes
{
    /// <summary>
    /// Equivalent to a <see cref="TooltipRefAttribute"/>, but
    /// property drawers don't look for it.
    ///
    /// This is used with <see cref="DecoratedList{T}"/>.
    /// </summary>
    [PublicAPI]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Class | AttributeTargets.Struct)]
    public class TooltipRefInertAttribute : Attribute
    {
        public TooltipRefInertAttribute(string assetRef)
        {
            AssetRef = assetRef;
        }

        public string AssetRef { get; }
    }
}