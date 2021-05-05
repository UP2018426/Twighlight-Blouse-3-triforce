using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endPoint : MonoBehaviour
{
    GameManager gm;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    [SerializeField]
    GameObject endscreen;

    [SerializeField]
    Transform pos;

    [SerializeField]
    Vector3 size;

    [SerializeField]
    LayerMask layer;

    private void Update()
    {
        if (Physics.CheckBox(pos.position, size, Quaternion.identity, layer))
        {
            endscreen.SetActive(true);
            gm.endscreen = true;
            Time.timeScale = 0f;
        }
    }
}
