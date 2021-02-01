using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(GridGen))]
public class GridGenEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GridGen generator = (GridGen)target;

        if (GUILayout.Button("GenerateDungeon"))
        {
            Debug.Log("click");

            generator.CreateGrid();
        }
    }

}