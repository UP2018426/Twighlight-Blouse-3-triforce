using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkGensize : MonoBehaviour
{
    private Camera mainCamera;

    private IstvanRoomGen roomGen;


    Ray camRay;
    float rayLength;

    Vector3 fromPos;
    Vector3 toPos;
    Vector3 dir;

    void Awake()
    {
        mainCamera = FindObjectOfType<Camera>();

        roomGen = GameObject.FindGameObjectWithTag("start").GetComponent<IstvanRoomGen>();
    }


    List<GameObject> grid;

    private void Start()
    {
        for (int i = 0, z = 0; z <= roomGen.Zsize; z++)
        {
            for (int x = 0; x <= roomGen.Xsize; x++)
            {
                fromPos = transform.position;

                toPos = new Vector3(x * roomGen.gridSpacing, 0, z * roomGen.gridSpacing);

                dir = toPos - fromPos;

                RaycastHit hit;

                if(Physics.Raycast(fromPos,dir,out hit,Mathf.Infinity))
                {
                    if(true)//needs to add room to list
                    {

                    }
                }
                ++i;
            }
        }
    }
}
