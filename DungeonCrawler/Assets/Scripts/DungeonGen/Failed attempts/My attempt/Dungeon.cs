using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    [SerializeField]
    int numberOfRooms;
    
    bool isStartRoom;
    bool isFinishRoom;

    Vector3[] roomLocation;

    [SerializeField]
    GameObject[] Rooms;

    private void Awake()
    {
        CreateDungeon(numberOfRooms);
    }


    void CreateDungeon(int _numberOfRooms)
    {
        
    }

}
