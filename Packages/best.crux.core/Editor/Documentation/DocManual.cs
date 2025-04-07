using System.Collections.Generic;
using Crux.Core.Runtime;
using UnityEngine;

namespace Crux.Core.Editor.Documentation
{
    /// <summary>
    /// The root of a documentation set.
    /// </summary>
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