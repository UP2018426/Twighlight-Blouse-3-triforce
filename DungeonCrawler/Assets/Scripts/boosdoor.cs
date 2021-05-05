using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boosdoor : MonoBehaviour
{
    public GameObject keyEnemy;

    public bool done = false;
    public bool donedone = false;
    public bool KEYMADE = false;

    [SerializeField]
    Transform pos;

    [SerializeField]
    Vector3 size;

    [SerializeField]
    LayerMask layer;

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

        if(Physics.CheckBox(pos.position,size,Quaternion.identity,layer) && !donedone)
        {
            transform.position += Vector3.down * 5;
            donedone = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(pos.position,size);
    }

}
