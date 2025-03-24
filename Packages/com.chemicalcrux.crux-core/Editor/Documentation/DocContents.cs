using System.Collections.Generic;
using UnityEngine;

namespace ChemicalCrux.CruxCore.Editor.Documentation
{
    [CreateAssetMenu]
    public class DocContents : ScriptableObject
    {
        public string title;
        public string description;
        public Texture2D icon;
        
        public List<DocCategory> categories;
    }
}