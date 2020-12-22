using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //add velocity to stats, gravity to stats, all ground check stuff too

    public CharacterController controller;

    public float speed = 12f;

    public float runMultiplier = 0f;

    private Vector3 velocity;

    public float gravity = -9.81f;

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
        if(Input.GetButtonDown("Cancel"))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        isGrounded = Physics.CheckSphere(groundCheck.position,gCheckSize,groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        /*
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        */

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Fire3"))
        {
            runMultiplier = 12;
        }
        else if(Input.GetButtonUp("Fire3"))
        {
            runMultiplier = 0;
        }

        if(Input.GetButtonDown("Fire2"))
        {
            transform.localScale -= new Vector3 (1f,0.5f,1f); 
        }
        else if(Input.GetButtonUp("Fire2"))
        {
            transform.localScale += new Vector3(1f, 0.5f, 1f);
        }

        moveDirection = (transform.right * x + transform.forward * z);
        //moveDirection = moveDirection.normalized * speed;

        //need normalised to work

        //moveDirection.y *= Time.fixedDeltaTime;
        //moveDirection.x *= Time.fixedDeltaTime;//lines 49 to 53 move to fixed update

        if (moveDirection.magnitude >= 0.1)
        {
            controller.Move(moveDirection.normalized * (speed + runMultiplier) * Time.deltaTime);
        }
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, gCheckSize);
        
    }

}
