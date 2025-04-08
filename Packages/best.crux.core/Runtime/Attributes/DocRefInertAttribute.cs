using System;
using JetBrains.Annotations;

namespace Crux.Core.Runtime.Attributes
{
    /// <summary>
    /// Equivalent to a <see cref="DocRefAttribute"/>, but
    /// property drawers don't look for it.
    ///
    /// This is used with <see cref="DecoratedList{T}"/>.
    /// </summary>
    [PublicAPI]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Class | AttributeTargets.Struct)]
    public class DocRefInertAttribute : Attribute
    {
        public DocRefInertAttribute(string manualRef = "", string pageRef = "")
        {
            ManualRef = manualRef;
            PageRef = pageRef;
        }

        public string ManualRef { get; }
        public string PageRef { get; }
    }
}