using System;

namespace ChemicalCrux.CruxCore.Runtime
{
    [Serializable]
    public class OverrideItem<T>
    {
        public bool active;
        public T value;

        public T Merge(T incoming) => active ? value : incoming;
    }
}