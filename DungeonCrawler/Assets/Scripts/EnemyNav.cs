using System.Collections;
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
    [SerializeField]
    private string state;

    public float WaitTimer;

    private bool tempBool;

    // Start is called before the first frame update
    void Start()
    {
        nma = GetComponent<NavMeshAgent>();
        PatrolNum = 0;
        tempBool = true;
    }

    // Update is called once per frame
    void Update()
    {
        //nma.destination = TargetPos;
        //PatrolPosLength = PatrolPos.Length;

        if (PatrolPos.Length == 0)
        {
            Debug.LogWarning("No patrol positions have been set for enemy of name: " + this.name);
        }

        if(PatrolPos.Length == 1)
        {
            Debug.LogWarning("Only 1 patrol position is set for enemy of name: " + this.name);
        }

        if (PatrolPos.Length >= 2 && state == "")
        {
            if (PatrolPos[PatrolNum].x == this.transform.position.x && PatrolPos[PatrolNum].z == this.transform.position.z)
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

            nma.destination = PatrolPos[PatrolNum];
            tempBool = false;
        }
    }
}
