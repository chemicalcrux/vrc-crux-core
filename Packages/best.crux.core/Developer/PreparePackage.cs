using System.IO;
using Crux.Core.Runtime;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Crux.Core.Developer
{
    public static class PreparePackage
    {
        // https://learn.microsoft.com/en-us/dotnet/standard/io/how-to-copy-directories
        static void CopyDirectory(string sourceDir, string destinationDir, bool recursive)
        {
            // Get information about the source directory
            var dir = new DirectoryInfo(sourceDir);

            // Check if the source directory exists
            if (!dir.Exists)
                throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");

            // Cache directories before we start copying
            DirectoryInfo[] dirs = dir.GetDirectories();

            // Create the destination directory
            Directory.CreateDirectory(destinationDir);

            // Get the files in the source directory and copy to the destination directory
            foreach (FileInfo file in dir.GetFiles())
            {
                string targetFilePath = Path.Combine(destinationDir, file.Name);
                file.CopyTo(targetFilePath, true);
            }

            // If recursive and copying subdirectories, recursively call this method
            if (recursive)
            {
                foreach (DirectoryInfo subDir in dirs)
                {
                    string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
                    CopyDirectory(subDir.FullName, newDestinationDir, true);
                }
            }
        }
    
        [MenuItem("Assets/Prepare", false, CoreConsts.ContextAssetOrder)]
        static void Prepare()
        {
            var root = Path.GetDirectoryName(Application.dataPath);
            var manifestPath = AssetDatabase.GetAssetPath(Selection.activeObject);
        
            var packageRelative = Path.GetDirectoryName(manifestPath);
        
            var templateDir = Path.Join(root, "Assets", "Sample Templates");

            var packageDir = Path.Join(root, packageRelative);
            var samplesDir = Path.Join(packageDir, "Samples~");

            var templateRelative = Path.GetRelativePath(root, templateDir);

            ReserializeAssets.ReserializeDirectory(templateRelative);
            ReserializeAssets.ReserializeDirectory(packageRelative);

            if (new DirectoryInfo(samplesDir).Exists)
                new DirectoryInfo(samplesDir).Delete(true);
        
            CopyDirectory(templateDir, samplesDir, true);
        }

        [MenuItem("Assets/Prepare", true, CoreConsts.ContextAssetOrder)]
        static bool PrepareValidation()
        {
            return Selection.activeObject && AssetDatabase.Contains(Selection.activeObject) &&
                   Selection.activeObject.name == "package" && Selection.activeObject is PackageManifest;
        }
    }
}