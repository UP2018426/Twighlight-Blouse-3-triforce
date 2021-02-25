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

    private GameObject player;

    [SerializeField]
    private float angleToPlayer;

    [SerializeField]
    private bool inRange;
    [SerializeField]
    private bool inFOV;

    public GameObject sliderObj; //delete
    public Slider slider; //delete

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        slider = sliderObj.GetComponent<Slider>(); //delete

        inFOV = false;
        inRange = false;

        direction = player.transform.position - transform.position;

        RaycastHit hit;
        if(Physics.Raycast(transform.position, direction, out hit))
        {
            if (hit.distance < range)
            {
                inRange = true;
            }

            if(hit.collider.tag == ("Player"))
            {
                if(angleToPlayer <= FOV / 2 /*|| angleToPlayer >= FOV / 2*/)
                {
                    inFOV = true;
                }
            }
        }

        

        Debug.Log(hit.collider.tag);
        angleToPlayer = Vector3.Angle(direction.normalized, transform.forward);

        if(inFOV && inRange == true)
        {
            Debug.DrawRay(transform.position, direction, Color.green);
            Debug.Log("Can be seen");

            slider.value += (0.1f * Time.deltaTime); //delete
        }

        if(inFOV == false)
        {
            slider.value -= (0.1f * Time.deltaTime); //delete
        }
    }
}
