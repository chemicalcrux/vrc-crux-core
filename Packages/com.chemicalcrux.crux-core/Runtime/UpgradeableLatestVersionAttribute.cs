using System;

namespace ChemicalCrux.CruxCore.Runtime
{
    public class UpgradeableLatestVersionAttribute : Attribute
    {
        public int version;

        public UpgradeableLatestVersionAttribute(int version)
        {
            this.version = version;
        }
    }
}