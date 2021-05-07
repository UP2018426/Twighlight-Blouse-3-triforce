using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile2 : MonoBehaviour
{

    public LayerMask layerToCompare;

    private void OnCollisionEnter(Collision col)
    {
        //if(col.gameObject.CompareTag(enemies))//enemies
        //{

        //}
        if (col.gameObject.GetComponent<PlayerController>())
        {
            col.gameObject.GetComponent<PlayerController>().TakeDamage(3);
        }
        if (col.gameObject.layer == layerToCompare)//floor and walls
        {
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
