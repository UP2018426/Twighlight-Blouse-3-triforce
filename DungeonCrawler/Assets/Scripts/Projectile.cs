using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;

    public float gravity = -9.8f;

    public string[] tagToCompare;

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));
        transform.Translate(Vector3.down * (gravity * Time.deltaTime));
        //may want to add a bullet drop by using the same method transform.translate and using vector3.down

    }


    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag(tagToCompare[0]))//enemies
        {

        }
        if (col.gameObject.layer == LayerMask.NameToLayer(tagToCompare[1]))//floor and walls
        {

        }
    }
}
