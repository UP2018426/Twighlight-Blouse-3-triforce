using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //add velocity to stats, gravity to stats, all ground check stuff too

    //CharcterStats stats;
    /*
    [System.Serializable]
    class CharacterStats
    {
        public float gravity;

        public float jumpHeight;

        public float speed;

        public float moveMultiplier;

        public Transform groundCheck;
        public Vector3 gCheckSize;
        public LayerMask groundMask;
        public bool isGrounded;


        float maxHealth;
        float currentHealth;
        float health
        {
            get
            {
                return currentHealth;
            }
            set
            {
                if (value <= 0)
                {
                    currentHealth = 0;
                }
                if (value >= maxHealth)
                {
                    currentHealth = maxHealth;
                }

            }
        }
    }
    */

    public CharacterStats characterStats = new CharacterStats();

    public CharacterController controller;

    //public float speed = 12f;

    //public float moveMultiplier = 1f;

    private Vector3 velocity;
    
    public float gravity = -56.86f;

    //public float jumpHeight = 10;

    private Vector3 moveDirection;

    //public Transform groundCheck;
    //public Vector3 gCheckSize;
    //public LayerMask groundMask;
    //public bool isGrounded;


    public Transform ceilingCheck;
    public Vector3 cCheckSize;
    public LayerMask ceilingMask;
    public bool roofAbove;
    public float SphereCheckSize;
    //COULD BE MADE INTO ARRY WITH GROUND CHAECJ CASUWE IS SAME THIUNG

    public bool isCrouching;

    public Transform CamPos;
    public float SmoothSpeed;

    public GameObject Camera;

    //private Vector3 moveDirJ;

    //public float airM;

    // Start is called before the first frame update


    public Transform shootPos;
    public GameObject shootObj;
    public float fireForce;

    public Vector3 attackSize;
    public LayerMask enemyMask;

    public float timeBetweenAttacks;
    public float startTimeBetweenAttacks;


    public int dmg;


    public float maxHealth;
    public float currentHealth;
    float health
    {
        get
        {
            return currentHealth;
        }
        set
        {
            if (value <= 0)
            {
                currentHealth = 0;
            }
            if (value >= maxHealth)
            {
                currentHealth = maxHealth;
            }

        }
    }


    public int weaponSelected;

    void Awake()
    {
        //stats = GetComponent<CharcterStats>();

        //stats.gravity = -58.86f;

        currentHealth = maxHealth;
    }
    

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        //moveDirJ.x = Mathf.Clamp(moveDirJ.x, -1, 1);
        //moveDirJ.z = Mathf.Clamp(moveDirJ.z, -1, 1);

        controller.stepOffset = 0.3f;

        //characterStats.moveMultiplier = 1f;
        //characterStats.speed = 12f;
        //characterStats.jumpHeight = 10f;
    }

    public void TakeDamage(int _dmg)
    {
        currentHealth -= _dmg;
        Debug.Log("player " + currentHealth);
    }


    // Update is called once per frame
    void Update()
    {
        //Istvan wrote this
        if (Input.GetMouseButtonDown(0))
        {
            //Cursor.visible = false;
            //Cursor.lockState = CursorLockMode.Locked;
        }
        //my bad bro


        //shootPos.localRotation = gameObject.transform.rotation;

        //shoot
        if (Input.GetButtonDown("Fire1") && timeBetweenAttacks <= 0 && weaponSelected == 2)
        {
            TakeDamage(1);

            GameObject proj = Instantiate(shootObj, shootPos.position, gameObject.transform.rotation);

            proj.GetComponent<Rigidbody>().AddForce(Camera.transform.forward * fireForce, ForceMode.Impulse);
            
            timeBetweenAttacks = startTimeBetweenAttacks;
        }
        else
        {
            timeBetweenAttacks -= Time.deltaTime;
        }

        //melee
        if (Input.GetButtonDown("Fire1") && timeBetweenAttacks <= 0 && weaponSelected < 2)
        {
            Collider[] enemies = Physics.OverlapBox(shootPos.position, attackSize, Quaternion.identity,enemyMask);
            
            Debug.Log(enemies.Length);
            for (int i = 0; i < enemies.Length; i++)
            {
                if (weaponSelected == 1)
                {
                    enemies[i].GetComponent<EnemyNav>().TakeDamage(dmg * 2);
                }
                else
                {
                    enemies[i].GetComponent<EnemyNav>().TakeDamage(dmg);
                }
            }
            timeBetweenAttacks = startTimeBetweenAttacks;
        }
        else
        {
            timeBetweenAttacks -= Time.deltaTime;
        }

        //should be working when enemies are made




        if (Input.GetKeyDown("1"))//sword
        {
            weaponSelected = 1;
            TakeDamage(2);
        }
        if (Input.GetKeyDown("2"))//bow
        {
            weaponSelected = 2;
        }
        if (Input.GetKeyDown("0"))//handle
        {
            weaponSelected = 0;
        }




        //shootObj.transform.position = transform.position - transform.forward * distFromTarget;


        if (Input.GetButtonDown("Cancel"))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        //isGrounded = Physics.CheckBox(groundCheck.position, gCheckSize, Quaternion.Euler(0,0,0), groundMask);

        if (/*isGrounded*/ controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -10f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //running
        if (Input.GetButtonDown("Fire3"))
        {
            characterStats.moveMultiplier = 1.5f;
        }
        else if (Input.GetButtonUp("Fire3"))
        {
            characterStats.moveMultiplier = 1;
        }



        roofAbove = Physics.CheckSphere(ceilingCheck.position, /*cCheckSize*/SphereCheckSize, ceilingMask /*Quaternion.Euler(0, 0, 0), ceilingMask*/);

        //crouching
        if (Input.GetButtonDown("Fire2") && !isCrouching)
        {
            controller.center = new Vector3(0, -0.5f, 0);
            controller.height = 1.5f;
            characterStats.moveMultiplier = 0.5f;
            isCrouching = true;
        }
        else if (Input.GetButtonDown("Fire2") && isCrouching & !roofAbove)
        {
            controller.center = new Vector3(0, 0, 0);
            controller.height = 3;

            isCrouching = false;
            characterStats.moveMultiplier = 1f;
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

        //Debug.Log(moveDirection);
        //Debug.Log(moveDirJ);

        velocity.y += gravity * Time.deltaTime;
        //velocity.x = (x + speed * moveMultiplier) * Time.deltaTime;
        //velocity.z = (z + speed * moveMultiplier) * Time.deltaTime;

        //velocity = new Vector3(moveDirection.x += moveMultiplier, velocity.y, moveDirection.z += moveMultiplier);

        controller.Move(velocity * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && controller.isGrounded/*isGrounded*/ && !isCrouching)
        {
            velocity.y = Mathf.Sqrt(characterStats.jumpHeight * -2 * gravity);
        }
        else if(Input.GetButtonDown("Jump") && controller.isGrounded && isCrouching)
        {

        }

        if (moveDirection.magnitude >= 0.1)
        {
            controller.Move(moveDirection.normalized * (characterStats.speed * characterStats.moveMultiplier) * Time.deltaTime);
        }

        //var vari = (current - previous) / Time.deltaTime;

        //Debug.Log(controller.velocity);

        //moveDirJ = controller.velocity;



        if (!controller.isGrounded)
        {
            //moveMultiplier = -8;
            controller.stepOffset = 0;



            //moveMultiplier = 0;

            //moveDirJ.x -= 0.2f * Time.deltaTime;
            //moveDirJ.z -= 0.5f * Time.deltaTime;



            //controller.Move(((moveDirJ + (moveDirection.normalized * airM))) * Time.deltaTime);//could be changed slightly to fix the extra speed

            //controller.Move(vari);

            //moveMultiplier = 0.1f;
            //controller.Move(velocity * 12 * Time.deltaTime);
        }
        else
        {
            //moveMultiplier = 0;
            controller.stepOffset = 0.3f;


            //moveDirJ = moveDirection.normalized * (speed * moveMultiplier);
            //moveDirJA = moveDirJ;

            //if (moveDirection.magnitude >= 0.1)
            //{
            //    controller.Move(moveDirection.normalized * (speed * moveMultiplier) * Time.deltaTime);
            //}
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
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireCube(groundCheck.position, gCheckSize);

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(shootPos.position,attackSize);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(ceilingCheck.position, SphereCheckSize);
    }

}
