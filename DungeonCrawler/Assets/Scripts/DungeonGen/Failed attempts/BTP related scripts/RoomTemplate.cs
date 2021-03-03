using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplate : MonoBehaviour
{
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;

    public GameObject closedRoom;

    public List<GameObject> rooms;


    //public float waitTime;
    //private bool SpawnedBoss = false;
    //public GameObject Boss;

    /*
    private void Update()
    {
        if (waitTime <= 0 && SpawnedBoss)
        {
            for (int i = 0; i < rooms.Count - 1; i++)
            {
                if (i == rooms.Count - 1)
                {
                    Instantiate(Boss, rooms[i].transform.position, Quaternion.identity);
                }
            }
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }
    */
}
