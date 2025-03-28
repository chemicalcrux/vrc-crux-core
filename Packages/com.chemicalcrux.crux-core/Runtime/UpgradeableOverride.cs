namespace ChemicalCrux.CruxCore.Runtime
{
    public abstract class UpgradeableOverride<T> : Upgradeable<UpgradeableOverride<T>> where T : Upgradeable<T>
    {
        public abstract bool TryOverride(T original, out T result);
    }
}