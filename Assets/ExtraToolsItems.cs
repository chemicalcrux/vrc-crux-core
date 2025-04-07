using Crux.Core.Runtime;
using UnityEditor;

internal static class ExtraToolsItems
{
    [MenuItem("Tools/Foo")]
    [MenuItem("Tools/Bar")]
    [MenuItem("Tools/Offset2000", priority = 2000)]
    [MenuItem(CoreConsts.MenuRootPath + "Whatever/Foo", priority = CoreConsts.MenuPackagePriority)]
    [MenuItem(CoreConsts.MenuRootPath + "Another/Foo", priority = CoreConsts.MenuPackagePriority)]
    [MenuItem(CoreConsts.MenuRootPath + "Lots Of Packages/Foo", priority = CoreConsts.MenuPackagePriority)]
    private static void Bogus() { }
}