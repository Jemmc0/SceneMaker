
using UnityEditor;

[CustomEditor(typeof(MoveWaypoint))]

public class MoveWay : Editor
{
    private MoveWaypoint _waypoints;

    private void OnEnable()
    {
        _waypoints = (MoveWaypoint)target;
    }

    private void OnSceneGUI()
    {
        Tools.current = Tool.None;

        _waypoints.transform.position = Handles.PositionHandle(_waypoints.transform.position, _waypoints.transform.rotation);

        _waypoints.transform.rotation = Handles.RotationHandle(_waypoints.transform.rotation, _waypoints.transform.position);

        _waypoints.transform.localScale = Handles.ScaleHandle(_waypoints.transform.localScale, _waypoints.transform.position, _waypoints.transform.rotation, 0.2f);
    }
}
