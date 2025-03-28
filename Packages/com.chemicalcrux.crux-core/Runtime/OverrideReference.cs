using System;
using UnityEngine;

namespace ChemicalCrux.CruxCore.Runtime
{
    [Serializable]
    public class OverrideReference<T> where T : UnityEngine.Object
    {
        public T reference;
    }
}