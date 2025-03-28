using ChemicalCrux.CruxCore.Runtime;
using ChemicalCrux.CruxCore.Runtime.Interfaces;
using UnityEngine;

namespace ChemicalCrux.CruxCoreTest.Runtime.Upgrading
{
    [AddComponentMenu(menuName: "chemicalcrux/Crux Core Demos/Upgrade/Override Example")]
    public class UpgradeOverrideExample : MonoBehaviour, IOverrideProvider<ModelBase>
    {
        [SerializeReference] public ModelOverrideBase overrideModel;
        public OverrideReference<UpgradeOverrideExample, ModelBase> overrideComponent;

        public bool TryOverride(ModelBase incoming, out ModelBase outgoing)
        {
            if (!overrideModel.TryOverride(incoming, out var result))
            {
                Debug.LogWarning("Failed to apply my own override.");
                outgoing = default;
                return false;
            }

            if (!overrideComponent.TryOverride(result, out ModelBase overridden))
            {
                Debug.LogWarning("Failed to apply the next override.");
                outgoing = default;
                return false;
            }

            outgoing = overridden;
            return true;
        }

        void Reset()
        {
            overrideModel = new ModelOverrideV1();
        }
    }
}