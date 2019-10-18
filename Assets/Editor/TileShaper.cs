using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Tile))]
[CanEditMultipleObjects]
public class TileEditor : Editor
{
    private int height;
    private bool showHandles;

    void OnEnable()
    {
        Tile tile = (Tile)target;
        height = Mathf.RoundToInt(tile.transform.localScale.y);
        showHandles = true;
    }

    public override void OnInspectorGUI()
    {
        //EditorGUILayout.BeginHorizontal();
        height = EditorGUILayout.IntSlider(new GUIContent("Height"), height, 1, 10);
        //EditorGUILayout.EndHorizontal();
        foreach (Object o in targets)
        {
            Tile t = o as Tile;
            t.transform.localScale = new Vector3(1, height, 1);
        }

        showHandles = EditorGUILayout.Toggle(showHandles);
        if (!showHandles)
        {
            Tools.current = Tool.None;
        }

        if (GUILayout.Button("Save"))
        {
            EditorUtility.SetDirty(target);
        }


    }

}
