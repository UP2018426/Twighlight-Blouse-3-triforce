using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShaderScript : MonoBehaviour
{
    public float height;
    public float TopRange;
    public float BottomRange;
    public Material thisMat;

    public bool collaped;

    public float speeeeed;

    void Start()
    {
        
    }

    void Update()
    {
        //thisMat.SetFloat("Vector1_F9AEF008", height);
        /*if (Input.GetKeyDown("e"))
        {
            collaped = false;
        }

        if (Input.GetKeyDown("q"))
        {
            collaped = true;
        }*/

        if(collaped == true)
        {
            height = Mathf.Lerp(thisMat.GetFloat("Vector1_F9AEF008"), BottomRange, speeeeed * Time.deltaTime);
        }
        
        if(collaped == false)
        {
            height = Mathf.Lerp(thisMat.GetFloat("Vector1_F9AEF008"), TopRange, speeeeed * Time.deltaTime);
        }
                

        thisMat.SetFloat("Vector1_F9AEF008", height);
        /*if (Input.GetKey("q"))
        {
            height += (8f * Time.deltaTime);
        }
        if (Input.GetKey("e"))
        {
            height -= (8f * Time.deltaTime);
        }*/
    }
}
