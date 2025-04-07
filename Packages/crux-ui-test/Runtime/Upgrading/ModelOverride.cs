using System;
using Crux.Core.Runtime.Upgrades;

namespace ChemicalCrux.CruxCoreTest.Runtime.Upgrading
{
    [Serializable]
    [UpgradableLatestVersion(version: 3)]
    public abstract class ModelOverrideBase : UpgradableOverride<ModelBase>
    {
        
    }

    [Serializable]
    [UpgradableVersion(version: 1)]
    public class ModelOverrideV1 : ModelOverrideBase
    {
        public OverrideItem<int> foo;
        public OverrideItem<string> badName;
        
        public override UpgradableOverride<ModelBase> Upgrade()
        {
            return new ModelOverrideV2
            {
                foo = new OverrideItem<float>
                {
                    active = foo.active,
                    value = foo.value
                },
                properName = badName,
                widgets = new OverrideItem<int>()
            };
        }

        protected override bool TryPerformOverride(ModelBase original, out ModelBase result)
        {
            if (!original.TryUpgradeTo(out ModelV1 upgraded))
            {
                result = original;
                return false;
            }

            result = new ModelV1()
            {
                foo = foo.Merge(upgraded.foo),
                badName = badName.Merge(upgraded.badName)
            };
            
            return true;
        }
    }

    [Serializable]
    [UpgradableVersion(version: 2)]
    public class ModelOverrideV2 : ModelOverrideBase
    {
        public OverrideItem<float> foo;
        public OverrideItem<string> properName;
        public OverrideItem<int> widgets;
        
        public override UpgradableOverride<ModelBase> Upgrade()
        {
            return new ModelOverrideV3
            {
                foo = foo,
                bar = foo,
                properName = properName,
                doStuff = new OverrideItem<bool>(),
                widgets = widgets
            };
        }

        protected override bool TryPerformOverride(ModelBase original, out ModelBase result)
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

    [Serializable]
    [UpgradableVersion(version: 3)]
    public class ModelOverrideV3 : ModelOverrideBase
    {
        public OverrideItem<float> foo;
        public OverrideItem<float> bar;
        public OverrideItem<string> properName;
        public OverrideItem<bool> doStuff;
        public OverrideItem<int> widgets;
        
        public override UpgradableOverride<ModelBase> Upgrade()
        {
            return this;
        }

        protected override bool TryPerformOverride(ModelBase original, out ModelBase result)
        {
            if (!original.TryUpgradeTo(out ModelV3 model))
            {
                result = original;
                return false;
            }

            result = new ModelV3
            {
                foo = foo.Merge(model.foo),
                bar = bar.Merge(model.bar),
                properName = properName.Merge(model.properName),
                doStuff = doStuff.Merge(model.doStuff),
                widgets = widgets.Merge(model.widgets)
            };
            
            return true;
        }
    }
}