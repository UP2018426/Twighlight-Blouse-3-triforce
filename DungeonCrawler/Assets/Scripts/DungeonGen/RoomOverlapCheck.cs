using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomOverlapCheck : MonoBehaviour
{
    //public float countdown;

    Vector3 oldTransform;

    RoomGen roomGen;

    GridGen gridGen;

    public bool mainRoom = false;

    private void Awake()
    {
        roomGen = GameObject.FindGameObjectWithTag("Respawn").GetComponent<RoomGen>();

        gridGen = GameObject.FindGameObjectWithTag("Respawn").GetComponent<GridGen>();

        //gameObject.AddComponent<BoxCollider>().isTrigger = true;

        gameObject.tag = "Room";
    }

    private void Start()
    {
        //countdown = roomGen.timer;

        //countdown -= Time.deltaTime;
        //countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        if(gameObject.transform.localScale.x >= roomGen.minMainRoomsSize && gameObject.transform.localScale.z >= roomGen.minMainRoomsSize)
        {
            mainRoom = true;
        }
    }

    private void Update()
    {
        if(/*countdown <= 0*/ Input.GetKey("b"))
        {
            oldTransform = transform.position;

            //gameObject.GetComponent<BoxCollider>().isTrigger = true;
            transform.position = new Vector3(Mathf.Round(oldTransform.x),0, Mathf.Round(oldTransform.z));
        }

        if(mainRoom == true)
        {
            gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
        }
        //else
        //{
        //    Destroy(gameObject);
        //}
        //need to make a timer
        //Debug.Log(roomCode);

    }

    public string roomCode;

    public GameObject[] conectedTo;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Room"))
        {
            
                Destroy(gameObject);
            

        }
    }

}
