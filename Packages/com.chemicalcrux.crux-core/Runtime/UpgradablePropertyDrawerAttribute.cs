using System;

namespace ChemicalCrux.CruxCore.Runtime
{
    public class UpgradablePropertyDrawerAttribute : Attribute
    {
        public string path;

        public UpgradablePropertyDrawerAttribute(string path)
        {
            this.path = path;
        }
    }
}