using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 93.2f;

    public float gravity = -1.6f;

    public LayerMask layerToCompare;

    public string enemies;

    private void FixedUpdate()
    {
        //transform.Translate(Vector3.forward * (speed * Time.deltaTime));
        //transform.Translate(Vector3.up * (gravity * Time.deltaTime));
        //may want to add a bullet drop by using the same method transform.translate and using vector3.down

    }


    private void OnCollisionEnter(Collision col)
    {
        //if(col.gameObject.CompareTag(enemies))//enemies
        //{

        //}
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