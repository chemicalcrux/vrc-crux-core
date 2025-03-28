using ChemicalCrux.CruxCore.Runtime;
using UnityEngine;

namespace ChemicalCrux.CruxCoreTest.Runtime.Upgrading
{
    [AddComponentMenu(menuName: "chemicalcrux/Crux Core Demos/Upgrade/Example")]
    public class UpgradeExample : MonoBehaviour
    {
        [SerializeReference] public ModelBase model;
        public OverrideReference<UpgradeOverrideExample> overrideComponent;

        void Reset()
        {
            model = new ModelV1();
        }

        private bool TryGetModel(out ModelV3 result)
        {
            ModelBase overridden = model;
            
            if (overrideComponent.reference)
            {
                if (!overrideComponent.reference.TryOverride(model, out overridden))
                {
                    Debug.LogWarning("Failed to override.");
                    result = default;
                    return false;
                }
            }
            
            if (!overridden.TryUpgradeTo(out ModelV3 upgraded))
            {
                Debug.LogWarning("Failed to upgrade.");
                result = default;
                return false;
            }

            result = upgraded;
            return true;
        }

        [ContextMenu("Get Values")]
        void GetValues()
        {
            if (!TryGetModel(out var upgraded))
                return;
            
            Debug.Log("Foo: " + upgraded.foo);
            Debug.Log("Bar: " + upgraded.bar);
            Debug.Log("Widgets: " + upgraded.widgets);
            Debug.Log("Do Stuff: " + upgraded.doStuff);
            Debug.Log("Proper Name: " + upgraded.properName);
        }
    }
}