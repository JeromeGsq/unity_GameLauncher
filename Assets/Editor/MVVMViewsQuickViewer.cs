#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class MVVMViewsQuickViewer : EditorWindow
{
    private Vector2 scroll = Vector2.zero;

    [MenuItem("Tools/MVVM/MVVMViewsQuickViewer")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(MVVMViewsQuickViewer));
    }

    private void OnGUI()
    {
        this.scroll = EditorGUILayout.BeginScrollView(this.scroll);
        var prefabs = Resources.LoadAll("Prefabs/UI/Views");

        if (GUILayout.Button("Main scene", GUILayout.Height(40), GUILayout.Width(100)))
        {
            StageUtility.GoToMainStage();
        }

        GUILayout.Space(10);

        foreach (var item in prefabs)
        {
            if (item.name.Equals("ExempleScreen"))
            {
                continue;
            }

            GUILayout.Space(5);

            if (GUILayout.Button(item.name, GUILayout.Height(30), GUILayout.Width(100)))
            {
                AssetDatabase.OpenAsset(item);
            }
        }

        EditorGUILayout.EndScrollView();
    }
}
#endif