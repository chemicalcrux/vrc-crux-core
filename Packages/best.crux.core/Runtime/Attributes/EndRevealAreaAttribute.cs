using System;
using JetBrains.Annotations;

namespace Crux.Core.Runtime.Attributes
{
    [PublicAPI]
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class EndRevealAreaAttribute : Attribute
    {
        
    }
}