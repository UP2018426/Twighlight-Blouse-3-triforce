using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RoomGen : MonoBehaviour
{
    [System.Serializable]
    public class Room
    {
        public float width;
        public float length;
        public float height;
    }
    public Room room;

    public RoomOverlapCheck roomOverlapCheck;

    public float radius;

    public float minCircleSize;
    public float maxCircleSize;

    public int numRoomsToGen;


    public int tileSize;


    public List<GameObject> rooms;


    public float minMainRoomsSize;

    bool run = false;
    void Awake()
    {
        for (int i = 0; i < numRoomsToGen; i++)
        {
            CreateRoom(Random.Range(1, 4), Random.Range(1, 4), room.height);
        }
    }

    public void CreateRoom(float _width, float _length, float _height)
    {
        GameObject roomFloor = GameObject.CreatePrimitive(PrimitiveType.Cube);

        roomFloor.AddComponent<Rigidbody>();
        //roomFloor.AddComponent<BoxCollider>();

        //roomFloor.GetComponent<Rigidbody>().isKinematic = true;
        roomFloor.GetComponent<Rigidbody>().useGravity = false;
        roomFloor.GetComponent<Rigidbody>().freezeRotation = true;
        roomFloor.GetComponent<Rigidbody>().drag = 10f;
        //roomFloor.GetComponent<MeshCollider>().convex = true;
        //roomFloor.GetComponent<MeshCollider>().isTrigger = true;
        //roomFloor.GetComponent<BoxCollider>().isTrigger = true;

        roomFloor.AddComponent<RoomOverlapCheck>();

        roomFloor.tag = "Room";




        rooms.Add(roomFloor.gameObject);

        roomFloor.transform.localScale = new Vector3(_width, _height, _length);
        //roomFloor.transform.position = new Vector3(GetRandomPointInCircle(radius).x, 0f, GetRandomPointInCircle(radius).y);
        roomFloor.transform.position = new Vector3(GetRandomPointInEllipse(30, 7).x, 0f, GetRandomPointInEllipse(30, 7).y);
    }



    //picks a random point in a cricle that is then used as an initial spawn point for a room
    Vector2 GetRandomPointInCircle(float _radius)
    {
        float t = 2 * Mathf.PI * Random.Range(0f, 1f);
        float u = Random.Range(0f, 1f) + Random.Range(0f, 1f);
        float r = _radius * Mathf.Sqrt(Random.Range(0f, 1f));
        if (u > 1)
        {
            r = 2 - u;
        }
        else
        {
            r = u;
        }
        Vector2 randcriclepoint = new Vector2(_radius * r * Mathf.Cos(t), _radius * r * Mathf.Sin(t));
        randcriclepoint.x = roundm(randcriclepoint.x, tileSize);
        randcriclepoint.y = roundm(randcriclepoint.y, tileSize);
        return randcriclepoint;
    }

    Vector2 GetRandomPointInEllipse(float ellipse_width, float ellipse_height)
    {
        float t = 2 * Mathf.PI * Random.Range(0f, 1f);
        float u = Random.Range(0f, 1f) + Random.Range(0f, 1f);
        float r = 0;

        if (u > 1)
        {
            r = 2 - u;
        }
        else
        {
            r = u;
        }

        Vector2 randElipsePoint = new Vector2(roundm(ellipse_width * r * Mathf.Cos(t) / 2, tileSize), roundm(ellipse_height * r * Mathf.Sin(t) / 2, tileSize));

        randElipsePoint.x = roundm(randElipsePoint.x, tileSize);
        randElipsePoint.y = roundm(randElipsePoint.y, tileSize);

        return randElipsePoint;
    }




    //used to round the positions of the grid within the circle to they are in line
    float roundm(float n, float m)
    {
        return Mathf.Floor(((n + m - 1) / m)) * m;
    }

    void Spread()
    {
        for (int i = 0; i < Mainrooms.Count; i++)
        {
            Mainrooms[i].transform.Translate(roundm(transform.position.x, tileSize), 0, roundm(transform.position.z, tileSize));
        }
    }


    public float countDown;

    public List<GameObject> Mainrooms;

    bool run2 = false;

    public List<GameObject> reached;
    public List<GameObject> unReached;

    private void Update()
    {
        if (Input.GetKeyDown("l"))
        {
            Spread();
        }

        countDown -= Time.deltaTime;
        countDown = Mathf.Clamp(countDown, 0f, Mathf.Infinity);

        if (run2 == false)
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                if (rooms[i].GetComponent<RoomOverlapCheck>().mainRoom == true)
                {
                    Mainrooms.Add(rooms[i]);
                }
            }
            for (int i = 0; i < Mainrooms.Count; i++)
            {
                Mainrooms[i].GetComponent<RoomOverlapCheck>().roomCode = "R" + i.ToString();
            }

            run2 = true;
        }


        Tree();

        if (countDown <= 0 && !run)
        {
            unReached = new List<GameObject>();


            for (int i = 0; i < Mainrooms.Count; i++)
            {
                unReached.Add(Mainrooms.ToArray()[i]);
            }

            reached.Add(unReached[0]);
            unReached.RemoveRange(0, 1);

            for (int i = 0; i < rooms.Count; i++)
            {
                if (rooms[i].GetComponent<RoomOverlapCheck>().mainRoom == false)
                {
                    Destroy(rooms[i]);
                }
            }
            run = true;
        }
    }

    void Tree()
    {
        if (unReached.Count > 0)
        {
            var record = 1000f;
            var rIndex = 0;
            var uIndex = 0;

            for (int i = 0; i < reached.Count; i++)
            {
                for (int j = 0; j < unReached.Count; j++)
                {


                    var v1 = reached[i];
                    var v2 = unReached.ToArray()[j];
                    var d = dist(v1.transform.position.x, v1.transform.position.z, v2.transform.position.x, v2.transform.position.z);

                    if (d < record)
                    {
                        record = d;
                        rIndex = i;
                        uIndex = j;
                    }
                } 
            }
            
            Debug.DrawLine(new Vector3(reached[rIndex].transform.position.x, 0, reached[rIndex].transform.position.z), new Vector3(unReached[uIndex].transform.position.x, 0, unReached[uIndex].transform.position.z), Color.white, 10000);

            GameObject coridor = GameObject.CreatePrimitive(PrimitiveType.Cube);

            coridor.transform.localScale = new Vector3(1,1,1);
            coridor.transform.position = VectorMidPoint(reached[rIndex].transform.position, unReached[uIndex].transform.position);

            //need to get distance between points and figure out how to scale the corridors to fit that space
            //need to gen coridoors in two parts by getting room pos I want to puut coridoor from traveling in x to reach that height then duplicate for z

            //if corridor overlaps room delete it


            reached.Add(unReached[uIndex]);
            unReached.RemoveRange(uIndex, 1);
        }
    }


    //add the array stuff here

    //need to get current reached that is being checked and the unreached it is connecting to

    //for (int i = 0; i < Mainrooms.Count; i++)
    //{
    //    if (Mainrooms[i] == reached[i])
    //    {
    //        Mainrooms[i].GetComponent<RoomOverlapCheck>().conectedTo.Add(unReached[uIndex]);
    //    }
    //}

    //treeConections[rIndex][uIndex] = unReached[uIndex];

    //roomOverlapCheck.conectedTo.Add(unReached[uIndex]);

    //instead just put new shape as place holder that gets to the end pos


    //public GameObject[][] treeConections;//use first part of aray to store all rooms then use the array in that to store the rooms it connects to

    float dist(float x1, float y1, float x2, float y2)
    {
        return Mathf.Sqrt(Mathf.Pow((x2 - x1),2) + Mathf.Pow((y2 - y1), 2));
    }

    Vector3 VectorMidPoint(Vector3 P1, Vector3 P2)
    {
        return ((P1 + P2 )/ 2);
    }
}