using UnityEditor;
using UnityEngine;

namespace ChemicalCrux.CruxCore.Editor
{
    internal static class CopyGuid
    {
#if CRUX_DEV
        [MenuItem("Assets/Copy GUID")]
        static void Copy()
        {
            if (AssetDatabase.TryGetGUIDAndLocalFileIdentifier(Selection.activeObject, out var guid, out long fileID))
            {
                EditorGUIUtility.systemCopyBuffer = guid + "," + fileID;
                Debug.Log(guid + " " + fileID);
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