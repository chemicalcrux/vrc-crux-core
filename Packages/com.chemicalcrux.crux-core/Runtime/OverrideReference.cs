using System;
using ChemicalCrux.CruxCore.Runtime.Interfaces;

namespace ChemicalCrux.CruxCore.Runtime
{
    [Serializable]
    public class OverrideReference<TReferenced,TUpgradable> where TReferenced : UnityEngine.Object, IOverrideProvider<TUpgradable> where TUpgradable : Upgradable<TUpgradable>
    {
        public TReferenced reference;

        public bool TryOverride(TUpgradable incoming, out TUpgradable outgoing)
        {
            if (reference != null)
                return reference.TryOverride(incoming, out outgoing);

            outgoing = incoming;
            return true;
        }
    }
}