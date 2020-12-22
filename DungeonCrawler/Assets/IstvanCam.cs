using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IstvanCam : MonoBehaviour
{
    public bool crouched;
    public float SmoothSpeed;

    public Transform CamPos;

    void Update()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            crouched = true;
        }
        if(Input.GetButtonUp("Fire2"))
        {
            crouched = false;
        }
        if(crouched == true)
        {
            transform.position = Vector3.Lerp(transform.position, CamPos.transform.position, SmoothSpeed * Time.deltaTime);
        }
        if(crouched == false)
        {
            transform.position = Vector3.Lerp(transform.position, CamPos.transform.position, SmoothSpeed * Time.deltaTime);
        }

    }
}
