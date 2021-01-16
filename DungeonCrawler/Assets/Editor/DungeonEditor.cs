using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(DungeonGenerator))]
public class NewBehaviourScript : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        DungeonGenerator generator = (DungeonGenerator)target;

        if(GUILayout.Button("GenerateDungeon"))
        {
            Debug.Log("click");

            generator.CreateGrid();
        }
    }

}
