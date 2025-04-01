using System.Collections.Generic;
using UnityEngine;

namespace ChemicalCrux.CruxCoreTest.Runtime
{
    [CreateAssetMenu]
    public class ExampleAsset : ScriptableObject
    {
        public int foo;
        public bool bar;
        public List<float> baz;
        public Object qux;
    }
}
