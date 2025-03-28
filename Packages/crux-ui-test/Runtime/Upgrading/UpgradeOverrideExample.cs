using ChemicalCrux.CruxCore.Runtime;
using UnityEngine;

namespace ChemicalCrux.CruxCoreTest.Runtime.Upgrading
{
    [AddComponentMenu(menuName: "chemicalcrux/Crux Core Demos/Upgrade/Override Example")]
    public class UpgradeOverrideExample : MonoBehaviour
    {
        [SerializeReference] public ModelOverrideBase overrideModel;
        public OverrideReference<UpgradeOverrideExample> overrideComponent;

        public bool TryOverride(ModelBase incoming, out ModelBase outgoing)
        {
            if (!overrideModel.TryOverride(incoming, out var result))
            {
                outgoing = default;
                return false;
            }

            ModelBase overridden = result;

            if (overrideComponent.reference != null)
            {
                if (!overrideComponent.reference.TryOverride(result, out overridden))
                {
                    outgoing = default;
                    return false;
                }
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