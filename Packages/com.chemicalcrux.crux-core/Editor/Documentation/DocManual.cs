using System.Collections.Generic;
using ChemicalCrux.CruxCore.Runtime;
using UnityEngine;

namespace ChemicalCrux.CruxCore.Editor.Documentation
{
#if CRUX_DEV
    [CreateAssetMenu(menuName = CoreConsts.AssetDocPath + "Manual", order = CoreConsts.AssetInternalOrder)]
#endif
    public class DocManual : ScriptableObject
    {
        public string title;
        public string description;
        public Texture2D icon;
        
        public List<DocCategory> categories;
    }
}