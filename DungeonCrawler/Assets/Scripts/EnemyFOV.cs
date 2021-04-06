using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class EnemyFOV : MonoBehaviour
{
    private float distance;
    [SerializeField]
    public float range;
    [Range(0,360)]
    public float FOV;

    private Vector3 direction;

    private Transform player;

    [SerializeField]
    private float angleToPlayer;

    [SerializeField]
    private bool inRange;
    [SerializeField]
    private bool inFOV;


    [Range(0f, 100f)]
    public float DetectionLevel;

    [SerializeField]
    private int State; // 1 = CALM ||| 2 = SUS ||| 3 = FIGHT ||| 4 = SEARCH

    void Start()
    {
        player = GameObject.Find("CamPos").GetComponent<Transform>();

        //StartCoroutine("Findplayer",0f);

        DetectionLevel = 0f;

        State = 1;
    }

    void Update()
    {
        inFOV = false;
        inRange = false;

        direction = player.transform.position - transform.position;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit))
        {
            if (hit.distance < range)
            {
                inRange = true;
            }

            if (hit.collider.tag == ("Player"))
            {
                if (angleToPlayer <= FOV / 2)
                {
                    inFOV = true;
                }
            }
        }



        //Debug.Log(hit.collider.tag);
        angleToPlayer = Vector3.Angle(direction.normalized, transform.forward);

        if (inFOV && inRange == true && DetectionLevel < 100)
        {
            Debug.DrawRay(transform.position, direction, Color.green);
            //Debug.Log("Can be seen");

            DetectionLevel += (5f * (range - hit.distance) * Time.deltaTime);
        }

        if (inFOV == false && DetectionLevel > 0)
        {
            DetectionLevel -= (5f * Time.deltaTime);
        }

        if(DetectionLevel >= 100f)
        {
            Debug.Log("FIGHT");
            State = 3;
        }
        
        if(DetectionLevel <= 70f && State == 3)
        {
            Debug.Log("SEARCH");
            State = 4;
            
        }
        
        if(DetectionLevel >= 10f)
        {
            Debug.Log("SUS");
            State = 2;
        }

    }
}

    /*IEnumerator Findplayer(float delay)
    {
        while(true)
        {
            yield return new WaitForSeconds(delay);
            Look();
        }
    }


    void Look()
    {
        inFOV = false;
        inRange = false;

        direction = player.transform.position - transform.position;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit))
        {
            if (hit.distance < range)
            {
                inRange = true;
            }

            if (hit.collider.tag == ("Player"))
            {
                if (angleToPlayer <= FOV / 2 )
                {
                    inFOV = true;
                }
            }
        }



        //Debug.Log(hit.collider.tag);
        angleToPlayer = Vector3.Angle(direction.normalized, transform.forward);

        if (inFOV && inRange == true)
        {
            Debug.DrawRay(transform.position, direction, Color.green);
            Debug.Log("Can be seen");

            DetectionLevel += (0.1f * Time.deltaTime);
        }

        if (inFOV == false)
        {
            DetectionLevel -= (0.1f * Time.deltaTime);
        }
    }
}*/

