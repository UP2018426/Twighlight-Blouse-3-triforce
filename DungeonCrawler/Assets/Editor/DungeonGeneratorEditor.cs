using UnityEngine;
using UnityEditor;


[RequireComponent(typeof(RoomGen))]
public class DungeonGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        RoomGen roomGen = (RoomGen)target;

        if (GUILayout.Button("Spawn Rooms"))
        {
            //roomGen.CreateRoom(roomGen.room.width, roomGen.room.length /*, roomGen.room.height*/);
        }
    }

    void OnSceneGUI()
    {
        RoomGen roomGen = (RoomGen)target;



        Handles.color = Color.magenta;
        Handles.DrawWireArc(roomGen.transform.position, Vector3.up, Vector3.forward, 360, roomGen.radius);
    }

}
