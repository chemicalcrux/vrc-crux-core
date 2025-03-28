using System;
using JetBrains.Annotations;

namespace ChemicalCrux.CruxCore.Runtime.Upgrades
{
    /// <summary>
    /// This base type is used where we don't care about exactly what is being upgraded. It does not require
    /// a generic type parameter.
    /// </summary>
    [PublicAPI]
    public abstract class UpgradableBase
    {
        /// <summary>
        /// Reports the highest known version for a specific type.
        /// This is read from the <see cref="UpgradableLatestVersionAttribute"/>,
        /// which must be attached.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static int GetLatestVersion(Type type)
        {
            var attributes = type.GetCustomAttributes(typeof(UpgradableLatestVersionAttribute), true);

            if (attributes.Length == 0)
                return -1;

            var attr = attributes[0] as UpgradableLatestVersionAttribute;

            return attr!.version;
        }

        /// <summary>
        /// Reports the version for the given type. This is read from <see cref="UpgradableVersionAttribute"/>,
        /// which must be attached.
        /// </summary>
        /// <param name="type">The type to query the version of</param>
        /// <returns>A version number. -1 if no version is found.</returns>
        public static int GetVersion(Type type)
        {
            foreach (UpgradableVersionAttribute attribute in type.GetCustomAttributes(
                         typeof(UpgradableVersionAttribute), true))
            {
                return attribute.Version;
            }

            return -1;
        }

        /// <summary>
        /// Reports the version number for this object.
        /// </summary>
        /// <returns>A non-negative version number, or -1 if no version can be found.</returns>
        public int GetVersion()
        {
            return GetVersion(GetType());
        }

        /// <summary>
        /// Reports the highest version number this object could be upgraded to. 
        /// </summary>
        /// <returns>A non-negative version number, or -1 if no version can be found.</returns>
        public int GetLatestVersion()
        {
            return GetLatestVersion(GetType());
        }

        /// <summary>
        /// Tries to upgrade this object to a specific version number.
        /// </summary>
        /// <param name="target">The desired version number</param>
        /// <param name="result">The upgraded object, if the upgrade succeeded</param>
        /// <returns>
        /// <see langword="true"/> if the upgrade succeeded (including no change).<br/>
        /// <see langword="false"/> if the upgrade failed because either:
        /// <list type="bullet">
        /// <item>The current version of this object is higher than the target version</item>
        /// <item>The current version of this object is higher than the highest known version (which should not be possible)</item> 
        /// <item>The target version is too high</item>
        /// </list>
        /// </returns>
        public bool TryUpgradeToVersion(int target, out UpgradableBase result)
        {
            int current = GetVersion();
            int limit = GetLatestVersion();

            if (current > target)
            {
                result = this;
                return false;
            }

            if (current > limit)
            {
                result = this;
                return false;
            }

            if (target > limit)
            {
                result = this;
                return false;
            }

            var upgraded = this;

            while (upgraded.GetVersion() < target)
                upgraded = upgraded.UpgradeWithoutType();

            result = upgraded;
            return true;
        }

        /// <summary>
        /// This is implemented in <see cref="Upgradable{T}"/>. This performs an upgrade
        /// without needing to know the exact type being upgraded.
        /// </summary>
        /// <returns>See <see cref="Upgradable{T}.Upgrade"/> for usage.</returns>
        public abstract UpgradableBase UpgradeWithoutType();
    }
}