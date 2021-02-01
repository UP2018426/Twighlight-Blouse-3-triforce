using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats
{
    public float gravity;

    public float jumpHeight = 10;

    public float speed = 12f;

    public float moveMultiplier = 1f;

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
            if(value <= 0)
            {
                currentHealth = 0;
            }
            if(value >= maxHealth)
            {
                currentHealth = maxHealth;
            }

        }
    }



}
