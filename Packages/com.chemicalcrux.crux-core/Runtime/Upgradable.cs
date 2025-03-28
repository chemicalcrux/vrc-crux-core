using System;

namespace ChemicalCrux.CruxCore.Runtime
{
    public abstract class Upgradable<T> : UpgradableBase where T : Upgradable<T>
    {
        public bool TryUpgradeTo<U>(out U upgraded) where U : T
        {
            int fromVersion = GetVersion(GetType());
            int toVersion = GetVersion(typeof(U));

            if (fromVersion > toVersion)
            {
                upgraded = default;
                return false;
            }

            var current = this as T;

            while (current is not U)
                current = current!.Upgrade();

            upgraded = current as U;
            return true;
        }

        public abstract T Upgrade();

        public override UpgradableBase UpgradeWithoutType()
        {
            return Upgrade();
        }

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