namespace ChemicalCrux.CruxCore.Runtime.Interfaces
{
    public interface IOverrideProvider<T> where T : Upgradable<T>
    {
        public bool TryOverride(T incoming, out T outgoing);
    }
}