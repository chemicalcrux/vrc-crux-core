using System;
using ChemicalCrux.CruxCore.Runtime;

namespace ChemicalCrux.CruxCoreTest.Runtime.Upgrading
{
    [Serializable]
    [UpgradableLatestVersion(version: 2)]
    public abstract class ModelOverrideBase : UpgradableOverride<ModelBase>
    {
        
    }

    [Serializable]
    [UpgradableVersion(version = 1)]
    public class ModelOverrideV1 : ModelOverrideBase
    {
        public OverrideItem<int> foo;
        public OverrideItem<string> badName;
        
        public override UpgradableOverride<ModelBase> Upgrade()
        {
            return this;
        }

        public override bool TryOverride(ModelBase original, out ModelBase result)
        {
            if (original is not ModelV1 model)
            {
                result = original;
                return false;
            }

            result = new ModelV1()
            {
                foo = foo.Merge(model.foo),
                badName = badName.Merge(model.badName)
            };
            
            return true;
        }
    }

    [Serializable]
    [UpgradableVersion(version = 2)]
    public class ModelOverrideV2 : ModelOverrideBase
    {
        public OverrideItem<float> foo;
        public OverrideItem<string> properName;
        public OverrideItem<int> widgets;
        
        public override UpgradableOverride<ModelBase> Upgrade()
        {
            return this;
        }

        public override bool TryOverride(ModelBase original, out ModelBase result)
        {
            if (!original.TryUpgradeTo(out ModelV2 model))
            {
                result = original;
                return false;
            }

            result = new ModelV2
            {
                foo = foo.Merge(model.foo),
                properName = properName.Merge(model.properName),
                widgets = widgets.Merge(model.widgets)
            };
            return true;
        }
    }
}