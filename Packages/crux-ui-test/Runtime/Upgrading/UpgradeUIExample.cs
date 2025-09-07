using UnityEngine;

namespace ChemicalCrux.CruxCoreTest.Runtime.Upgrading
{
    public class UpgradeUIExample : MonoBehaviour
    {
        [SerializeField, SerializeReference] private ModelWithUI data = new ModelWithUIV1();
    }
}