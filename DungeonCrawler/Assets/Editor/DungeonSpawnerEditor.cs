using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(SpawnDungeon))]
public class DungeonSpawnerEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SpawnDungeon spwn = (SpawnDungeon)target;

        if(GUILayout.Button("Spawn Initial Room"))
        {
            spwn.Test();
        }
    }
    
}
