using System;

namespace Crux.Core.Runtime.Upgrades
{
    /// <summary>
    /// Mandatory attribute. Indicates which version number is the most recent one.
    ///
    /// This could be calculated automatically, but it would require scanning all assemblies every time
    /// the maximum version is needed.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class UpgradableLatestVersionAttribute : Attribute
    {
        public int version;

        public UpgradableLatestVersionAttribute(int version)
        {
            this.version = version;
        }
    }
}