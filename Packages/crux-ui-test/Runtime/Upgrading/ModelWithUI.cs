using System;
using Crux.Core.Runtime.Upgrades;

namespace ChemicalCrux.CruxCoreTest.Runtime.Upgrading
{
    [Serializable]
    [UpgradableLatestVersion(1)]
    public abstract class ModelWithUI : Upgradable<ModelWithUI>
    {
        
    }

    [UpgradableVersion(1)]
    [UpgradablePropertyDrawer("ba0d744024e4c41afa5c1cdc2a2a0232,9197481963319205126")]
    public class ModelWithUIV1 : ModelWithUI
    {
        public override ModelWithUI Upgrade()
        {
            throw new NotImplementedException();
        }
    }
}