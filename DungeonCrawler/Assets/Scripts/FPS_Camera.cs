using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS_Camera : MonoBehaviour
{

    public float mouseSensitivity;

    public GameObject Player;

    private float vertRotation;

    public float minClamp;
    public float maxClamp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        vertRotation -= mouseY;

        vertRotation = Mathf.Clamp(vertRotation,minClamp,maxClamp);

        Player.transform.Rotate(Vector3.up * mouseX);

        transform.localRotation = Quaternion.Euler(vertRotation,0f,0f);

        
    }
}
