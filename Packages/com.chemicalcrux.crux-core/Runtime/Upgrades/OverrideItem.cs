using System;

namespace ChemicalCrux.CruxCore.Runtime.Upgrades
{
    /// <summary>
    /// This works like Unity's volume profiles – if the active field is set, then the value
    /// will be used. If not, the original value is kept.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class OverrideItem<T>
    {
        public bool active;
        public T value;

        public T Merge(T incoming) => active ? value : incoming;
    }
}