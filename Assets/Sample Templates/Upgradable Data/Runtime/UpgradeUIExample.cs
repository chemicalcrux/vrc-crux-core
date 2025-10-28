using UnityEngine;

namespace Crux.Core.Samples.UpgradableData.Runtime
{
    public class UpgradeUIExample : MonoBehaviour
    {
        [SerializeField, SerializeReference] internal ModelWithUI data = new ModelWithUIV1();
    }
}