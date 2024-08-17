using CodeBase.Editor.Windows;
using UnityEditor;

namespace CodeBase.Editor
{
    public static class EditorWindowsMenues
    {
        [MenuItem("MyTools/Windows/Init")]
        public static void InitWindows()
        {
            TrafficEditorWindow.Init();
        }
    }
}
