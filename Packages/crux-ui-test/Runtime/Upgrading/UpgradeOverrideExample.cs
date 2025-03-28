using ChemicalCrux.CruxCore.Runtime;
using UnityEngine;

namespace ChemicalCrux.CruxCoreTest.Runtime.Upgrading
{
    [AddComponentMenu(menuName: "chemicalcrux/Crux Core Demos/Upgrade/Override Example")]
    public class UpgradeOverrideExample : MonoBehaviour
    {
        [SerializeReference] public ModelOverrideBase overrideModel;
        public OverrideReference<UpgradeOverrideExample> overrideComponent;

        void Reset()
        {
            overrideModel = new ModelOverrideV1();
        }
    }
}