using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkGensize : MonoBehaviour
{
    private Camera mainCamera;

    private IstvanRoomGenDelete rooms;


    Ray camRay;
    float rayLength;

    Vector3 fromPos;
    Vector3 toPos;
    Vector3 dir;

    //void Awake()
    //{
    //    mainCamera = FindObjectOfType<Camera>();

    //    roomGen = GameObject.FindGameObjectWithTag("start").GetComponent<IstvanRoomGen>();
    //}




    private void Start()
    {
        //for (int i = 0, z = 0; z <= roomGen.Zsize; z++)
        //{
        //    for (int x = 0; x <= roomGen.Xsize; x++)
        //    {
        //        fromPos = transform.position;

        //        toPos = new Vector3(x * roomGen.gridSpacing, 0, z * roomGen.gridSpacing);

        //        dir = toPos - fromPos;

        //        RaycastHit hit;

        //        if (Physics.Raycast(fromPos, dir, out hit, Mathf.Infinity))
        //        {
        //            if (true)//needs to add room to list
        //            {

        //            }
        //        }
        //        ++i;
        //    }
        //}


    }




    private void Awake()
    {
        CreateGrid();

        //rooms = GameObject.FindGameObjectWithTag("Sphere").GetComponents<IstvanRoomGenDelete>();
    }

    public int Zsize;
    public int Xsize;

    public float gridSpacing;

    private Vector3[] gPoints;

    public GameObject SphereObj;

    public void CreateGrid()
    {
        gPoints = new Vector3[(Xsize + 1) * (Zsize + 1)];

        for (int i = 0, z = 0; z <= Zsize; z++)
        {
            for (int x = 0; x <= Xsize; x++)
            {
                gPoints[i] = new Vector3(x * gridSpacing, 0, z * gridSpacing);
                Instantiate(SphereObj, gPoints[i], Quaternion.identity);
                ++i;
            }
        }
    }

    public float gizmoSize = 1f;

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;

    //    for (int i = 0; i < gPoints.Length; i++)
    //    {
    //        //Gizmos.DrawCube(Verticies[i], new Vector3(0.1f,0.1f,0.1f));
    //        Gizmos.DrawSphere(gPoints[i], gizmoSize);
    //    }
    //}



    public List<GameObject> grid;

    public List<GameObject> boss;


    //may want to check how many rooms were is coneceteded

    //need to random pick an obj from the grid list and set value as

    private void Update()
    {
        if (Input.GetKeyDown(";"))
        {
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("Sphere").Length; i++)
            {
                grid.Add(GameObject.FindGameObjectsWithTag("Sphere")[i]);
            }

            var tjing = Random.Range(0, grid.Count);

            grid[tjing].gameObject.GetComponent<IstvanRoomGenDelete>().isConected = true;
        }

        if (Input.GetKeyDown("/"))
        {
            for (int i = 0; i < grid.Count; i++)
            {
                if (grid[i].GetComponent<IstvanRoomGenDelete>().corners == 1 && grid[i].GetComponent<IstvanRoomGenDelete>().isConected)
                {
                    boss.Add(grid[i]);//all that needs to happen is that it sets two rooms with start / end
                }
            }

            var num = Random.Range(0, boss.Count);

            boss[num].gameObject.GetComponent<IstvanRoomGenDelete>().isBeegBoss = true;

            boss.RemoveAt(num);

            var num2 = Random.Range(0, boss.Count);

            boss[num2].gameObject.GetComponent<IstvanRoomGenDelete>().start = true;
        }

        if (Input.GetKeyDown("return"))
        {
            SpawnDungeon();
        }
    }

    void LogDungeon()
    {
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Sphere").Length; i++)
        {
            grid.Add(GameObject.FindGameObjectsWithTag("Sphere")[i]);
        }

        var tjing = Random.Range(0, grid.Count);

        grid[tjing].gameObject.GetComponent<IstvanRoomGenDelete>().isConected = true;
    }

    void Log1DoorRooms()
    {
        for (int i = 0; i < grid.Count; i++)
        {
            if (grid[i].GetComponent<IstvanRoomGenDelete>().corners == 1 && grid[i].GetComponent<IstvanRoomGenDelete>().isConected)
            {
                boss.Add(grid[i]);//all that needs to happen is that it sets two rooms with start / end
            }
        }

        var num = Random.Range(0, boss.Count);

        boss[num].gameObject.GetComponent<IstvanRoomGenDelete>().isBeegBoss = true;

        boss.RemoveAt(num);

        var num2 = Random.Range(0, boss.Count);

        boss[num2].gameObject.GetComponent<IstvanRoomGenDelete>().start = true;
    }

    public bool delete = false;
    public bool check = false;
    public bool spawn = false;

    bool logDungeon = false;
    bool log1Rooms = false;

    public void SpawnDungeon()
    {
        // 'k'(delete grid objs)

        //rooms.Delete();

        if (delete == true &! logDungeon)
        {
            LogDungeon();
            logDungeon = true;
        }

        // ';'(adds all remaining grid objs to )

        //LogDungeon();

        // 'j'(checks what rooms to make and if they are connected)

        //rooms.RoomChecking();

        if (check == true &! log1Rooms)
        {
            Log1DoorRooms();
            log1Rooms = true;
        }

        // '/'(adds all rooms with 1 room to a list so that two can be picked for start and end)

        //Log1DoorRooms();

        // 'h'(spawns the rooms and sets corners may need to swap the coners setting to before spawning to get the things working properly)

        //rooms.Spawn();

        //if (spawn == false)
        //{
        //    spawn = true;
        //}

        // needs to be used to reset if there were problems possibly or earlier

        //needs to delete and redo all if dungoen not right(size or lacking start and end room)
    }

    // 'k'(delete grid objs)
    // ';'(adds all remaining grid objs to)
    // 'j'(checks what rooms to make and if they are connected)
    // '/'(adds all rooms with 1 room to a list so that two can be picked for start and end)
    // 'h'(spawns the rooms and sets corners may need to swap the coners setting to before spawning to get the things working properly)
}