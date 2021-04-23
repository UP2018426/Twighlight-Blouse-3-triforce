using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRoom : MonoBehaviour
{
    public GameObject player;
    private void Start()
    {
        var k = transform.position + new Vector3(0, 5, 0);
        Instantiate(player, k,Quaternion.identity);
    }
}
