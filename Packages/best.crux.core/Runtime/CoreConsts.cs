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
        public const string MenuRootPath = "Tools/chemicalcrux/";

        public const int MenuRootPriority = 1000;
        public const int MenuDocPriority = MenuRootPriority;
        public const int MenuPackagePriority = MenuRootPriority + 100;

        public const string AssetRootPath = "chemicalcrux/";
        public const string AssetDocPath = AssetRootPath + "Documentation/";

        public const int AssetRootOrder = 0;
        public const int AssetInternalOrder = AssetRootOrder + 0;
        public const int AssetDocOrder = AssetInternalOrder + 0;
        public const int AssetPackageOrder = AssetRootOrder + 100;
    }
}
