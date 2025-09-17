using JetBrains.Annotations;

namespace Crux.Core.Runtime
{
    /// <summary>
    /// Contains useful constant values. Mostly used for attributes like
    /// <see cref="UnityEditor.MenuItem"/>. 
    /// </summary>
    [PublicAPI]
    public static class CoreConsts
    {
        /// <summary>
        /// The root path that all <see cref="UnityEditor.MenuItem"/> attributes should use
        /// </summary>
        public const string MenuRootPath = "Tools/chemicalcrux/";

        /// <summary>
        /// The base priority that all <see cref="UnityEditor.MenuItem"/> attributes should use
        /// </summary>
        public const int MenuRootPriority = 1000;
        /// <summary>
        /// The base priority for documentation-related menu items
        /// </summary>
        public const int MenuDocPriority = MenuRootPriority;
        /// <summary>
        /// The base priority for most menu items added by other packages
        /// </summary>
        public const int MenuPackagePriority = MenuRootPriority + 100;

        /// <summary>
        /// The root path that all <see cref="UnityEngine.CreateAssetMenuAttribute"/> attributes should use
        /// </summary>
        public const string AssetRootPath = "chemicalcrux/";
        /// <summary>
        /// The root path for anything documentation-related
        /// </summary>
        public const string AssetDocPath = AssetRootPath + "Documentation/";

        /// <summary>
        /// The base order that all <see cref="UnityEngine.CreateAssetMenuAttribute"/> attributes should use
        /// </summary>
        public const int AssetRootOrder = 0;
        /// <summary>
        /// The base order for anything internal -- that is, only exposed when CRUX_DEV is defined
        /// </summary>
        public const int AssetInternalOrder = AssetRootOrder + 0;
        /// <summary>
        /// The base order for anything documentation-related
        /// </summary>
        public const int AssetDocOrder = AssetInternalOrder + 0;
        
        /// <summary>
        /// The base order used by other packages
        /// </summary>
        public const int AssetPackageOrder = AssetRootOrder + 100;

        /// <summary>
        /// The root path that all <see cref="UnityEngine.AddComponentMenu"/> attributes should use
        /// </summary>
        public const string ComponentRootPath = "chemicalcrux/";

        /// <summary>
        /// The root order that all <see cref="UnityEngine.AddComponentMenu"/> attributes should use
        /// </summary>
        public const int ComponentRootOrder = 0;
        /// <summary>
        /// The base order for anything internal -- that is, only exposed when CRUX_DEV is defined
        /// </summary>
        public const int ComponentInternalOrder = ComponentRootOrder + 0;
        /// <summary>
        /// The base order for components added by other packages
        /// </summary>
        public const int ComponentPackageOrder = ComponentRootOrder + 1000;

        /// <summary>
        /// The base order for anything that goes in an asset's context menu
        /// </summary>
        public const int ContextAssetOrder = 100;
    }
}
