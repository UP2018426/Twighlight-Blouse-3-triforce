using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using System.Linq;

public class ConvexHull : MonoBehaviour
{
    //giftwrapping method (takes a starting point then checks all points to see which point is furthest to the left of curtrent point(until start point reached))

    //pick leftmost point of points
    //Vector2 startingpoint;

    //list of points
    //Vector2[] points;

    //void TestPoints(points to gen)
    //{

    //}

    /*
    public static class JarvisMarch
    {
        public static List<Vertex> GetConvexHull(List<Vertex> points)
        {
            if (points.Count == 3)
            {
                return points;
            }
            if (points.Count < 3)
            {
                return null;
            }

            List<Vertex> convexHull = new List<Vertex>();

            Vertex startVertex = points[0];


            Vector3 startPos = startVertex.position;

            for(int i = 0; i < points.Count; i++)
            {
                Vector3 testPos = points[i].position;

                if(testPos.x < startPos.x || (Mathf.Approximately(testPos.x , startPos.x)) && testPos.z < startPos.z)
                {
                    startVertex = points[i];

                    startPos = startVertex.position;
                }
            }

            convexHull.Add(startVertex);

            points.Remove(startVertex);

            Vertex currentPoint = convexHull[0];

            List<Vertex> colinearPoints = new List<Vertex>();

            int counter = 0;

            while (true)
            {
                if (counter == 2)
                {
                    points.Add(convexHull[0]);
                }

                Vertex
            }
        }

    }
    */
}
