using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Crux.Core.Runtime.Attributes
{
    [PublicAPI]
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
}