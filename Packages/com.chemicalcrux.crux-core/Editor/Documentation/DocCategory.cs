using System.Collections.Generic;
using ChemicalCrux.CruxCore.Runtime;
using UnityEngine;

namespace ChemicalCrux.CruxCore.Editor.Documentation
{
    /// <summary>
    /// A collection of <see cref="DocPage"/> assets.
    /// </summary>
#if CRUX_DEV
    [CreateAssetMenu(menuName = CoreConsts.AssetDocPath + "Category", order = CoreConsts.AssetInternalOrder)]
#endif
    public class DocCategory : ScriptableObject
    {
        public string label;
        public List<DocPage> pages;
    }
}