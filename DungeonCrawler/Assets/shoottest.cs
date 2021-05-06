using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoottest : MonoBehaviour
{

    public GameObject bullet;
    public float fireForce;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("r"))
        {
            GameObject proj = Instantiate(bullet, transform.position, gameObject.transform.rotation);
            proj.GetComponent<Rigidbody>().AddForce(transform.forward * fireForce, ForceMode.Impulse);
        }
    }
 
}