using UnityEditor;
using UnityEngine;

namespace ChemicalCrux.CruxCore.Editor
{
    public class AssetReference
    {
        public readonly string guid;
        public readonly long fileID;

        public AssetReference(string guid, long fileID)
        {
            this.guid = guid;
            this.fileID = fileID;
        }
        
        public static bool TryParse (string serialized, out AssetReference result)
        {
            result = default;

            var parts = serialized.Split(",");

            if (parts.Length != 2 || !GUID.TryParse(parts[0], out var _) || !long.TryParse(parts[1], out var fileID))
            {
                Debug.LogWarning("An asset reference was invalid. Please report this as a bug!\nReference: " + serialized);
                return false;
            }

            result = new AssetReference(parts[0], fileID);
            return true;
        }
    }
}