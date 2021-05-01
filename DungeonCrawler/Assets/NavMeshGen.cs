using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshGen : MonoBehaviour
{
    public NavMeshSurface surface;
    //void Start()
    //{
    //    surface.BuildNavMesh();
    //}

    private void Update()
    {
        surface.BuildNavMesh();
    }
}
