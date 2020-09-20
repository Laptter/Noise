using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(SurfaceCreator))]
public class SurfaceCreatorEditor : Editor
{
    private SurfaceCreator creator;
    private void OnEnable()
    {
        creator = target as SurfaceCreator;
        Undo.undoRedoPerformed += RefreshCreator;
    }

    void RefreshCreator()
    {
        if (Application.isPlaying)
        {
            creator.Refresh();
        }
    }

    private void OnDisable()
    {
        Undo.undoRedoPerformed -= RefreshCreator;   
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        DrawDefaultInspector();
        if (EditorGUI.EndChangeCheck())
        {
            RefreshCreator();
        }
    }
}
