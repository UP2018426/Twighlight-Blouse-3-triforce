using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.AI;

public class NavMeshGen : MonoBehaviour
{
    void Start()
    {
        NavMeshBuilder.BuildNavMesh();
    }
}
