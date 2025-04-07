using UnityEditor;
using UnityEngine;

namespace Crux.Core.Editor
{
    internal static class CopyGuid
    {
#if CRUX_DEV
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
            return AssetDatabase.Contains(Selection.activeObject);
        }
#endif
    }
}