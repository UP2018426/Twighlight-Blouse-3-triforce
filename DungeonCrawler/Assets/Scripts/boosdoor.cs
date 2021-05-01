using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boosdoor : MonoBehaviour
{
    GameObject keyEnemy;

    bool done;

    void Update()
    {
        if(keyEnemy == null &! done)
        {
            this.transform.position += Vector3.up * 5;

            done = true;
        }
    }
}
