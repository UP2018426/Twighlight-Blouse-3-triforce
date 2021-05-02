using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boosdoor : MonoBehaviour
{
    public GameObject keyEnemy;

    public bool done = false;
    public bool KEYMADE = false;

    void Update()
    {
        if(keyEnemy == null &! KEYMADE)
        {
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("enemy").Length; i++)
            {
                if(GameObject.FindGameObjectsWithTag("enemy")[i].GetComponent<EnemyNav>().holdKey == true)
                {
                    keyEnemy = GameObject.FindGameObjectsWithTag("enemy")[i];
                }
            }
            //keyEnemy = GameObject.FindWithTag("enemy");
            if(keyEnemy != null)
            {
                KEYMADE = true;
            }
        }

        if(keyEnemy == null &! done && KEYMADE)
        {
            this.transform.position += Vector3.up * 5;

            done = true;
        }
    }
}
