using System;
using System.Collections.Generic;
using Crux.Core.Runtime;
using Crux.Core.Runtime.Attributes;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ChemicalCrux.CruxCoreTest.Runtime
{
    [CreateAssetMenu]
    public class ExampleAsset : ScriptableObject
    {
        public int foo;
        public bool bar;
        [DocRefInert(manualRef: "237fb495d65834b049da64d12c70ebed,11400000")]
        [TooltipRefInert(assetRef: "8a72288a46f054f73bdb29eef0e2f825,9197481963319205126")]
        public DecoratedList<float> baz;
        public Object qux;

        [Serializable]
        public class Inner1
        {
            public Inner2 something;
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
        
        public Inner1 inner;
    }
}
