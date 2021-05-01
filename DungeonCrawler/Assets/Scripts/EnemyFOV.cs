using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class EnemyFOV : MonoBehaviour
{
    //public float distance;
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
    public int FOVState; // 1 = CALM ||| 2 = SUS ||| 3 = FIGHT ||| 4 = SEARCH

    public PlayerController PlayerController;
    
    public bool IsCrouching;
    [Range(1f,20f)]
    public float standingMultiplier;

    private EnemyNav NavScript;

    public Image mark;

    void Start()
    {
        player = GameObject.Find("CamPos").GetComponent<Transform>();
        PlayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();


        NavScript = this.gameObject.GetComponent<EnemyNav>();

        //StartCoroutine("Findplayer",0f);

        DetectionLevel = 0f;

        FOVState = 1;
    }

    void Update()
    {
        mark.fillAmount = DetectionLevel / 100;

        inFOV = false;
        inRange = false;

        direction = player.transform.position - transform.position;

        IsCrouching = PlayerController.isCrouching;

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



        Debug.Log(hit.collider.tag);
        angleToPlayer = Vector3.Angle(direction.normalized, transform.forward);

        if (inFOV && inRange == true && DetectionLevel < 100)
        {
            Debug.DrawRay(transform.position, direction, Color.green);
            Debug.Log("Can be seen");

            if(IsCrouching == false)
            {
                DetectionLevel += (2f * standingMultiplier * (range - hit.distance) * Time.deltaTime);
            }

            if (IsCrouching == true)
            {
                DetectionLevel += (2f * (range - hit.distance) * Time.deltaTime);
            }
            
        }

        if (inFOV == false && DetectionLevel > 0)
        {
            DetectionLevel -= (5f * Time.deltaTime);
        }

        if(DetectionLevel >= 100f && FOVState != 3)
        {
            Debug.Log("FIGHT");
            FOVState = 3;
        }
        
        /*if(DetectionLevel <= 70f && FOVState == 3 && FOVState != 4)
        {
            Debug.Log("SEARCH");
            FOVState = 4;
            
        }*/

        if (DetectionLevel >= 10f && DetectionLevel < 100f && FOVState != 2)
        {
            Debug.Log("SUS");
            FOVState = 2;
        }

        if(DetectionLevel < 10f && FOVState != 1)
        {
            FOVState = 1;
        }
        
        if(DetectionLevel > 100)
        {
            DetectionLevel = 100;
        }
        else if(DetectionLevel < 0)
        {
            DetectionLevel = 0;
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