using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(Waypoints))]

public class SceneMaker : Editor
{
    private Waypoints _waypoints;

    private void OnEnable()
    {
        _waypoints = (Waypoints)target;
    }

    public override void OnInspectorGUI()
    {
        Grilla();
    }

    private void Grilla()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("GridX", GUILayout.MaxWidth(110f));
        _waypoints.countx = EditorGUILayout.IntField(_waypoints.countx);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("GridY", GUILayout.MaxWidth(110f));
        _waypoints.countz = EditorGUILayout.IntField(_waypoints.countz);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Waypoint",GUILayout.MaxWidth(110f));
        _waypoints.waypointPrefab = (GameObject)EditorGUILayout.ObjectField(_waypoints.waypointPrefab, typeof(GameObject),true);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Terrain", GUILayout.MaxWidth(110f));
        _waypoints.terrain = (GameObject)EditorGUILayout.ObjectField(_waypoints.terrain, typeof(GameObject), true);
        EditorGUILayout.EndHorizontal();
        if (GUILayout.Button("Grilla"))
            Grid();
        if (GUILayout.Button("Borrar waypoints"))
            ClearWaypoints();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Render Waypoints", GUILayout.MaxWidth(110f));
        _waypoints.rende = EditorGUILayout.Toggle(_waypoints.rende);
        EditorGUILayout.EndHorizontal();
        
        Renderer();

    }

    void Grid()
    {
        _waypoints.CreateTarget(_waypoints.countx,_waypoints.countz,_waypoints.terrain);
    }   
    void ClearWaypoints()
    {
        while (_waypoints.terrain.transform.Find("Waypoints").childCount != 0)
            DestroyImmediate(_waypoints.terrain.transform.Find("Waypoints").GetChild(0).gameObject);
    }

    void Renderer()
    {
        if (_waypoints.terrain.transform.Find("Waypoints"))
        {
            foreach (Transform i in _waypoints.terrain.transform.Find("Waypoints"))
            i.GetComponent<Renderer>().enabled = _waypoints.rende;
        }
    }
        
}
