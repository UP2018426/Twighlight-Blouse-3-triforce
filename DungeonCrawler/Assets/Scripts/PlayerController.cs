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

    bool isCrouching;

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

        
        //crouching
        if (Input.GetButtonDown("Fire2") && !isCrouching)
        {


            //controller.center = new Vector3(0, -0.7f, 0);

            moveMultiplier = -6f;
            isCrouching = true;
        }
        else if (Input.GetButtonDown("Fire2") && isCrouching)
        {


            controller.center = new Vector3(0, 0, 0);


            isCrouching = false;
            moveMultiplier = 0f;
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

        }
        else
        {
            moveMultiplier = 0;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, gCheckSize);
        
    }

}
