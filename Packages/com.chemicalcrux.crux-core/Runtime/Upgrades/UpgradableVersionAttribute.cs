using System;

namespace ChemicalCrux.CruxCore.Runtime.Upgrades
{
    /// <summary>
    /// Indicates which version a specific Upgradable type is.
    /// </summary>
    public class UpgradableVersionAttribute : Attribute
    {
        public UpgradableVersionAttribute(int version)
        {
            Version = version;
        }

        public int Version { get; }
    }
}