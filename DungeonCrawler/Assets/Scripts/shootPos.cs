using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootPos : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public GameObject player;
    public float dstFromTarget;

    private float Yaw;
    private float Pitch;

    public Vector2 PitchMinMax = new Vector2(20, 80);

    public Vector3 ShootPosOffset;

    // Update is called once per frame
    void Update()
    {
        Yaw += Input.GetAxis("Mouse X");
        Pitch -= Input.GetAxis("Mouse Y");
        Pitch = Mathf.Clamp(Pitch, PitchMinMax.x, PitchMinMax.y);

        Vector3 TargetRotation = new Vector3(Pitch, Yaw);
        transform.eulerAngles = TargetRotation;

        transform.position = (player.transform.position + ShootPosOffset) + transform.forward * dstFromTarget;
    }
}
