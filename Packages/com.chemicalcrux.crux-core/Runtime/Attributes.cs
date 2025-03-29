using System;
using UnityEditor.VersionControl;
using UnityEngine;

namespace ChemicalCrux.CruxCore.Runtime
{
    [AttributeUsage(AttributeTargets.Field)]
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

    [AttributeUsage(AttributeTargets.Field)]
    public class TooltipRefAttribute : PropertyAttribute
    {
        public TooltipRefAttribute(string assetRef)
        {
            AssetRef = assetRef;
        }

        public string AssetRef { get; }
    }
}