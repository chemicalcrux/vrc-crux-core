using System;
using Crux.Core.Runtime;
using Crux.Core.Runtime.Attributes;
using Crux.Core.Runtime.Upgrades;
using JetBrains.Annotations;
using UnityEngine;

namespace Crux.Core.Samples.UpgradableData.Runtime
{
    [UpgradableLatestVersion(version: 3)]
    public abstract class ModelBase : Upgradable<ModelBase>
    {
    }

    [PublicAPI]
    [UpgradableVersion(version: 1)]
    [TooltipRef(assetRef: "8a72288a46f054f73bdb29eef0e2f825,9197481963319205126")]
    [DocRef(manualRef: "237fb495d65834b049da64d12c70ebed,11400000",
        pageRef: "a1a15fc308d084456a5a8c5a29613cef,11400000")]
    public class ModelV1 : ModelBase
    {
        [DocRef(manualRef: "237fb495d65834b049da64d12c70ebed,11400000",
            pageRef: "a1a15fc308d084456a5a8c5a29613cef,11400000")]
        [TooltipRef(assetRef: "84f4b25d9a92744a1bec12e2fa3c4253,9197481963319205126")]
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
        [Serializable]
        public class Thing
        {
            public bool enable;

            [BeginRevealArea(nameof(enable), true)]
            public int amount;
        }
        public enum MyMode
        {
            No,
            Yes
        }
        
        [DocRef(manualRef: "237fb495d65834b049da64d12c70ebed,11400000",
            pageRef: "a1a15fc308d084456a5a8c5a29613cef,11400000")]
        [TooltipRef(assetRef: "84f4b25d9a92744a1bec12e2fa3c4253,9197481963319205126")]
        public float foo;
        [DocRef(manualRef: "237fb495d65834b049da64d12c70ebed,11400000",
            pageRef: "a1a15fc308d084456a5a8c5a29613cef,11400000")]
        public float bar;
        [TooltipRef(assetRef: "84f4b25d9a92744a1bec12e2fa3c4253,9197481963319205126")]
        public string properName;
        [TooltipRef(assetRef: "84f4b25d9a92744a1bec12e2fa3c4253,9197481963319205126")]
        [DocRef(manualRef: "237fb495d65834b049da64d12c70ebed,11400000",
            pageRef: "a1a15fc308d084456a5a8c5a29613cef,11400000")]
        public bool doStuff;
        [BeginRevealArea(nameof(doStuff), true)]
        public int widgets;

        public MyMode theMode;

        [BeginEnumRevealArea(nameof(theMode), typeof(MyMode),
            BeginEnumRevealAreaAttribute.EnumFlagKind.Off, MyMode.Yes)]
        public DecoratedList<int> numbers;
        public string onlyIfYes;

        public Thing thing;
        [EndRevealArea]
        [EndRevealArea] public float whatever;

        public override ModelBase Upgrade()
        {
            return this;
        }
    }
}