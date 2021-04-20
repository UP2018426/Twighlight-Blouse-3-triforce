﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNav : MonoBehaviour
{ 
    private NavMeshAgent nma;
    [SerializeField]
    private int PatrolNum;
    [SerializeField]
    private Vector3[] PatrolPos;

    private Vector3 LocalPos;

    public float WaitTimer;

    private bool tempBool;

    private EnemyFOV FOVScript;

    public float RunSpeed;
    public float WalkSpeed;

    public Transform ParentRoom;

    void Start()
    {
        nma = GetComponent<NavMeshAgent>();

        FOVScript = this.gameObject.GetComponent<EnemyFOV>();

        PatrolNum = 0;
        tempBool = false;
        nma.destination = PatrolPos[0];
    }

    void Update()
    {
        //nma.destination = TargetPos;
        //PatrolPosLength = PatrolPos.Length;

        if (FOVScript.FOVState == 1) //FOLLOW PATROL ROUTE
        {
            nma.speed = WalkSpeed;

            if (PatrolPos.Length == 0)
            {
                Debug.LogWarning("No patrol positions have been set for enemy of name: " + this.name);
            }

            if (PatrolPos.Length == 1)
            {
                Debug.LogWarning("Only 1 patrol position is set for enemy of name: " + this.name);
            }

            if (PatrolPos.Length >= 2)
            {
                if (ParentRoom.transform.position.x - PatrolPos[PatrolNum].x == this.transform.position.x && ParentRoom.transform.position.z - PatrolPos[PatrolNum].z == this.transform.position.z)
                {
                    tempBool = true;
                    Debug.Log("in pos");
                }

                if (PatrolNum >= PatrolPos.Length - 1 && tempBool == true)
                {
                    PatrolNum = 0;
                    Debug.Log("reset to 0");
                }

                else if (PatrolNum < PatrolPos.Length && tempBool == true)
                {
                    PatrolNum++;
                    Debug.Log("+ 1");
                }

                nma.destination = ParentRoom.transform.position - PatrolPos[PatrolNum];
                tempBool = false;
                //nma.destination = PatrolPos[PatrolNum];
            }
        }

        if(FOVScript.FOVState == 2)
        {
            nma.speed = WalkSpeed;
            nma.destination = GameObject.FindGameObjectWithTag("Player").transform.position;
        }

        if(FOVScript.FOVState == 3)
        {
            nma.speed = RunSpeed;
            nma.destination = GameObject.FindGameObjectWithTag("Player").transform.position;
        }
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
        Collider[] target = Physics.OverlapBox(attackPos.transform.position, attackSize, Quaternion.identity, targetMask);

        Debug.Log(target.Length);
        for (int i = 0; i < target.Length; i++)
        {
            Debug.Log("Working");
            target[i].GetComponent<PlayerController>().TakeDamage(dmg);
        }
    }


    public void TakeDamage(int _dmg)
    {
        currentHealth -= _dmg;
        //Debug.Log(characterStats.currentHealth);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(attackPos.transform.position, attackSize);
    }
}
