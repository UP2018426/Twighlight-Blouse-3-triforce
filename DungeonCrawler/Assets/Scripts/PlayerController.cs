using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //add velocity to stats, gravity to stats, all ground check stuff too

    public CharacterController controller;

    public float speed = 12f;

    public float moveMultiplier = 0f;

    private Vector3 velocity;

    public float gravity;

    public float jumpHeight = 10;

    private Vector3 moveDirection;

    public Transform groundCheck;
    public float gCheckSize;
    public LayerMask groundMask;
    public bool isGrounded;

    
    public Transform ceilingCheck;
    public Vector3 cCheckSize;
    public LayerMask ceilingMask;
    public bool roofAbove;

    public bool isCrouching;

    public Transform CamPos;
    public float SmoothSpeed;

    public GameObject Camera;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, gCheckSize, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Fire3"))
        {
            moveMultiplier = 12;
        }
        else if (Input.GetButtonUp("Fire3"))
        {
            moveMultiplier = 0;
        }



         roofAbove = Physics.CheckBox(ceilingCheck.position, cCheckSize, Quaternion.Euler(0,0,0), ceilingMask);



        //crouching
        if (Input.GetButtonDown("Fire2") && !isCrouching)
        {
            controller.center = new Vector3(0, -0.7f, 0);
            controller.height = 1.5f;
            moveMultiplier = -6f;
            isCrouching = true;


        }
        else if (Input.GetButtonDown("Fire2") && isCrouching &! roofAbove)
        {


            controller.center = new Vector3(0, 0, 0);
            controller.height = 3;

            isCrouching = false;
            moveMultiplier = 0f;
        }

        if (isCrouching == true)
        {
            Camera.transform.position = Vector3.Lerp(Camera.transform.position, CamPos.position, SmoothSpeed * Time.deltaTime);
        }
        if (isCrouching == false)
        {
            Camera.transform.position = Vector3.Lerp(Camera.transform.position, CamPos.position, SmoothSpeed * Time.deltaTime);
        }


        moveDirection = (transform.right * x + transform.forward * z);


        if (moveDirection.magnitude >= 0.1)
        {
            controller.Move(moveDirection.normalized * (speed + moveMultiplier) * Time.deltaTime);
        }
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        if (!isGrounded)
        {

            //moveMultiplier = -8;
            controller.stepOffset = 0;
        }
        else
        {
            moveMultiplier = 0;
            controller.stepOffset = 0.3f;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, gCheckSize);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(ceilingCheck.position, cCheckSize);
    }

}
