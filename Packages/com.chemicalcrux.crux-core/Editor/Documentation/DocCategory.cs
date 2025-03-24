using System.Collections.Generic;
using UnityEngine;

namespace ChemicalCrux.CruxCore.Editor.Documentation
{
    [CreateAssetMenu]
    public class DocCategory : ScriptableObject
    {
        public string label;
        public List<DocPage> pages;
    }
}