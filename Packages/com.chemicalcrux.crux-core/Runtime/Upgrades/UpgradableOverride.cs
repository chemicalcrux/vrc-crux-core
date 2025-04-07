using UnityEngine;

namespace Crux.Core.Runtime.Upgrades
{
    /// <summary>
    /// A specific kind of Upgradable that can override another Upgradable. Overrides should produce
    /// a new object, possibly with different values than the original.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class UpgradableOverride<T> : Upgradable<UpgradableOverride<T>> where T : Upgradable<T>
    {
        public bool TryOverride(T original, out T result)
        {
            int overriderVersion = GetVersion();
            int overrideeVersion = original.GetVersion();

            int target = Mathf.Max(overrideeVersion, overriderVersion);

            if (!TryUpgradeToVersion(target, out var overrider))
            {
                Debug.LogWarning($"Failed to upgrade the overrider to version {target}");
                result = default;
                return false;
            }

            if (!original.TryUpgradeToVersion(target, out var overridee))
            {
                Debug.LogWarning($"Failed to upgrade the overridee to version {target}");
                result = default;
                return false;
            }

            if (!overrider.TryPerformOverride(overridee, out result))
            {
                Debug.LogWarning($"Failed to perform the override.");
                return false;
            }

            return true;
        }
        
        protected abstract bool TryPerformOverride(T original, out T result);
    }
}