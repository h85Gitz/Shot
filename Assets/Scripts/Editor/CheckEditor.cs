using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class CheckEditor : MonoBehaviour
    {
        [MenuItem("Check/if Editor is Updating")]
        public static void CheckIfEditorIsUpdating()
        {
            if (EditorApplication.isUpdating)
            {
                Debug.Log("Editor is currently updating.");
            }
            else
            {
                Debug.Log("Editor is not updating.");
            }
        }
    }
}