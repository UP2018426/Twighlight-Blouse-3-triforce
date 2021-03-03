using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoom : MonoBehaviour
{
    private RoomTemplate template;

    private void Awake()
    {
        template = GameObject.FindGameObjectWithTag("Room").GetComponent<RoomTemplate>();
        template.rooms.Add(this.gameObject);
    }
}
