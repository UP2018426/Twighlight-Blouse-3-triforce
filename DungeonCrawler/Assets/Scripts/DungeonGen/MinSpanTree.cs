using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinSpanTree : MonoBehaviour
{
    public GameObject[] points;

    //public Stack<GameObject> reached;
    //public GameObject[] reached;
    public List<GameObject> reached;
    public List<GameObject> unReached;//may need to be changed to a stack or list



    //start at point add point to reached then add closest point to first point to reached

    //now check point that is closest toeither point in reached

    RoomGen roomGen;

    private void Awake()
    {
        roomGen = GameObject.FindGameObjectWithTag("Respawn").GetComponent<RoomGen>();
    }

    void Start()
    {
        unReached = new List<GameObject>();


        for (int i = 0; i < roomGen.Mainrooms.Count; i++)
        {
            unReached.Add(roomGen.Mainrooms.ToArray()[i]);
        } 

        reached.Add(unReached[0]);
        unReached.RemoveRange(0,1);

        //while(unReached.Count > 0)
        //{
        //    var record = 1000f;
        //    var rIndex = 0;
        //    var uIndex = 0;

        //    for (int i = 0; i < reached.Count; i++)
        //    {
        //        for (int j = 0; j < unReached.Count; j++)
        //        {
                    

        //            var v1 = reached.ToArray()[i];
        //            var v2 = unReached.ToArray()[j];
        //            var d = dist(v1.transform.position.x,v1.transform.position.z,v2.transform.position.x,v2.transform.position.z);

        //            if(d < record)
        //            {
        //                record = d;
        //                rIndex = i;
        //                uIndex = j;
        //            }
        //        }
        //    }
            
        //    Debug.DrawLine(new Vector3(reached.ToArray()[rIndex].transform.position.x, 0, reached.ToArray()[rIndex].transform.position.z), new Vector3(unReached[uIndex].transform.position.x, 0, unReached[uIndex].transform.position.z),Color.white,10000);

        //    reached.Push(unReached[uIndex]);
        //    unReached.RemoveRange(uIndex,1);
        //}
    }

    private void Update()
    {

        //if (timer)
        //{
            Tree();
        //}
        //if()
        //{
        //    if (unReached.Count > 0)
        //    {
        //        unReached = new List<GameObject>();
        //        for (int i = 0; i < points.Length; i++)
        //        {
        //            unReached.Add(points[i]);
        //        }
        //    }
        //}

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



            reached.Add(unReached[uIndex]);
            unReached.RemoveRange(uIndex, 1);
        }
    }


    float dist(float x1, float y1, float x2, float y2)
    {
        return Mathf.Sqrt(Mathf.Pow((x2 - x1),2) + Mathf.Pow((y2 - y1), 2));
    }
}
