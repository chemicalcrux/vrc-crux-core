using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ChemicalCrux.CruxCoreTest.Runtime
{
    [CreateAssetMenu]
    public class ExampleAsset : ScriptableObject
    {
        public int foo;
        public bool bar;
        public List<float> baz;
        public Object qux;

        [Serializable]
        public class Inner
        {
            public int something;
        }
        
        public Inner inner;
    }
}
