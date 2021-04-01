using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGen : MonoBehaviour
{
    public int Zsize;
    public int Xsize;

    public float gridSpacing;

    public Vector3[] gPoints;


    void Start()
    {
        CreateGrid();
    }

    public void CreateGrid()
    {
        gPoints = new Vector3[(Xsize + 1) * (Zsize + 1)];

        for (int i = 0, z = 0; z <= Zsize; z++)
        {
            for (int x = 0; x <= Xsize; x++)
            {
                gPoints[i] = new Vector3(x * gridSpacing, 0, z * gridSpacing);
                ++i;
            }
        }
    }


    public float gizmoSize = 1f;

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;

        for (int i = 0; i < gPoints.Length; i++)
        {
            //Gizmos.DrawCube(Verticies[i], new Vector3(0.1f,0.1f,0.1f));
            Gizmos.DrawSphere(gPoints[i], gizmoSize);
        }
    }
}