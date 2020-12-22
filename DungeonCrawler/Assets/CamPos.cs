using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamPos : MonoBehaviour
{
    public Vector3 Standing;
    public Vector3 Crouching;

    PlayerController player;
    public GameObject Player;


    // Start is called before the first frame update
    void Start()
    {
        Standing = new Vector3(0, 0.9f, 0);
        Crouching = new Vector3(0, 0, 0);

        player = Player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localPosition = Standing;
        if (player.isCrouching)
        {
            //Debug.Log("shit is real");
            this.transform.localPosition = Crouching;
        }

    }
}
