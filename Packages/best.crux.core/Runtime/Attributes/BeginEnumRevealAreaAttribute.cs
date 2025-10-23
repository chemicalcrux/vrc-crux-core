using System;
using JetBrains.Annotations;

namespace Crux.Core.Runtime.Attributes
{
    [PublicAPI]
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class BeginEnumRevealAreaAttribute : Attribute
    {
        public enum EnumFlagKind
        {
            Off = 0,
            Any = 1,
            All = 2,
            None = 3,
            NotAll = 4
        }
        
        public BeginEnumRevealAreaAttribute(string property, Type enumType, object enumValue, EnumFlagKind flagsUsage)
        {
            Property = property;
            EnumType = enumType;
            EnumValue = (int) enumValue;
            FlagsUsage = flagsUsage;
        }

        public string Property { get; }
        public Type EnumType { get; }
        public int EnumValue { get; }
        public EnumFlagKind FlagsUsage { get; }
    }
}