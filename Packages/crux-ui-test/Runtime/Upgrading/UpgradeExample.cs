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
    }
}