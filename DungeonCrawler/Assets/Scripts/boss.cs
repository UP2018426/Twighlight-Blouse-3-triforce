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

        startTimeBetweenAttacks = 1;

        nma = GetComponent<NavMeshAgent>();
    }


    GameObject player;

    public GameObject bullet;
    public float fireForce;

    public bool go = false;

    public float timeBetweenAttacks;
    float startTimeBetweenAttacks;

    private void Update()
    {
        anim.SetFloat("Walk", Mathf.Abs(nma.speed));

        if (go)
        {
            nma.destination = GameObject.FindGameObjectWithTag("Player").transform.position;
        }


        if (FOVScript.hit.distance <= 2 && timeBetweenAttacks <= 0)
        {
            Smack();
            timeBetweenAttacks = startTimeBetweenAttacks;
        }
        timeBetweenAttacks -= Time.deltaTime;

        if(FOVScript.hit.distance >= 4 && timeBetweenAttacks <= 0)//shoot
        {
            GameObject proj = Instantiate(bullet, attackPos.transform.position, gameObject.transform.rotation);

            proj.GetComponent<Rigidbody>().AddForce(attackPos.transform.forward * fireForce, ForceMode.Impulse);
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
        anim.SetTrigger("Attack");

        audScource.PlayOneShot(sound);

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
