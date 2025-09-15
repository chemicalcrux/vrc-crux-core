using UnityEditor;
using UnityEngine;

namespace Crux.Core.Developer
{
    internal static class CopyGuid
    {
        [MenuItem("Assets/Copy GUID")]
        static void Copy()
        {
            if (AssetDatabase.TryGetGUIDAndLocalFileIdentifier(Selection.activeObject, out var guid, out long fileID))
            {
                string reference = guid + "," + fileID;
                EditorGUIUtility.systemCopyBuffer = reference;
                Debug.Log(reference);
            }
        }

        [MenuItem("Assets/Copy GUID", true)]
        static bool CopyValidation()
        {
            return Selection.activeObject && AssetDatabase.Contains(Selection.activeObject);
        }
    }
}