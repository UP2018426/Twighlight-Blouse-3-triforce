using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDungeon : MonoBehaviour
{
    //public RoomSpawner roomSpawner;


    //RoomTemplate template;

    public GameObject[] startingRooms;

    [Range(0, 100)]
    public int numRoomsToSpawn;

    public bool spwning = false;

    void Awake()
    {
        //template = GameObject.FindGameObjectWithTag("Room").GetComponent<RoomTemplate>();

        //roomSpawner = new RoomSpawner();



        //Invoke("Spawn", 3f);
    }

    //void Start()
    //{
    //    _SpawnDungeon();
    //}


    private void Update()
    {
        /*
        if(spawned == roomsspawned after spawnign rooms)
        {
            
        }
        */

        if (!spwning)
        {
            CreateDungeon();
        }
    }

    void CreateDungeon()
    {
        switch (Random.Range(0, 3)) 
        {
            case 0:
                Instantiate(startingRooms[0],new Vector3(0,0,0),Quaternion.identity);
                //room with door at the bottom
                //Instantiate(template.bottomRooms[Random.Range(0, template.bottomRooms.Length)], transform.position, transform.rotation);
                break;
            case 1:
                Instantiate(startingRooms[1], new Vector3(0, 0, 0), Quaternion.identity);
                //room with a door at the top
                //Instantiate(template.topRooms[Random.Range(0, template.topRooms.Length)], transform.position, transform.rotation);
                break;
            case 2:
                Instantiate(startingRooms[2], new Vector3(0, 0, 0), Quaternion.identity);
                //room with a door on the left
                //Instantiate(template.leftRooms[Random.Range(0, template.leftRooms.Length)], transform.position, transform.rotation);
                break;
            case 3:
                Instantiate(startingRooms[3], new Vector3(0, 0, 0), Quaternion.identity);
                //room with a door on the right
                //Instantiate(template.rightRooms[Random.Range(0, template.rightRooms.Length)], transform.position, transform.rotation);
                break;
        }
    }

    public void Test()
    {
        CreateDungeon();
    }


}
