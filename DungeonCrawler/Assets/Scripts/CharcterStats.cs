using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharcterStats
{
    float speed;

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
