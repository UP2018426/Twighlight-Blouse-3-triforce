using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class boss : MonoBehaviour
{

    public Animator anim;
    bool dead = false;

    private NavMeshAgent nma;

    EnemyFOV FOVScript;

    private void Start()
    {
        currentHealth = maxHealth;

        anim = GetComponent<Animator>();

        FOVScript = this.gameObject.GetComponent<EnemyFOV>();

        startTimeBetweenAttacks = 0.8f;/////////////////////////////

        nma = GetComponent<NavMeshAgent>();

        player = GameObject.FindGameObjectWithTag("Player");

        retreatStatus = false;
    }


    GameObject player;

    public GameObject bullet;
    public float fireForce;

    public bool go = false;

    public float timeBetweenAttacks;
    float startTimeBetweenAttacks;

    public float distance;
    public float closestDist;

    public int attackNumber;
    public float nthAttackTime;

    private float RetreatTimer;
    private bool retreatStatus;
    public Transform Corner1;
    public Transform Corner2;

    bool t = false;

    private void Update()
    {
        anim.SetBool("Walk", true);

        distance = Vector3.Distance(player.transform.position, this.transform.position);

        if (go)
        {
            //anim.SetBool("Walk", true);
            
            if(distance < closestDist && retreatStatus == false)
            {
                nma.speed = 0;
                RetreatTimer += 1 * Time.deltaTime;
            }

            if(RetreatTimer > 10)
            {
                retreatStatus = true;
            }

            if(retreatStatus == true)
            {
                nma.speed = 6;
                
                if(Vector3.Distance(Corner1.position, transform.position) > Vector3.Distance(Corner1.position, transform.position)) //corner1 is further. GO THERE
                {
                    nma.SetDestination(Corner2.position);
                }

                else
                {
                    nma.SetDestination(Corner1.position);
                }
            }

            if(this.transform.position.z == Corner1.transform.position.z && this.transform.position.x == Corner1.transform.position.x || this.transform.position.z == Corner2.transform.position.z && this.transform.position.x == Corner2.transform.position.x)
            {
                retreatStatus = false;
            }

            if(distance >= closestDist)
            {
                nma.speed = 6;
                RetreatTimer = 0;
            }

            if(nma.speed == 0)
            {
                transform.LookAt(new Vector3(player.transform.position.x,0,player.transform.position.z));
            }


            if (retreatStatus == false)
            {
                nma.destination = GameObject.FindGameObjectWithTag("Player").transform.position;
            }

            attackPos.transform.LookAt(player.transform.position);


            //if (FOVScript.hit.distance <= 2 && timeBetweenAttacks <= 0)
            //{
            //    Smack();
            //    timeBetweenAttacks = startTimeBetweenAttacks;
            //}



            Debug.Log(attackNumber % 5);
            if (attackNumber % 5 == 0 && t) 
            {
                startTimeBetweenAttacks = nthAttackTime;

                t = false;
            }
            
            

            if (FOVScript.hit.distance >= 4 && timeBetweenAttacks <= 0 && retreatStatus != true)//shoot
            {
                GameObject proj = Instantiate(bullet, attackPos.transform.position, gameObject.transform.rotation);

                proj.GetComponent<Rigidbody>().AddForce((1 * attackPos.transform.forward) * fireForce, ForceMode.Impulse);
                timeBetweenAttacks = startTimeBetweenAttacks;
                
                attackNumber++;

            }
            if (!t)
            {
                startTimeBetweenAttacks = nthAttackTime;
                t = true;
                startTimeBetweenAttacks = 2;
            }



            /*
            else if (FOVScript.FOVState == 3)
            {
                //nma.destination = GameObject.FindGameObjectWithTag("Player").transform.position;

                if (FOVScript.hit.distance <= 2 && timeBetweenAttacks <= 0)
                {
                    Smack();
                    timeBetweenAttacks = startTimeBetweenAttacks;
                }
                timeBetweenAttacks = startTimeBetweenAttacks;

            }
            */
            timeBetweenAttacks -= Time.deltaTime;
        }


        checkIfDead();
    }


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

    public LayerMask targetMask;

    public GameObject attackPos;

    public Vector3 attackSize;

    public int dmg;

    [SerializeField]
    AudioSource audScource;

    [SerializeField]
    AudioClip sound;

    void Smack()
    {
        //anim.SetTrigger("Attack");//////////////////////////////

        //audScource.PlayOneShot(sound);

        Collider[] target = Physics.OverlapBox(attackPos.transform.position, attackSize, Quaternion.identity, targetMask);

        Debug.Log(target.Length);
        for (int i = 0; i < target.Length; i++)
        {
            Debug.Log("Working");
            target[i].GetComponent<PlayerController>().TakeDamage(dmg);
        }
    }

    public float timer;

    void checkIfDead()
    {
        if (currentHealth <= 0)
        { 
            dead = true;
            anim.SetBool("Dead", true);

            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void TakeDamage(int _dmg)
    {
        currentHealth -= _dmg;
        Debug.Log(currentHealth);
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(1);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(attackPos.transform.position, attackSize);
    }
}
