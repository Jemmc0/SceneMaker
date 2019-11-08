using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(Waypoints))]

public class SceneMaker : Editor
{
    //private Custom _custom;
    private Waypoints _waypoints;

    private void OnEnable()
    {
        //_custom = (Custom)target;
        _waypoints = (Waypoints)target;
    }

    public override void OnInspectorGUI()
    {
        DrawHeroParameters();
    }

    private void DrawHeroParameters()
    {
        _waypoints.countx = EditorGUILayout.IntField("GridX", _waypoints.countx);
        _waypoints.countz = EditorGUILayout.IntField("GridY", _waypoints.countz);
        if (GUILayout.Button("Grilla"))
            Grid();
    }

    void Grid()
    {
        _waypoints.CreateTarget(_waypoints.countx,_waypoints.countz);
    }   

        
}
