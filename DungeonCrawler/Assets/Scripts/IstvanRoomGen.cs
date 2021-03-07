using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IstvanRoomGen : MonoBehaviour
{
    public int tempInt;

    public int Zoffset;
    public int Xoffset;

    public GameObject SphereObj;

    private Vector3 Trans;

    /*public GameObject r1; //1 door
    public GameObject r2; //90 degree door
    public GameObject r3; //straight
    public GameObject r4; //T doors 
    public GameObject r5; //all 4 doors
    public GameObject r6; //end*/

    public GameObject[] gridpos;

    // Start is called before the first frame update
    void Start()
    {
        Zoffset = 0;
        Xoffset = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("l"))
        {
            if(Xoffset >= 50)
            {
                Xoffset = 0;
                Zoffset += 10;
            }

            else
            {
                Xoffset += 10;
            }

            Trans = this.transform.position;

            Trans.x += Xoffset;
            Trans.z += Zoffset;

            Instantiate(SphereObj, Trans, Quaternion.identity);

        }
    }
}
