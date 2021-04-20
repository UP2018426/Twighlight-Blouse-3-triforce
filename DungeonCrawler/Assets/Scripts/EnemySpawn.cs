using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [System.Serializable]
    public class EnemyRoomSpawn
    {
        public GameObject enemy;
        public int numSpawn;
        public Transform SpawnPos;
    }

    public Transform[] patrolPoints;

    public EnemyRoomSpawn toSpawn;

    private void Start()
    {
        for (int i = 0; i < toSpawn.numSpawn; i++)
        {
            Instantiate(toSpawn.enemy,toSpawn.SpawnPos.position,Quaternion.identity);
        }
    }

}
