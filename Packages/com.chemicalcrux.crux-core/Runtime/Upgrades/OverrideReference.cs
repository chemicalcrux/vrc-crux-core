using System;

namespace Crux.Core.Runtime.Upgrades
{
    /// <summary>
    /// Points to a Component or ScriptableObject that can perform an override.
    /// </summary>
    /// <typeparam name="TReferenced">The kind of object to reference</typeparam>
    /// <typeparam name="TUpgradable">The kind of object that can be upgraded</typeparam>
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