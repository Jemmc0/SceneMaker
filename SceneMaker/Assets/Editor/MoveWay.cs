using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(MoveWaypoint))]

public class MoveWay : Editor
{
    private MoveWaypoint _waypoints;

    private void OnEnable()
    {
        //_custom = (Custom)target;
        _waypoints = (MoveWaypoint)target;
    }

    private void OnSceneGUI()
    {
        //Puedo desactivar los handles por defecto
        Tools.current = Tool.None;
        //Posicion
        _waypoints.transform.position = Handles.PositionHandle(_waypoints.transform.position, _waypoints.transform.rotation);

        //Rotacion
        _waypoints.transform.rotation = Handles.RotationHandle(_waypoints.transform.rotation, _waypoints.transform.position);

        //Tamaño
        _waypoints.transform.localScale = Handles.ScaleHandle(_waypoints.transform.localScale, _waypoints.transform.position, _waypoints.transform.rotation, 0.2f);
    }
}
