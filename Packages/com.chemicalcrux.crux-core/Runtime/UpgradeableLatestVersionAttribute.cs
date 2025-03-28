using System;

namespace ChemicalCrux.CruxCore.Runtime
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class UpgradeableLatestVersionAttribute : Attribute
    {
        public int version;

        public UpgradeableLatestVersionAttribute(int version)
        {
            this.version = version;
        }
    }
}