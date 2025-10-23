using System;
using JetBrains.Annotations;

namespace Crux.Core.Runtime.Attributes
{
    [PublicAPI]
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class EndRevealAreaAttribute : Attribute
    {
        
    }
}