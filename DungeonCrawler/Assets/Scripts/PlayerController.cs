using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //add velocity to stats, gravity to stats, all ground check stuff too

    public CharacterController controller;

    public float speed = 12f;

    public float moveMultiplier = 1f;

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
    //COULD BE MADE INTO ARRY WITH GROUND CHAECJ CASUWE IS SAME THIUNG

    public bool isCrouching;

    public Transform CamPos;
    public float SmoothSpeed;

    public GameObject Camera;

    private Vector3 moveDirJ;

    private Vector3 moveChange;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        moveDirJ.x = Mathf.Clamp(moveDirJ.x, -1, 1);
        moveDirJ.z = Mathf.Clamp(moveDirJ.z, -1, 1);
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

        //running
        if (Input.GetButtonDown("Fire3"))
        {
            moveMultiplier = 1.5f;
        }
        else if (Input.GetButtonUp("Fire3"))
        {
            moveMultiplier = 1;
        }



        roofAbove = Physics.CheckBox(ceilingCheck.position, cCheckSize, Quaternion.Euler(0, 0, 0), ceilingMask);



        //crouching
        if (Input.GetButtonDown("Fire2") && !isCrouching)
        {
            controller.center = new Vector3(0, -0.7f, 0);
            controller.height = 1.5f;
            moveMultiplier = 0.5f;
            isCrouching = true;


        }
        else if (Input.GetButtonDown("Fire2") && isCrouching & !roofAbove)
        {


            controller.center = new Vector3(0, 0, 0);
            controller.height = 3;

            isCrouching = false;
            moveMultiplier = 1f;
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

        Debug.Log(moveDirection);

        velocity.y += gravity * Time.deltaTime;
        //velocity.x = (x + speed * moveMultiplier) * Time.deltaTime;
        //velocity.z = (z + speed * moveMultiplier) * Time.deltaTime;

        //velocity = new Vector3(moveDirection.x += moveMultiplier, velocity.y, moveDirection.z += moveMultiplier);

        controller.Move(velocity * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }



        //var vari = (current - previous) / Time.deltaTime;

        //Debug.Log(controller.velocity);

        //moveDirJ = controller.velocity;



        if (!isGrounded)
        {
            //moveMultiplier = -8;
            controller.stepOffset = 0;



            //moveMultiplier = 0;

            //moveDirJ.x -= 0.2f * Time.deltaTime;
            //moveDirJ.z -= 0.5f * Time.deltaTime;

            
        
            controller.Move((moveDirJ + (moveDirection * 2)) * Time.deltaTime);

            //controller.Move(vari);

            //moveMultiplier = 0.1f;
            //controller.Move(velocity * 12 * Time.deltaTime);
        }
        else
        {
            //moveMultiplier = 0;
            controller.stepOffset = 0.3f;


            moveDirJ = moveDirection.normalized * (speed * moveMultiplier);

            if (moveDirection.magnitude >= 0.1)
            {
                controller.Move(moveDirection.normalized * (speed * moveMultiplier) * Time.deltaTime);
            }
            //velocity = moveDirection;
        }


        /*
        if (controller.isGrounded == true)
        {
            temp = moveDirection;
        }

        if (controller.isGrounded != true)
        {
            moveMultiplier = 0.1f;
            controller.Move(temp * 12 * Time.deltaTime);
        }
        */
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, gCheckSize);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(ceilingCheck.position, cCheckSize);
    }

}
