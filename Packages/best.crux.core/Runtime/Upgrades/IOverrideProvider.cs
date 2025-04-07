namespace Crux.Core.Runtime.Upgrades
{
    /// <summary>
    /// Describes a type that can override values for a specific
    /// upgradable type.
    /// </summary>
    /// <typeparam name="T">The type being overridden</typeparam>
    public interface IOverrideProvider<T> where T : Upgradable<T>
    {
        public bool TryOverride(T incoming, out T outgoing);
    }
}