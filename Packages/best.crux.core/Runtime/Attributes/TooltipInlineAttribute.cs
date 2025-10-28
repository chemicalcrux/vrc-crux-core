using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Crux.Core.Runtime.Attributes
{
    [PublicAPI]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Class | AttributeTargets.Struct)]
    public class TooltipInlineAttribute : PropertyAttribute
    {
        public TooltipInlineAttribute(string text)
        {
            Text = text;
        }

        public string Text { get; }
    }
}