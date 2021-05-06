using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss : MonoBehaviour
{

    public Animator anim;
    bool dead = false;

    //[SerializeField]
    //GameObject endpoint;

    private void Start()
    {
        currentHealth = maxHealth;

        anim = GetComponent<Animator>();
    }





    private void Update()
    {
        //anim.SetFloat("Walk", Mathf.Abs(nma.speed));

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
    void Smack()
    {
        anim.SetTrigger("Attack");

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
                //Instantiate(endpoint,this.transform);
                //Destroy(gameObject);
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
