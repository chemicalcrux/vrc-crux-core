using System;

namespace ChemicalCrux.CruxCore.Runtime
{
    public class UpgradeablePropertyDrawerAttribute : Attribute
    {
        public string path;

        public UpgradeablePropertyDrawerAttribute(string path)
        {
            this.path = path;
        }
    }
}