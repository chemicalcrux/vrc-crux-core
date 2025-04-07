namespace Crux.Core.Runtime.Upgrades
{
    /// <summary>
    /// An Upgradable type can upgrade itself from version X to version X+1.
    /// </summary>
    /// <typeparam name="T">The type that can be upgraded</typeparam>
    public abstract class Upgradable<T> : UpgradableBase where T : Upgradable<T>
    {
        /// <summary>
        /// Tries to upgrade to a specific type.
        /// </summary>
        /// <param name="upgraded">The upgraded object, if the upgrade succeeded.</param>
        /// <typeparam name="U">The type to upgrade to.</typeparam>
        /// <returns>
        /// <see langword="true" /> if the upgrade succeeded. <br />
        /// <see langword="false" /> if the upgrade failed. See <see cref="UpgradableBase.TryUpgradeToVersion"/> for more details.
        /// </returns>
        public bool TryUpgradeTo<U>(out U upgraded) where U : T
        {
            int toVersion = GetVersion(typeof(U));

            if (TryUpgradeToVersion(toVersion, out var innerUpgraded))
            {
                upgraded = innerUpgraded as U;
                return true;
            }

            upgraded = default;
            return false;
        }

        /// <summary>
        /// Returns either:
        /// <list type="bullet">
        /// <item>Itself, if this is the latest version.</item>
        /// <item>An upgraded object exactly one version greater than this object.</item>
        /// </list>
        /// </summary>
        /// <returns>The resulting object.</returns>
        public abstract T Upgrade();

        /// <summary>
        /// See <see cref="UpgradableBase.UpgradeWithoutType"/>
        /// </summary>
        /// <returns></returns>
        public override UpgradableBase UpgradeWithoutType()
        {
            return Upgrade();
        }

        /// <summary>
        /// Like <see cref="UpgradableBase.TryUpgradeToVersion"/>, but returns a more specific type.
        /// </summary>
        /// <param name="target">The desired version number.</param>
        /// <param name="result">The resulting object, if the upgrade succeeded.</param>
        /// <returns>Whether the upgrade succeeded.</returns>
        public bool TryUpgradeToVersion(int target, out T result)
        {
            if (base.TryUpgradeToVersion(target, out var upgraded))
            {
                result = upgraded as T;
                return true;
            }

            result = upgraded as T;
            return false;
        }
    }
}