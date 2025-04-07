using Crux.Core.Runtime;
using UnityEngine;
using UnityEngine.UIElements;

namespace Crux.Core.Editor.Documentation
{
    /// <summary>
    /// A single page of documentation, which corresponds to a single UXML document.
    /// </summary>
#if CRUX_DEV
    [CreateAssetMenu(menuName = CoreConsts.AssetDocPath + "Page", order = CoreConsts.AssetInternalOrder)]
#endif
    public class DocPage : ScriptableObject
    {
        public string title;
        public VisualTreeAsset document;
    }
    
}
