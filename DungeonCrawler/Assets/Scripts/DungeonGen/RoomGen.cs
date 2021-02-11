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

    public float timer;

    public float minMainRoomsSize;

    void Awake()
    {
        for (int i = 0; i < numRoomsToGen; i++)
        {
            CreateRoom(Random.Range(1, 4), Random.Range(1, 4) , room.height);            
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
        roomFloor.transform.position = new Vector3(GetRandomPointInEllipse(30,7).x, 0f, GetRandomPointInEllipse(30,7).y);
    }



    //picks a random point in a cricle that is then used as an initial spawn point for a room
    Vector2 GetRandomPointInCircle(float _radius)
    {
        float t = 2 * Mathf.PI * Random.Range(0f,1f);
        float u = Random.Range(0f,1f) + Random.Range(0f,1f);
        float r = _radius * Mathf.Sqrt(Random.Range(0f,1f));
        if(u > 1)
        {
            r = 2 - u;
        }
        else
        {
            r = u;
        }
        Vector2 randcriclepoint = new Vector2(_radius * r * Mathf.Cos(t), _radius *r*Mathf.Sin(t));
        randcriclepoint.x = roundm(randcriclepoint.x,tileSize);
        randcriclepoint.y = roundm(randcriclepoint.y,tileSize);
        return randcriclepoint;
    }

    Vector2 GetRandomPointInEllipse(float ellipse_width,float ellipse_height)
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

        Vector2 randElipsePoint = new Vector2 (roundm(ellipse_width * r * Mathf.Cos(t) / 2, tileSize), roundm(ellipse_height * r * Mathf.Sin(t) / 2, tileSize));

        randElipsePoint.x = roundm(randElipsePoint.x,tileSize);
        randElipsePoint.y = roundm(randElipsePoint.y,tileSize);

        return randElipsePoint;
    }




    //used to round the positions of the grid within the circle to they are in line
    float roundm(float n,float m)
    {
        return Mathf.Floor(((n + m - 1) / m)) * m;
    }

    void Spread()
    {
        for(int i = 0; i < rooms.Count; i++)
        {
                rooms[i].transform.Translate(roundm(transform.position.x,tileSize),0,roundm(transform.position.z,tileSize));
        }
    }



    private void Update()
    {
        if(Input.GetKeyDown("l"))
        {
            Spread();
        }
    }

}
