using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HeroConfig))]
public class HeroConfigEditor : Editor
{
    private GUIStyle myStyle;

    private void OnEnable()
    {
        myStyle = new GUIStyle
        {
            fontStyle = FontStyle.BoldAndItalic,
            alignment = TextAnchor.MiddleCenter,
            fontSize = 20,
            wordWrap = true
        };
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("My Scriptable", myStyle);
        EditorGUILayout.Space();

        DrawDefaultInspector(); //dibuja por defecto el inspector
    }
}
