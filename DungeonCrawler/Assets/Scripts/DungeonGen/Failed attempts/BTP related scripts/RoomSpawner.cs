using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    //1 == need bottom door to connect
    //2 == need top door to connect
    //3 == need left door to connect
    //4 == need right door to connect

    private RoomTemplate template;

    private bool spawned = false;

    private int rand;

    private SpawnDungeon spwnDungeon;



    //private int roomsSpawned;


    private void Awake()
    {
        //spwnDungeon = GameObject.FindGameObjectWithTag("InitialSpawner").GetComponent<SpawnDungeon>();

        template = GameObject.FindGameObjectWithTag("Room").GetComponent<RoomTemplate>();
        
    }

    void Start()
    {
        Invoke("Spawn", 0.1f);
    }

    //increse probability of certian room spawning requires same room to be added to the array or I create slot machine effect(re roll on percentage chance)
    public void Spawn()
    {
        if (!spawned) 
        {
            switch (openingDirection)
            {
                case 1:
                    //room with door at the bottom
                    rand = Random.Range(0, template.bottomRooms.Length);
                    Instantiate(template.bottomRooms[rand], transform.position, template.bottomRooms[rand].transform.rotation);
                    break;
                case 2:
                    //room with a door at the top
                    rand = Random.Range(0, template.topRooms.Length);
                    Instantiate(template.topRooms[rand], transform.position, template.topRooms[rand].transform.rotation);
                    break;
                case 3:
                    //room with a door on the left
                    rand = Random.Range(0, template.leftRooms.Length);
                    Instantiate(template.leftRooms[rand], transform.position, template.leftRooms[rand].transform.rotation);
                    break;
                case 4:
                    //room with a door on the right
                    rand = Random.Range(0, template.rightRooms.Length);
                    Instantiate(template.rightRooms[rand], transform.position, template.rightRooms[rand].transform.rotation);
                    break;
            }
            spawned = true;
            //spwnDungeon.spwning = true;
        }
        
        
        //roomsSpawned++;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("SpawnPoint"))
        {
            if(other.GetComponent<RoomSpawner>().spawned == false && spawned == false)
            {
                //spawn wall to block opening
                //need fix for initial room as when rooms spawn at same time room gets filled with closed room
                Instantiate(template.closedRoom, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            spawned = true;
        }
    }


    
    private void Update()
    {
        
    }
    
}
