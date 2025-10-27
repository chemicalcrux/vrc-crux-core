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
        
        public BeginEnumRevealAreaAttribute(string property, Type enumType, EnumFlagKind flagsUsage, params object[] enumValues)
        {
            Property = property;
            EnumType = enumType;
            EnumValues = enumValues;
            FlagsUsage = flagsUsage;
        }

        public string Property { get; }
        public Type EnumType { get; }
        public object[] EnumValues { get; }
        public EnumFlagKind FlagsUsage { get; }
        public string Key { get; set;  }
    }
}