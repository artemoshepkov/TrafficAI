using CodeBase.Tools;
using UnityEditor;
using UnityEngine;

namespace CodeBase.Editor.Windows
{
    public class TrafficEditorWindow : EditorWindow
    {
        private static TrafficEditorWindow _window;

        public static void Init() => _window = EditorWindow.GetWindow<TrafficEditorWindow>("TrafficEditor");

        private void OnGUI()
        {
            EditorGUILayout.BeginVertical();

            if (GUILayout.Button("Reset graph"))
            {
                TrafficGraph.Reset();
            }
            
            if (GUILayout.Button("Reset roads prefabs"))
            {
                TrafficTools.ResetRoadsPrefabs();
            }
            
            EditorGUILayout.EndVertical();
        }
    }
}