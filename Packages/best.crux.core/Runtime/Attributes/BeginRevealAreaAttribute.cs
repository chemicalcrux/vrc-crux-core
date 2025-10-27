using System;
using JetBrains.Annotations;

namespace Crux.Core.Runtime.Attributes
{
    [PublicAPI]
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class BeginRevealAreaAttribute : Attribute
    {
        public BeginRevealAreaAttribute(string property, bool condition)
        {
            Property = property;
            Condition = condition;
        }

        public string Property { get; }
        public bool Condition { get; }
        public string Key { get; set; }
    }
}