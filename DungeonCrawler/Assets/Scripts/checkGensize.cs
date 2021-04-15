using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkGensize : MonoBehaviour
{
    [SerializeField]
    private int minRoomNum;
    [SerializeField]
    private int maxRoomNum;
        
    private void Start()
    {
        StartCoroutine("LogDelay",0.1f);
        //StopCoroutine("LogDelay");
    }
    public int connected = 0;
    IEnumerator LogDelay(float delay)
    {
        while (!enoughroom)
        {
            Debug.Log("While Loop 1");
            while (connected < minRoomNum || connected > maxRoomNum)
            {
                Debug.Log("While Loop 2");
                Delete();
                Debug.Log("Delete");
                yield return new WaitForSeconds(delay);
                LogDungeon();
                Debug.Log("Logged");
                yield return new WaitForSeconds(delay);
                RoomChecking();
                Debug.Log("Checked");
                if (connected < minRoomNum || connected > maxRoomNum)
                {
                    Debug.Log("Connected < 5");
                    for (int i = 0; i < grid.Count; i++)
                    {
                        grid[i].gameObject.GetComponent<IstvanRoomGenDelete>().boom = true;
                    }//send trigger to all spheres to destroy themselves
                    Debug.Log("grid Objs destroyed");
                    grid.Clear();

                    maybeDelete.Clear();

                    Debug.Log("List cleared");
                    connected = 0;
                    Debug.Log("connected " + connected);
                    CreateGrid();
                    Debug.Log("Grid Respawned");
                }

                //Debug.Log(connected);

            }//may not be needed may ned to do it when log 1 dorr rooms fails
            yield return new WaitForSeconds(delay);
            enoughroom = Log1DoorRooms();//not quite right
            Debug.Log("logged + Assigned Start + End");
            if (!enoughroom)
            {
                Debug.Log("Boss List Clear");
                boss.Clear();
                for (int i = 0; i < grid.Count; i++)
                {
                    grid[i].gameObject.GetComponent<IstvanRoomGenDelete>().boom = true;
                }//send trigger to all spheres to destroy themselves
                Debug.Log("grid Objs destroyed");
                grid.Clear();

                maybeDelete.Clear();

                Debug.Log("List cleared");
                connected = 0;
                CreateGrid();
                Debug.Log("Grid Respawned");
            }
        }
        yield return new WaitForSeconds(delay);
        Spawn();
        Debug.Log("Spawned");
    }

    //Debug.Log("");
    private void Awake()
    {
        CreateGrid();
        //rooms = GameObject.FindGameObjectWithTag("Sphere").GetComponent<IstvanRoomGenDelete>();
    }

    public int Zsize;
    public int Xsize;

    public float gridSpacing;

    private Vector3[] gPoints;

    public GameObject SphereObj;

    public void CreateGrid()
    {
        gPoints = new Vector3[(Xsize) * (Zsize)];

        for (int i = 0, z = 0; z < Zsize; z++)
        {
            for (int x = 0; x < Xsize; x++)
            {
                gPoints[i] = new Vector3(x * gridSpacing, 0, z * gridSpacing);
                Instantiate(SphereObj, gPoints[i], Quaternion.identity);
                ++i;
            }
        }
    }

    public float gizmoSize = 1f;

    bool enoughroom = false;
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;

    //    for (int i = 0; i < gPoints.Length; i++)
    //    {
    //        //Gizmos.DrawCube(Verticies[i], new Vector3(0.1f,0.1f,0.1f));
    //        Gizmos.DrawSphere(gPoints[i], gizmoSize);
    //    }
    //}

    public List<GameObject> maybeDelete;

    public List<GameObject> grid;

    public List<GameObject> boss;


    //may want to check how many rooms were is coneceteded

    //need to random pick an obj from the grid list and set value as

    private void Update()
    {
        //if (Input.GetKeyDown(";"))
        //{
        //    for (int i = 0; i < GameObject.FindGameObjectsWithTag("Sphere").Length; i++)
        //    {
        //        grid.Add(GameObject.FindGameObjectsWithTag("Sphere")[i]);
        //    }

        //    var tjing = Random.Range(0, grid.Count);

        //    grid[tjing].gameObject.GetComponent<IstvanRoomGenDelete>().isConected = true;
        //}

        //if (Input.GetKeyDown("/"))
        //{
        //    for (int i = 0; i < grid.Count; i++)
        //    {
        //        if (grid[i].GetComponent<IstvanRoomGenDelete>().corners == 1 && grid[i].GetComponent<IstvanRoomGenDelete>().isConected)
        //        {
        //            boss.Add(grid[i]);//all that needs to happen is that it sets two rooms with start / end
        //        }
        //    }

        //    var num = Random.Range(0, boss.Count);

        //    boss[num].gameObject.GetComponent<IstvanRoomGenDelete>().isBeegBoss = true;

        //    boss.RemoveAt(num);

        //    var num2 = Random.Range(0, boss.Count);

        //    boss[num2].gameObject.GetComponent<IstvanRoomGenDelete>().start = true;
        //}




        //if(Input.GetKeyDown("q"))
        //{
        //    Delete();
        //}

        //if (Input.GetKeyDown("w"))
        //{
        //    LogDungeon();
        //}
        //if (Input.GetKeyDown("e"))
        //{
        //    RoomChecking();
        //}
        //if (Input.GetKeyDown("r"))
        //{
        //    Log1DoorRooms();
        //}
        //if (Input.GetKeyDown("t"))
        //{
        //    Spawn();
        //}


        //if (Input.GetKeyDown("return"))
        //{
        //    SpawnDungeon();
        //}
    }

    void Delete()
    {
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Sphere").Length; i++)
        {
            maybeDelete.Add(GameObject.FindGameObjectsWithTag("Sphere")[i]);
        }

        for (int i = 0; i < maybeDelete.Count; i++)
        {
            maybeDelete[i].GetComponent<IstvanRoomGenDelete>().Delete();
        }
    }

    void RoomChecking()
    {
        for (int i = 0; i < grid.Count; i++)
        {
            grid[i].GetComponent<IstvanRoomGenDelete>().RoomChecking();
        }
        for (int i = 0; i < grid.Count; i++)
        {
            grid[i].GetComponent<IstvanRoomGenDelete>().RoomChecking();
        }
        for (int i = 0; i < grid.Count; i++)
        {
            grid[i].GetComponent<IstvanRoomGenDelete>().RoomChecking();
        }
        for (int i = 0; i < grid.Count; i++)
        {
            grid[i].GetComponent<IstvanRoomGenDelete>().RoomChecking();
        }
        for (int i = 0; i < grid.Count; i++)
        {
            grid[i].GetComponent<IstvanRoomGenDelete>().RoomChecking();
        }
        for (int i = 0; i < grid.Count; i++)
        {
            grid[i].GetComponent<IstvanRoomGenDelete>().RoomChecking();
        }
        for (int i = 0; i < grid.Count; i++)
        {
            grid[i].GetComponent<IstvanRoomGenDelete>().RoomChecking();
        }
        for (int i = 0; i < grid.Count; i++)
        {
            grid[i].GetComponent<IstvanRoomGenDelete>().RoomChecking();
        }
        for (int i = 0; i < grid.Count; i++)
        {
            grid[i].GetComponent<IstvanRoomGenDelete>().RoomChecking();
        }
    }

    void Spawn()
    {
        for (int i = 0; i < grid.Count; i++)
        {
            grid[i].GetComponent<IstvanRoomGenDelete>().Spawn();
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

    bool Log1DoorRooms()
    {
        for (int i = 0; i < grid.Count; i++)
        {
            if (grid[i].GetComponent<IstvanRoomGenDelete>().corners == 1 && grid[i].GetComponent<IstvanRoomGenDelete>().isConected)
            {
                boss.Add(grid[i]);//all that needs to happen is that it sets two rooms with start / end
            }
        }

        
        //var num = Random.Range(0, boss.Count);

        //boss[num].gameObject.GetComponent<IstvanRoomGenDelete>().isBeegBoss = true;

        //boss.RemoveAt(num);

        //var num2 = Random.Range(0, boss.Count);

        //boss[num2].gameObject.GetComponent<IstvanRoomGenDelete>().start = true;

        if (boss.Count >= 2)
        {
            Debug.Log("log1 if working");
            var num = Random.Range(0, boss.Count);

            boss[num].gameObject.GetComponent<IstvanRoomGenDelete>().isBeegBoss = true;

            boss.RemoveAt(num);

            var num2 = Random.Range(0, boss.Count);

            boss[num2].gameObject.GetComponent<IstvanRoomGenDelete>().start = true;
            return true;
        }
        else
        {
            return false;
        }
    }

    //public bool delete = false;
    //public bool check = false;
    //public bool spawn = false;

    //public bool logDungeon = false;
    //public bool log1Rooms = false;

    //public float timeToCount;
   

    //    delete = false;
    //    if (delete == true & !logDungeon)
    //    {
    //       LogDungeon();
    //       logDungeon = true;
    //    }
    //    if (check == true & !log1Rooms)
    //    {
    //       Log1DoorRooms();
    //       log1Rooms = true;
    //    }
    //    if (spawn == false)
    //    {
    //       spawn = true;
    //    }

    // 'k'(delete grid objs)
    // ';'(adds all remaining grid objs to)
    // 'j'(checks what rooms to make and if they are connected)
    // '/'(adds all rooms with 1 room to a list so that two can be picked for start and end)
    // 'h'(spawns the rooms and sets corners may need to swap the coners setting to before spawning to get the things working properly)
}