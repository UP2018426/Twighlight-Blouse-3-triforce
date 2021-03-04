using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UsefulData
{
    public class Vertex
    {
        public Vector3 position;

        //The outgoing halfedge (a halfedge that starts at this vertex)
        //Doesnt matter which edge we connect to it
        public HalfEdge halfEdge;

        //Which triangle is this vertex a part of?
        public Triangle triangle;

        //The previous and next vertex this vertex is attached to
        public Vertex prevVertex;
        public Vertex nextVertex;

        //Properties this vertex may have
        //Reflex is concave
        public bool isReflex;
        public bool isConvex;
        public bool isEar;

        public Vertex(Vector3 _position)
        {
            position = _position;
        }

        /*
        public Vertex SetPos3D(Vector3 pos)
        {
            position = pos;
            return null;
        }

        public Vector3 GetPos3D()
        {
            return position;
        }
        */

        //Get 2d pos of this vertex
        public Vector2 GetPos2D_XZ()
        {
            Vector2 pos_2d_xz = new Vector2(position.x, position.z);

            return pos_2d_xz;
        }
    }

    public class HalfEdge
    {
        //The vertex the edge points to
        public Vertex v;

        //The face this edge is a part of
        public Triangle t;

        //The next edge
        public HalfEdge nextEdge;
        //The previous
        public HalfEdge prevEdge;
        //The edge going in the opposite direction
        public HalfEdge oppositeEdge;

        //This structure assumes we have a vertex class with a reference to a half edge going from that vertex
        //and a face (triangle) class with a reference to a half edge which is a part of this face 
        public HalfEdge(Vertex v)
        {
            this.v = v;
        }
    }

    public class Triangle
    {
        //Corners
        public Vertex v1;
        public Vertex v2;
        public Vertex v3;

        //If we are using the half edge mesh structure, we just need one half edge
        public HalfEdge halfEdge;

        public Triangle(Vertex v1, Vertex v2, Vertex v3)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
        }

        public Triangle(Vector3 v1, Vector3 v2, Vector3 v3)
        {
            this.v1 = new Vertex(v1);
            this.v2 = new Vertex(v2);
            this.v3 = new Vertex(v3);
        }

        public Triangle(HalfEdge halfEdge)
        {
            this.halfEdge = halfEdge;
        }

        //Change orientation of triangle from cw -> ccw or ccw -> cw
        public void ChangeOrientation()
        {
            Vertex temp = this.v1;

            this.v1 = this.v2;

            this.v2 = temp;
        }
    }

    public class Edge
    {
        public Vertex v1;
        public Vertex v2;

        //Is this edge intersecting with another edge?
        public bool isIntersecting = false;

        public Edge(Vertex v1, Vertex v2)
        {
            this.v1 = v1;
            this.v2 = v2;
        }

        public Edge(Vector3 v1, Vector3 v2)
        {
            this.v1 = new Vertex(v1);
            this.v2 = new Vertex(v2);
        }

        //Get vertex in 2d space (assuming x, z)
        public Vector2 GetVertex2D(Vertex v)
        {
            return new Vector2(v.position.x, v.position.z);
        }

        //Flip edge
        public void FlipEdge()
        {
            Vertex temp = v1;

            v1 = v2;

            v2 = temp;
        }
    }

    public class Plane
    {
        public Vector3 pos;

        public Vector3 normal;

        public Plane(Vector3 pos, Vector3 normal)
        {
            this.pos = pos;

            this.normal = normal;
        }
    }


///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public class UsefulAlgorithms
    {
        public static float IsAPointLeftOfVectorOrOnTheLine(Vector2 a, Vector2 b, Vector2 p)
        {
            float determinant = (a.x - p.x) * (b.y - p.y) - (a.y - p.y) * (b.x - p.x);

            return determinant;
        }
        public static int ClampListIndex(int index, int listSize)
        {
            index = ((index % listSize) + listSize) % listSize;

            return index;
        }

        public static bool IsTriangleOrientedClockwise(Vector2 p1, Vector2 p2, Vector2 p3)
        {
            bool isClockWise = true;

            float determinant = p1.x * p2.y + p3.x * p1.y + p2.x * p3.y - p1.x * p3.y - p3.x * p2.y - p2.x * p1.y;

            if (determinant > 0f)
            {
                isClockWise = false;
            }

            return isClockWise;
        }

        public static bool IsPointInTriangle(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p)
        {
            bool isWithinTriangle = false;

            //Based on Barycentric coordinates
            float denominator = ((p2.y - p3.y) * (p1.x - p3.x) + (p3.x - p2.x) * (p1.y - p3.y));

            float a = ((p2.y - p3.y) * (p.x - p3.x) + (p3.x - p2.x) * (p.y - p3.y)) / denominator;
            float b = ((p3.y - p1.y) * (p.x - p3.x) + (p1.x - p3.x) * (p.y - p3.y)) / denominator;
            float c = 1 - a - b;

            //The point is within the triangle or on the border if 0 <= a <= 1 and 0 <= b <= 1 and 0 <= c <= 1
            //if (a >= 0f && a <= 1f && b >= 0f && b <= 1f && c >= 0f && c <= 1f)
            //{
            //    isWithinTriangle = true;
            //}

            //The point is within the triangle
            if (a > 0f && a < 1f && b > 0f && b < 1f && c > 0f && c < 1f)
            {
                isWithinTriangle = true;
            }

            return isWithinTriangle;
        }

        public static bool AreLinesIntersecting(Vector2 l1_p1, Vector2 l1_p2, Vector2 l2_p1, Vector2 l2_p2, bool shouldIncludeEndPoints)
        {
            bool isIntersecting = false;

            float denominator = (l2_p2.y - l2_p1.y) * (l1_p2.x - l1_p1.x) - (l2_p2.x - l2_p1.x) * (l1_p2.y - l1_p1.y);

            //Make sure the denominator is > 0, if not the lines are parallel
            if (denominator != 0f)
            {
                float u_a = ((l2_p2.x - l2_p1.x) * (l1_p1.y - l2_p1.y) - (l2_p2.y - l2_p1.y) * (l1_p1.x - l2_p1.x)) / denominator;
                float u_b = ((l1_p2.x - l1_p1.x) * (l1_p1.y - l2_p1.y) - (l1_p2.y - l1_p1.y) * (l1_p1.x - l2_p1.x)) / denominator;

                //Are the line segments intersecting if the end points are the same
                if (shouldIncludeEndPoints)
                {
                    //Is intersecting if u_a and u_b are between 0 and 1 or exactly 0 or 1
                    if (u_a >= 0f && u_a <= 1f && u_b >= 0f && u_b <= 1f)
                    {
                        isIntersecting = true;
                    }
                }
                else
                {
                    //Is intersecting if u_a and u_b are between 0 and 1
                    if (u_a > 0f && u_a < 1f && u_b > 0f && u_b < 1f)
                    {
                        isIntersecting = true;
                    }
                }

            }

            return isIntersecting;
        }


        public static Vector2 GetLineLineIntersectionPoint(Vector2 l1_p1, Vector2 l1_p2, Vector2 l2_p1, Vector2 l2_p2)
        {
            float denominator = (l2_p2.y - l2_p1.y) * (l1_p2.x - l1_p1.x) - (l2_p2.x - l2_p1.x) * (l1_p2.y - l1_p1.y);

            float u_a = ((l2_p2.x - l2_p1.x) * (l1_p1.y - l2_p1.y) - (l2_p2.y - l2_p1.y) * (l1_p1.x - l2_p1.x)) / denominator;

            Vector2 intersectionPoint = l1_p1 + u_a * (l1_p2 - l1_p1);

            return intersectionPoint;
        }

        public static float DistanceFromPointToPlane(Vector3 planeNormal, Vector3 planePos, Vector3 pointPos)
        {
            //Positive distance denotes that the point p is on the front side of the plane 
            //Negative means it's on the back side
            float distance = Vector3.Dot(planeNormal, pointPos - planePos);

            return distance;
        }


        public static Vector3 GetRayPlaneIntersectionCoordinate(Vector3 planePos, Vector3 planeNormal, Vector3 rayStart, Vector3 rayDir)
        {
            float denominator = Vector3.Dot(-planeNormal, rayDir);

            Vector3 vecBetween = planePos - rayStart;

            float t = Vector3.Dot(vecBetween, -planeNormal) / denominator;

            Vector3 intersectionPoint = rayStart + rayDir * t;

            return intersectionPoint;
        }

        public static bool AreLinePlaneIntersecting(Vector3 planeNormal, Vector3 planePos, Vector3 linePos1, Vector3 linePos2)
        {
            bool areIntersecting = false;

            Vector3 lineDir = (linePos1 - linePos2).normalized;

            float denominator = Vector3.Dot(-planeNormal, lineDir);

            //No intersection if the line and plane are parallell
            if (denominator > 0.000001f || denominator < -0.000001f)
            {
                Vector3 vecBetween = planePos - linePos1;

                float t = Vector3.Dot(vecBetween, -planeNormal) / denominator;

                Vector3 intersectionPoint = linePos1 + lineDir * t;

                if (IsPointBetweenPoints(linePos1, linePos2, intersectionPoint))
                {
                    areIntersecting = true;
                }
            }

            return areIntersecting;
        }

        public static bool IsPointBetweenPoints(Vector3 a, Vector3 b, Vector3 c)
        {
            bool isBetween = false;

            //Entire line segment
            Vector3 ab = b - a;
            //The intersection and the first point
            Vector3 ac = c - a;

            //Need to check 2 things: 
            //1. If the vectors are pointing in the same direction = if the dot product is positive
            //2. If the length of the vector between the intersection and the first point is smaller than the entire line
            if (Vector3.Dot(ab, ac) > 0f && ab.sqrMagnitude >= ac.sqrMagnitude)
            {
                isBetween = true;
            }

            return isBetween;
        }

        public static Vector3 GetLinePlaneIntersectionCoordinate(Vector3 planeNormal, Vector3 planePos, Vector3 linePos1, Vector3 linePos2)
        {
            Vector3 vecBetween = planePos - linePos1;

            Vector3 lineDir = (linePos1 - linePos2).normalized;

            float denominator = Vector3.Dot(-planeNormal, lineDir);

            float t = Vector3.Dot(vecBetween, -planeNormal) / denominator;

            Vector3 intersectionPoint = linePos1 + lineDir * t;

            return intersectionPoint;
        }

        public static bool IsPointInPolygon(List<Vector2> polygonPoints, Vector2 point)
        {
            //Step 1. Find a point outside of the polygon
            //Pick a point with a x position larger than the polygons max x position, which is always outside
            Vector2 maxXPosVertex = polygonPoints[0];

            for (int i = 1; i < polygonPoints.Count; i++)
            {
                if (polygonPoints[i].x > maxXPosVertex.x)
                {
                    maxXPosVertex = polygonPoints[i];
                }
            }

            //The point should be outside so just pick a number to make it outside
            Vector2 pointOutside = maxXPosVertex + new Vector2(10f, 0f);

            //Step 2. Create an edge between the point we want to test with the point thats outside
            Vector2 l1_p1 = point;
            Vector2 l1_p2 = pointOutside;

            //Step 3. Find out how many edges of the polygon this edge is intersecting
            int numberOfIntersections = 0;

            for (int i = 0; i < polygonPoints.Count; i++)
            {
                //Line 2
                Vector2 l2_p1 = polygonPoints[i];

                int iPlusOne = ClampListIndex(i + 1, polygonPoints.Count);

                Vector2 l2_p2 = polygonPoints[iPlusOne];

                //Are the lines intersecting?
                if (AreLinesIntersecting(l1_p1, l1_p2, l2_p1, l2_p2, true))
                {
                    numberOfIntersections += 1;
                }
            }

            //Step 4. Is the point inside or outside?
            bool isInside = true;

            //The point is outside the polygon if number of intersections is even or 0
            if (numberOfIntersections == 0 || numberOfIntersections % 2 == 0)
            {
                isInside = false;
            }

            return isInside;
        }
    }

    
}





public class DataStructures : MonoBehaviour
{

}
