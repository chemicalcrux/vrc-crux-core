using System;

namespace ChemicalCrux.CruxCore.Runtime
{
    public class UpgradeableVersionAttribute : Attribute
    {
        public int version;

        public UpgradeableVersionAttribute()
        {
            
        }
    }
}