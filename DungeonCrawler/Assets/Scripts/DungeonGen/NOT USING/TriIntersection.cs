using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriIntersection : MonoBehaviour
{
    public Transform[] triPoints;

    public GameObject[] triangle;

    public GameObject[] boxes;

    //contains info for how to make tri
    public struct Triangle
    {
        public Vector3 p1, p2, p3;//points of tri

        public LineSegment[] lineSegment;//lines to connect points

        public Triangle(Vector3 _p1, Vector3 _p2, Vector3 _p3)
        {
            p1 = _p1;
            p2 = _p2;
            p3 = _p3;

            lineSegment = new LineSegment[3];

            lineSegment[0] = new LineSegment(p1, p2);
            lineSegment[1] = new LineSegment(p2, p3);
            lineSegment[2] = new LineSegment(p3, p1);
        }
    }
    //contains info for how to make lines between points
    public struct LineSegment
    {
        public Vector3 p1, p2;//start and end coordinates for line

        public LineSegment(Vector3 _p1, Vector3 _p2)
        {
            p1 = _p1;
            p2 = _p2;
        }
    }

    //uses structs above to create a tri
    void CreateTri(Triangle triangle, GameObject triangleObj)
    {
        Vector3[] newVerticies = new Vector3[3];

        newVerticies[0] = triangle.p1;
        newVerticies[1] = triangle.p2;
        newVerticies[2] = triangle.p3;

        int[] newTriangles = new int[6];

        newTriangles[0] = 2;
        newTriangles[1] = 1;
        newTriangles[2] = 0;

        newTriangles[3] = 0;
        newTriangles[4] = 1;
        newTriangles[5] = 2;

        Mesh newMesh = new Mesh();

        newMesh.vertices = newVerticies;
        newMesh.triangles = newTriangles;

        triangleObj.GetComponent<MeshFilter>().mesh = newMesh;
    }




    private void Update()
    {
        Vector3 triP1 = triPoints[0].position;
        Vector3 triP2 = triPoints[1].position;
        Vector3 triP3 = triPoints[2].position;

        Vector3 triP4 = triPoints[3].position;
        Vector3 triP5 = triPoints[4].position;
        Vector3 triP6 = triPoints[5].position;

        Triangle triangle1 = new Triangle(triP1, triP2, triP3);
        Triangle triangle2 = new Triangle(triP4, triP5, triP6);

        CreateTri(triangle1, triangle[0]);
        CreateTri(triangle2, triangle[1]);

        if (IsTriangleIntersecting(triangle1, triangle2))
        {
            triangle[0].GetComponent<MeshRenderer>().material.color = Color.red;
            triangle[1].GetComponent<MeshRenderer>().material.color = Color.red;
        }
        else if (!IsTriangleIntersecting(triangle1, triangle2))
        {
            triangle[0].GetComponent<MeshRenderer>().material.color = Color.blue;
            triangle[1].GetComponent<MeshRenderer>().material.color = Color.blue;
        }

    }
        
    bool IsTriangleIntersecting(Triangle tri1, Triangle tri2)
    {
        bool isIntersecting = false;

        if (IsAABBIntersecting(tri1, tri2))//working
        {
            if (AnyLineSegmentIntersecting(tri1, tri2))
            {
                isIntersecting = true;
            }
            else if (CornerIntersecting(tri1, tri2))
            {
                isIntersecting = true;
            }
        }

        return isIntersecting;
    }

    bool IsAABBIntersecting(Triangle tri1, Triangle tri2)
    {
        float tri1_minX = Mathf.Min(tri1.p1.x, Mathf.Min(tri1.p2.x, tri1.p3.x));
        float tri1_maxX = Mathf.Max(tri1.p1.x, Mathf.Max(tri1.p2.x, tri1.p3.x));
        float tri1_minZ = Mathf.Min(tri1.p1.z, Mathf.Min(tri1.p2.z, tri1.p3.z));
        float tri1_maxZ = Mathf.Max(tri1.p1.z, Mathf.Max(tri1.p2.z, tri1.p3.z));

        float tri2_minX = Mathf.Min(tri2.p1.x, Mathf.Min(tri2.p2.x, tri2.p3.x));
        float tri2_maxX = Mathf.Max(tri2.p1.x, Mathf.Max(tri2.p2.x, tri2.p3.x));
        float tri2_minZ = Mathf.Min(tri2.p1.z, Mathf.Min(tri2.p2.z, tri2.p3.z));
        float tri2_maxZ = Mathf.Max(tri2.p1.z, Mathf.Max(tri2.p2.z, tri2.p3.z));

        bool isIntersecting = true;

        if (tri1_minX > tri2_maxX)
        {
            isIntersecting = false;
        }
        else if (tri2_minX > tri1_maxX)
        {
            isIntersecting = false;
        }
        else if (tri1_minZ > tri2_maxZ)
        {
            isIntersecting = false;
        }
        else if (tri2_minZ > tri1_maxZ)
        {
            isIntersecting = false;
        }
        
        Vector3 BB_1_CenterPos = new Vector3((tri1_maxX - tri1_minX) * 0.5f + tri1_minX, -0.2f, (tri1_maxZ - tri1_minZ) * 0.5f + tri1_minZ);

        boxes[0].transform.position = BB_1_CenterPos;

        boxes[0].transform.localScale = new Vector3(tri1_maxX - tri1_minX, 0.1f, tri1_maxZ - tri1_minZ);

        Vector3 BB_2_CenterPos = new Vector3((tri2_maxX - tri2_minX) * 0.5f + tri2_minX, -0.2f, (tri2_maxZ - tri2_minZ) * 0.5f + tri2_minZ);

        boxes[1].transform.position = BB_2_CenterPos;

        boxes[1].transform.localScale = new Vector3(tri2_maxX - tri2_minX, 0.1f, tri2_maxZ - tri2_minZ);

        return isIntersecting;
    }

    bool AnyLineSegmentIntersecting(Triangle tri1, Triangle tri2)
    {
        bool isIntersecting = false;

        for (int i = 0; i < tri1.lineSegment.Length; i++)
        {
            for (int j = 0; j < tri2.lineSegment.Length; j++)
            {
                Vector3 tri1_p1 = tri1.lineSegment[i].p1;
                Vector3 tri1_p2 = tri1.lineSegment[i].p2;
                Vector3 tri2_p1 = tri2.lineSegment[i].p1;
                Vector3 tri2_p2 = tri2.lineSegment[i].p2;

                if (LineSegmentIntersecting(tri1_p1, tri1_p2, tri2_p1, tri2_p2))
                {
                    isIntersecting = true;
                    
                    i = int.MaxValue - 1;

                    break;
                }
            }
        }

        return isIntersecting;
    }



    bool LineSegmentIntersecting(Vector3 _tri1_p1, Vector3 _tri1_p2, Vector3 _tri2_p1, Vector3 _tri2_p2)//something wrong here
    {
        bool isIntersecting = false;

        float denominator = (_tri2_p2.z - _tri2_p1.z) * (_tri1_p2.x - _tri1_p1.z) - (_tri2_p2.x - _tri2_p1.x) * (_tri1_p2.z - _tri1_p1.z);

        if (denominator != 0)//checks denominator and if denominator is 0 lines are parralell
        {
            float u_a = ((_tri2_p2.x - _tri2_p1.x) * (_tri1_p1.z - _tri2_p1.z) - (_tri2_p2.z - _tri2_p1.z) * (_tri1_p1.x - _tri2_p1.x)) / denominator;
            float u_b = ((_tri1_p2.x - _tri1_p1.x) * (_tri1_p1.z - _tri2_p1.z) - (_tri1_p2.z - _tri1_p1.z) * (_tri1_p1.x - _tri2_p1.x)) / denominator;

            if (u_a >= 0 && u_a <= 1 && u_b >= 0 && u_b <= 1)//if the values are between 0 and 1 its intersecting
            {
                isIntersecting = true;
            }
        }
        //Debug.Log(isIntersecting);
        return isIntersecting;
    }


    bool CornerIntersecting(Triangle tri1, Triangle tri2)
    {
        bool isIntersecting = false;

        if (PointInTri(tri1.p1, tri2.p1, tri2.p2, tri2.p3))
        {
            isIntersecting = true;
        }
        else if (PointInTri(tri2.p1, tri1.p1, tri1.p2, tri1.p3))
        {
            isIntersecting = true;
        }

        return isIntersecting;
    }


    bool PointInTri (Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4)
    {
        bool isInTriangle = false;

        float denominator = ((p3.z - p4.z) * (p2.x - p4.x) + (p4.x - p3.x) * (p2.z - p4.z));

        float a = ((p3.z - p4.z) * (p1.x - p4.x) + (p4.x - p3.x) * (p1.z - p4.z)) / denominator;
        float b = ((p4.z - p2.z) * (p1.x - p4.x) + (p2.x - p4.x) * (p1.z - p4.z)) / denominator;
        float c = 1 - a - b;

        if (a >= 0f && a <= 1f && b >= 0f && b <= 1f && c >= 0f && c <= 1f)
        {
            isInTriangle = true;
        }

        return isInTriangle;
    }
}