using System;

namespace Crux.Core.Runtime.Upgrades
{
    /// <summary>
    /// Tells the Upgradable property drawer where to find the UXML document.
    /// </summary>
    public class UpgradablePropertyDrawerAttribute : Attribute
    {
        public UpgradablePropertyDrawerAttribute(string path)
        {
            Path = path;
        }

        public string Path { get; }
    }
}