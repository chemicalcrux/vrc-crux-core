using ChemicalCrux.CruxCore.Runtime;
using UnityEditor;

internal static class ExtraToolsItems
{
    [MenuItem("Tools/Foo")]
    [MenuItem("Tools/Bar")]
    [MenuItem("Tools/Offset2000", priority = 2000)]
    [MenuItem(CoreConsts.MenuRoot + "Whatever/Foo", priority = CoreConsts.SubmenuPriority)]
    [MenuItem(CoreConsts.MenuRoot + "Another/Foo", priority = CoreConsts.SubmenuPriority)]
    [MenuItem(CoreConsts.MenuRoot + "Lots Of Packages/Foo", priority = CoreConsts.SubmenuPriority)]
    private static void Bogus() { }
}