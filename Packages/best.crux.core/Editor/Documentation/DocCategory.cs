using System.Collections.Generic;
using Crux.Core.Runtime;
using UnityEngine;

namespace Crux.Core.Editor.Documentation
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