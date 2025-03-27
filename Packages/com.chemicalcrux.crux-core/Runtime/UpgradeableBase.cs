using System;
using UnityEngine;

namespace ChemicalCrux.CruxCore.Runtime
{
    public abstract class UpgradeableBase
    {
        /// <summary>
        /// Given an Upgradeable&lt;T&gt; or any subclass thereof, this method
        /// returns the highest version number that exists.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static int GetLatestVersion(Type type)
        {
            // This walks up until we hit Upgradeable<>
            Type root = typeof(Upgradeable<>);

            while (type != root && type != null)
            {
                type = type.BaseType;

                if (type == null)
                    break;

                if (type.IsGenericType)
                {
                    Type constructedFrom = type.GetGenericTypeDefinition();

                    if (constructedFrom == root)
                        break;
                    else
                        type = constructedFrom;
                }
            }

            if (type == null)
                return -1;

            Type model = type.GetGenericArguments()[0];

            var attributes = model.GetCustomAttributes(typeof(UpgradeableLatestVersionAttribute), false);

            if (attributes.Length == 0)
                return -1;

            var attr = attributes[0] as UpgradeableLatestVersionAttribute;
            
            return attr.version;
        }
        
        public static int GetVersion(Type type)
        {
            foreach (UpgradeableVersionAttribute attribute in type.GetCustomAttributes(typeof(UpgradeableVersionAttribute), true))
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

        public bool TryUpgradeToVersion(int target, out UpgradeableBase result)
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
        
        public abstract UpgradeableBase UpgradeWithoutType();
    }
}