using System;

namespace ChemicalCrux.CruxCore.Runtime
{
    public class UpgradableVersionAttribute : Attribute
    {
        public int version;

        public UpgradableVersionAttribute()
        {
            
        }
    }
}