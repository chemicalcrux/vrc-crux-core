using UnityEngine;

namespace ChemicalCrux.CruxCoreTest.Runtime.Upgrading
{
    public class UpgradeExample : MonoBehaviour
    {
        [SerializeReference] public ModelBase model;

        void Reset()
        {
            model = new ModelV1();
        }
    }
}