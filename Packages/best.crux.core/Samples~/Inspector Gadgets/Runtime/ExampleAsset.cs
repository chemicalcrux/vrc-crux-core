using System;
using Crux.Core.Runtime;
using Crux.Core.Runtime.Attributes;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Crux.Core.Samples.InspectorGadgets.Runtime
{
    [CreateAssetMenu]
    public class ExampleAsset : ScriptableObject
    {
        public int foo;
        public bool bar;
        
        [BeginRevealArea(nameof(bar), true)]
        [DocRefInert(manualRef: "237fb495d65834b049da64d12c70ebed,11400000")]
        [TooltipRefInert(assetRef: "8a72288a46f054f73bdb29eef0e2f825,9197481963319205126")]
        public DecoratedList<float> baz;

        [EndRevealArea] [BeginRevealArea(nameof(bar), false)]

        public int dummy;
        [EndRevealArea]
        public Object qux;

        public enum CoolEnum
        {
            Foo = 0,
            Bar = 1,
            Baz = 2,
            Buz = 3
        }

        [Flags]
        public enum FlagsEnum
        {
            Alpha = 1,
            Bravo = 2,
            Charlie = 4,
            Delta = 64
        }

        public CoolEnum coolEnum;
        [BeginEnumRevealArea(nameof(coolEnum), typeof(CoolEnum), BeginEnumRevealAreaAttribute.EnumFlagKind.Off, CoolEnum.Foo)]
        public FlagsEnum flagsEnum;

        [BeginEnumRevealArea(nameof(flagsEnum), typeof(FlagsEnum), BeginEnumRevealAreaAttribute.EnumFlagKind.Any,
            FlagsEnum.Alpha, FlagsEnum.Bravo)]
        public string alphaOrBravo;
        [EndRevealArea]
        [EndRevealArea]
        public Inner1 inner;

        public int whoopsie;
        
        [Serializable]
        public class Inner1
        {
            public bool huh;
            [BeginRevealArea(nameof(huh), true)]
            public Inner2 something;

            [EndRevealArea]
            public int what;
        }

        [Serializable]
        public class Inner2
        {
            public Inner3 something;
        }

        [Serializable]
        public class Inner3
        {
            public Inner4 something;
        }

        [Serializable]
        public class Inner4
        {
            public Inner5 something;
        }

        [Serializable]
        public class Inner5
        {
            public int something;
        }
        
    }
}
