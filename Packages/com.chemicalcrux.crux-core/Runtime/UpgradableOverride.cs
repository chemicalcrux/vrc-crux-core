namespace ChemicalCrux.CruxCore.Runtime
{
    public abstract class UpgradableOverride<T> : Upgradable<UpgradableOverride<T>> where T : Upgradable<T>
    {
        public abstract bool TryOverride(T original, out T result);
    }
}