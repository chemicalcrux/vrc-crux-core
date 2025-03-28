using System;

namespace ChemicalCrux.CruxCore.Runtime
{
    public class OverridableWithAttribute : Attribute
    {
        private Type type;

        public OverridableWithAttribute(Type type)
        {
            this.type = type;
        }

        public Type Type => type;
    }
}