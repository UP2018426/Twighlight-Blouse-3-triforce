using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;


public class JARVIS : MonoBehaviour
{
    public int numPoints;
    public int minSpawn;
    public int maxSpawn;

    public GameObject prefab;

    public GameObject[] points;
    public GameObject[] hull;

    public List<GameObject> pnts;

    public Stack<GameObject> hullStack;

    public GameObject leftMost;
    public GameObject currentVertex;
    int index = 0;
    int nextIndex = -1;
    public GameObject nextVertex;

    void CreatePoints(int _numPoints, float _minSpawn, float _maxSpawn)
    {
        points = new GameObject[_numPoints];
        
        for (int i = 0; i < _numPoints; i++)
        {
            pnts.Add(null);
            points[i] = Instantiate(prefab, new Vector3(Random.Range(_minSpawn, _maxSpawn), 0, Random.Range(_minSpawn, _maxSpawn)), Quaternion.identity);
            //pnts[i] = points[i];

            
        }

        
        //pnts = pnts.OrderBy((pnts) => pnts.transform.position.x).ToList();
        PointsOrder();
    }


    
    void PointsOrder()//bubble sort
    {
                
        GameObject pnt;
        
        foreach (GameObject point in points)
        {
            for (int p = 0; p <= points.Length - 2; p++)
            {
                for (int i = 0; i <= points.Length - 2; i++)
                {
                    if (points[i].transform.position.x > points[i + 1].transform.position.x)
                    {
                        pnt = points[i + 1];
                        points[i + 1] = points[i];
                        points[i] = pnt;
                    }
                }
            }
        }


        //GameObject pont;
        //foreach (GameObject point in pnts)
        //{
        //    for(int p = 0; p <= pnts.Count - 2; p++)
        //    {
        //        for (int i = 0; i <= pnts.Count - 2; i++)
        //        {
        //            if(pnts[i].transform.position.x > pnts[i + 1].transform.position.x)
        //            {
        //                pont = pnts[i + 1];
        //                pnts[i + 1] = pnts[i];
        //                pnts[i] = pont;
        //            }
        //        }
        //    }
        //}

    }
    

    int CrossProduct(Vector3 a, Vector3 b, Vector3 c)
    {
        float area = (b.x - a.x) * (c.z - a.z) - (b.z - a.z) * (c.x - a.x);
        
        if(area < 0)
        {
            return -1;
        }
        else if(area > 0)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }


    private void Start()
    {
        hullStack = new Stack<GameObject>();

        CreatePoints(numPoints,minSpawn,maxSpawn);

        //for (int i = 0; i < numPoints; i++)
        //{
        //   GenerateConvexHull();
            //hullStack.CopyTo(hull, i);
            
        //}

        while(!GenerateConvexHull())
        {
            GenerateConvexHull();
            break;
        }

        for (int i = 0; i < hullStack.Count; i++)
        {
            if(i < hullStack.Count)
            {
                Debug.DrawLine(hullStack.ElementAt(i).transform.position, hullStack.ElementAt(i + 1).transform.position, Color.green, 100f);
            }
            else if(i == hullStack.Count)
            {
                Debug.DrawLine(hullStack.ElementAt(i).transform.position, hullStack.ElementAt(0).transform.position, Color.green, 100f);
            }
            
        }
        
        /*
        leftMost = pnts[0];
        currentVertex = leftMost;
        hullStack.Push(currentVertex);

        nextVertex = pnts[1];

        index = 2;

        var checking = points[index];

        Vector3 a = nextVertex.transform.position - currentVertex.transform.position;
        Vector3 b = checking.transform.position - currentVertex.transform.position;

        var cross = Vector3.Cross(a,b);

        if(cross.x < 0 && cross.z < 0)
        {
            nextVertex = checking;
            nextIndex = index;
        }
        index = index + 1;
        if (index == points.Length)
        {
            if (nextVertex == leftMost)
            {
                //return true;
            }

            hullStack.Push(nextVertex);
            currentVertex = nextVertex;
            index = 0;

            //Splice<GameObject>(pnts,nextIndex,1);

            nextVertex = leftMost;
        }
        */
    }

    bool GenerateConvexHull()
    {
        bool finishedHull = false;

        leftMost = points[0];
        currentVertex = leftMost;
        hullStack.Push(currentVertex);

        nextVertex = points[1];

        index = 2;

        var checking = points[index];

        Vector3 a = nextVertex.transform.position - currentVertex.transform.position;
        Vector3 b = checking.transform.position - currentVertex.transform.position;

        var cross = Vector3.Cross(a, b);

        if (cross.x < 0 && cross.z < 0)
        {
            nextVertex = checking;
            nextIndex = index;
        }
        index = index + 1;
        if (index == points.Length)
        {
            if (nextVertex == leftMost)
            {
                finishedHull = true;
            }

            hullStack.Push(nextVertex);
            //hull[i] = nextVertex; 
            currentVertex = nextVertex;
            index = 0;

            //Splice<GameObject>(pnts,nextIndex,1);

            nextVertex = leftMost;
        }
        return finishedHull;
    }

    public static List<T> Splice<T>(List<T> source, int index, int count)
    {
        var items = source.GetRange(index, count);
        source.RemoveRange(index, count);
        return items;
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < hull.Length; i++)
        {
            Gizmos.DrawLine(hullStack.ElementAt(i) .transform.position,hullStack.ElementAt(i + 1).transform.position);
        }


        //Gizmos.DrawLine(currentVertex.transform.position, nextVertex.transform.position);
        //need to draw gizmo lines between hull points
    }
}