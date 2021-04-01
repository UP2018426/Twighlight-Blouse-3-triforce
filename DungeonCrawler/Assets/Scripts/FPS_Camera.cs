using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS_Camera : MonoBehaviour
{

    public float mouseSensitivity;

    public GameObject player;

    //private float vertRotation;
    
    
    float mouseX;
    float mouseY;

    public float minClamp;
    public float maxClamp;



    //public Transform shootPos;
    public GameObject shootObj;
    public float fireForce;
    public float dist;

    // Update is called once per frame
    void Update()
    {
        //float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        //float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //vertRotation -= mouseY;

        //vertRotation = Mathf.Clamp(vertRotation,minClamp,maxClamp);

        //Player.transform.Rotate(Vector3.up * mouseX);



        
        mouseX += Input.GetAxis("Mouse X");
        mouseY -= Input.GetAxis("Mouse Y");
        mouseY = Mathf.Clamp(mouseY, minClamp, maxClamp);
        player.transform.rotation = Quaternion.Euler(0, mouseX, 0);
        //transform.localRotation = Quaternion.Euler(vertRotation, 0f, 0f);
        
        transform.localRotation = Quaternion.Euler(mouseY, 0f, 0f);

        //Physics.Linecast(gameObject.transform.position,transform.forward * dist);

        if (Input.GetButtonDown("Fire1"))
        {
            //GameObject proj = Instantiate(shootObj, , gameObject.transform.localRotation);

            //proj.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * fireForce, ForceMode.Impulse);
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawLine(transform.position, (transform.position += transform.forward) * dist);
    }

    //public float mouseSensitivity;

    //public GameObject player;

    //private float vertRotation;

    //public float minClamp;
    //public float maxClamp;



    //public Vector2 PitchMinMax = new Vector2(69, 69);

    //void Update()
    //{
    //float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
    //float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

    ///mouseX += Input.GetAxis("Mouse X");
    ///mouseY -= Input.GetAxis("Mouse Y");
    ///mouseY = Mathf.Clamp(mouseY, minClamp, maxClamp);

    //vertRotation -= mouseY;

    //vertRotation = Mathf.Clamp(vertRotation,minClamp,maxClamp);


    //Player.transform.Rotate(Vector3.up * mouseX);

    //}
}