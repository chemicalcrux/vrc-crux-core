using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Crux.Core.Runtime.Attributes
{
    /// <summary>
    /// Like <see cref="TooltipInlineAttribute"/>, but not used by property drawers.
    /// </summary>
    [PublicAPI]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Class | AttributeTargets.Struct)]
    public class TooltipInlineInertAttribute : Attribute
    {
        public TooltipInlineInertAttribute(string text)
        {
            Text = text;
        }

        public string Text { get; }
    }
}