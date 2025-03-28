using ChemicalCrux.CruxCore.Runtime;
using ChemicalCrux.CruxCore.Runtime.Upgrades;
using JetBrains.Annotations;

namespace ChemicalCrux.CruxCoreTest.Runtime.Upgrading
{
    [UpgradableLatestVersion(version: 3)]
    public abstract class ModelBase : Upgradable<ModelBase>
    {
        
    }

    [PublicAPI]
    [UpgradableVersion(version: 1)]
    public class ModelV1 : ModelBase
    {
        public int foo;
        public string badName;
        
        public override ModelBase Upgrade()
        {
            return new ModelV2()
            {
                foo = foo,
                widgets = 100,
                properName = badName
            };
        }
    }

    [PublicAPI]
    [UpgradableVersion(version: 2)]
    public class ModelV2 : ModelBase
    {
        public float foo;
        public string properName;
        public int widgets;
        
        public override ModelBase Upgrade()
        {
            return new ModelV3()
            {
                foo = foo,
                bar = foo,
                widgets = widgets,
                properName = properName,
                doStuff = true
            };
        }
    }

    [PublicAPI]
    [UpgradableVersion(version: 3)]
    public class ModelV3 : ModelBase
    {
        public float foo;
        public float bar;
        public string properName;
        public bool doStuff;
        public int widgets;
        
        public override ModelBase Upgrade()
        {
            return this;
        }
    }
}