using System;
using System.Collections.Generic;
using Crux.Core.Runtime;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ChemicalCrux.CruxCoreTest.Runtime
{
    [CreateAssetMenu]
    public class ExampleAsset : ScriptableObject
    {
        public int foo;
        public bool bar;
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
