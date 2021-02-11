using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//https://www.habrador.com/tutorials/math/ where I found the tutorial

namespace LinearAlgebra
{ 

    //determines whether a pooint is infrot or behind and initial point
    public class InFrontOrBehind : MonoBehaviour
    {
        public Transform pointA;//Point1position(Current)
        public Transform pointB;//Point2position(Other)

        private void Update()
        {
            
            Vector3 forwardVectorA = pointA.forward;
            
            Vector3 VectorAToB = pointB.position - pointA.position;

            float dotProduct = Vector3.Dot(forwardVectorA, VectorAToB);//gets the diference back as a whole number(1 || -1)

            if(dotProduct < 0f)
            {
                Debug.Log("In Front");
            }
            else if(dotProduct > 0f)
            {
                Debug.Log("Behind");
            }
        }



    }

    //determines wheter a point is left or right of an initial point
    public class LeftOrRight : MonoBehaviour
    {
        public Transform pointA;//Point1position(Current)
        public Transform pointB;//Point1position(Other)

        private void Update()
        {
            Vector3 dirPointA = pointA.forward;//finds forward direction for PointA

            Vector3 dirPointB = pointB.position - pointA.position;//gets direction to PointB

            Vector3 crossProduct = Vector3.Cross(dirPointA,dirPointB);

            float dotProduct = Vector3.Dot(crossProduct,pointA.up);

            if (dotProduct < 0f)
            {
                Debug.Log("Turn Left");
            }
            else if (dotProduct > 0f)
            {
                Debug.Log("Turn Right");
            }
        }
    }

    //finds whether you have passed a point or not
    public class PassedPoint : MonoBehaviour
    {
        public Transform pointA;//Point1position(Current)
        public Transform pointB;//Point2position(Point from Current(from))
        public Transform pointC;//Point3position(Curent from Point(to))

        private void Update()
        {
            Vector3 a = pointA.position - pointB.position;

            Vector3 b = pointC.position - pointB.position;
            
            float progress = (a.x * b.x + a.y * b.y + a.z * b.z) / (b.x * b.x + b.y * b.y + b.z * b.z);

            if(progress > 1f)
            {
                Debug.Log("Change Waypoint");
            }
            else if (progress < 1f)
            {
                Debug.Log("Keep Going");
            }
        }
    }

    //manual and visual raycast
    public class PlaneRayIntersection : MonoBehaviour
    {
        public LineRenderer lineRenderer;
        public Transform pointA;
        public Transform planePos;

        private void Update()
        {
            Vector3 p_0 = planePos.position;//gets world space of the plane

            Vector3 n = -planePos.up;//to give the plane a normal(soFacingDir is known needs to be facing down if ray coming from above)

            Vector3 l_0 = pointA.position;//gets world space of point your raycasting from

            Vector3 l = pointA.forward;//direction ray is fired in

            float denominator = Vector3.Dot(l,n);//determines size of ray and helps ensure it doesnt miss

            if (denominator > 0.00000001f)
            {
                float t = Vector3.Dot(p_0 - l_0,n) / denominator;//gets distance form ray origin to intersect point

                Vector3 p = l_0 + l * t;//gets position where intesect occours;

                //draws the raycast
                lineRenderer.SetPosition(0, p);//sets draw point where ray intersects
                lineRenderer.SetPosition(1, l_0);//sets draw point on ray origin
            }
            else
            {
                Debug.Log("Miss");
            }
        }
    }

    //checks if two lines are intersecting with each other
    public class LineInterrsection : MonoBehaviour
    {
        //pointA(start of line 1) pointB(end of line 1)
        public Transform PointA1;
        public Transform PointB1;

        //pointA(start of line 2) pointB(end of line 2)
        public Transform PointA2;
        public Transform PointB2;

        public LineRenderer lineRenderer1;
        public LineRenderer lineRenderer2;

        private void Start()
        {
            lineRenderer1.material = new Material(Shader.Find("UnlitColor"));
            lineRenderer2.material = new Material(Shader.Find("UnlitColor"));
        }

        private void Update()
        {
            //sets where to draw line 1
            lineRenderer1.SetPosition(0,PointA1.position);
            lineRenderer1.SetPosition(1,PointA2.position);

            //sets where to draw line 2
            lineRenderer2.SetPosition(0,PointB1.position);
            lineRenderer2.SetPosition(1,PointB2.position);

            lineRenderer1.material.color = Color.red;//line 1 colour becomes red so can tell difference
            lineRenderer2.material.color = Color.blue;//line 2 colour becomes blue so can tell difference

            if(IsIntersecting())
            {
                lineRenderer1.material.color = Color.magenta;
                lineRenderer2.material.color = Color.magenta;
            }

            IsIntersecting();
        }


        bool IsIntersecting()
        {
            bool isIntersecting = false;

            //3d ==> 2d
            Vector2 l1_start = new Vector2(PointA1.position.x, PointA1.position.z);
            Vector2 l1_end = new Vector2(PointA2.position.x, PointA2.position.z);

            Vector2 l2_start = new Vector2(PointB1.position.x, PointB1.position.z);
            Vector2 l2_end = new Vector2(PointB2.position.x, PointB2.position.z);

            Vector2 l1_dir = (l1_end - l1_start).normalized;//gets direction of line 1
            Vector2 l2_dir = (l2_end - l2_start).normalized;//gets direction of line 2

            Vector2 l1_normal = new Vector2(-l1_dir.y, l1_dir.x);//gets the normal of line 1
            Vector2 l2_normal = new Vector2(-l2_dir.y, l2_dir.x);//gets the normal of line 2

            float a = l1_normal.x;
            float b = l1_normal.y;

            float c = l2_normal.x;
            float d = l2_normal.y;

            float k1 = (a * l1_start.x) + (b * l1_start.y);
            float k2 = (c * l2_start.x) + (d * l2_start.y);

            //returns false if lines are parallel to each other
            if(IsParrallel(l1_normal,l2_normal))
            {
                Debug.Log("Lines are parrallel");
                return isIntersecting;
            }

            //returns false if lines are connected
            if(IsOrthoganal(l1_start - l2_start,l2_normal))
            {
                Debug.Log("Same Line");
                return isIntersecting;
            }

            float xIntercept = (d * k1 - b * k2);//gets the point of intersection on the x
            float yIntercept = (c * k1 - a * k2);//gets the point of intersection on the y

            Vector2 intersectPoint = new Vector2(xIntercept,yIntercept);//gets the intersect as a vector2 location

            if(IsBetween(l1_start,l1_end, intersectPoint) && IsBetween(l2_start, l2_end, intersectPoint))
            {
                Debug.Log("is Intersecting");

                isIntersecting = true;
            }

            return isIntersecting;
        }

        bool IsParrallel(Vector2 _l1_normal, Vector2 _l2_normal)
        {

            //is parralle if the angle between vectors is 180degrees or 0 degress means lines are || in some angle
            if(Vector2.Angle(_l1_normal,_l2_normal) == 0f || Vector2.Angle(_l1_normal,_l2_normal) == 180f)
            {
                return true;
            }
            return false;
        }

        bool IsOrthoganal(Vector2 _l1_normal, Vector2 _l2_normal)
        {
            //is orthoganal if dot product is 0
            if(Mathf.Abs(Vector2.Dot(_l1_normal,_l2_normal)) < 0.000001f)//checks agaisnt small float as dot is returned as a float and is a precautionary measure
            {
                return true;
            }
            return false;
        }

        bool IsBetween(Vector2 _start,Vector2 _end,Vector2 _intersectPoint)
        {
            bool isBetween = false;

            Vector2 ab = _end - _start;//length of full line

            Vector2 ac = _intersectPoint - _start;//length of line between the start and the intercept point

            //if vecotrs facing same direction dot is positive && if length of vector between intersecion and first point is smaller that entire line
            if(Vector2.Dot(ab,ac) > 0f && ab.sqrMagnitude >= ac.sqrMagnitude)
            {
                isBetween = true;
            }

            return isBetween;
        }
    }

}





public class Algerbra : MonoBehaviour
{
    
    

}
