using System;
using UnityEngine;

namespace ChemicalCrux.CruxCore.Runtime
{
    public abstract class UpgradableBase
    {
        /// <summary>
        /// Given an Upgradable&lt;T&gt; or any subclass thereof, this method
        /// returns the highest version number that exists.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static int GetLatestVersion(Type type)
        {
            var attributes = type.GetCustomAttributes(typeof(UpgradableLatestVersionAttribute), true);
            
            if (attributes.Length == 0)
                return -1;

            var attr = attributes[0] as UpgradableLatestVersionAttribute;
            
            return attr.version;
        }
        
        public static int GetVersion(Type type)
        {
            foreach (UpgradableVersionAttribute attribute in type.GetCustomAttributes(typeof(UpgradableVersionAttribute), true))
            {
                return attribute.version;
            }

            return -1;
        }

        public int GetVersion()
        {
            return GetVersion(GetType());
        }

        public int GetLatestVersion()
        {
            return GetLatestVersion(GetType());
        }

        public bool TryUpgradeToVersion(int target, out UpgradableBase result)
        {
            int current = GetVersion();
            int limit = GetLatestVersion();

            if (current > limit)
            {
                result = this;
                return false;
            }

            var upgraded = this;

            while (upgraded.GetVersion() < limit)
                upgraded = upgraded.UpgradeWithoutType();

            result = upgraded;
            return true;
        }
        
        public abstract UpgradableBase UpgradeWithoutType();
    }
}