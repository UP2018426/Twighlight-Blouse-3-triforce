using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IstvanRoomGenDelete : MonoBehaviour
{
    private int randomNum;
    
    [SerializeField]
    public bool fwd;
    [SerializeField]
    public bool bck;
    [SerializeField]
    public bool lft;
    [SerializeField]
    public bool rgt;


    public bool isConected;

    public bool isBeegBoss = false;

    public bool start = false;

    //public IstvanRoomGenDelete iSDelete;

    private float dist;

    public int corners;

    public GameObject r1; //1 door
    public GameObject r2; //L degree door
    public GameObject r3; //straight
    public GameObject r4; //T doors 
    public GameObject r5; //all 4 doors
    public GameObject r6; //end
    public GameObject r7; //start

    //IstvanRoomGen iRoomgen;

    checkGensize CheckGensize;

    

    private void Awake()
    {
        //iRoomgen = GameObject.FindGameObjectWithTag("start").GetComponent<IstvanRoomGen>();
        CheckGensize = GameObject.FindGameObjectWithTag("GameController").GetComponent<checkGensize>();
        dist = CheckGensize.gridSpacing;
    }

    // Start is called before the first frame update
    void Start()
    {
        //dist = iRoomgen.gridSpacing;
        
    }

    public bool boom = false;
    public void Delete()
    {
        randomNum = Random.Range(1, 8);
    
        if(randomNum >= 5)
        {
            //Debug.Log("destroyed");
            Destroy(this.gameObject);
        }

        //CheckGensize.delete = true;
    }

    bool flipped = false;

    public void RoomChecking()
    {
        RaycastHit hit;

        if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.forward), out hit, dist))
        {
            fwd = true;
        }

        if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.right), out hit, dist))
        {
            rgt = true;
        }

        if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.back), out hit, dist))
        {
            bck = true;
        }

        if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.left), out hit, dist))
        {
            lft = true;
        }

        
        //needs to be slightly changed so that if 1,2,3 or 4 are true then it increments once

        //for checkign how many conected
        if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.forward), out hit, dist) && isConected == true)
        {
            hit.transform.gameObject.GetComponent<IstvanRoomGenDelete>().isConected = true;
            Debug.DrawRay(this.transform.position, transform.TransformDirection(Vector3.up) * 50, Color.magenta, 100f);
        }

        if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.right), out hit, dist) && isConected == true)
        {
            hit.transform.gameObject.GetComponent<IstvanRoomGenDelete>().isConected = true;
            Debug.DrawRay(this.transform.position, transform.TransformDirection(Vector3.up) * 50, Color.magenta, 100f);
        }

        if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.back), out hit, dist) && isConected == true)
        {
            hit.transform.gameObject.GetComponent<IstvanRoomGenDelete>().isConected = true;
            Debug.DrawRay(this.transform.position, transform.TransformDirection(Vector3.up) * 50, Color.magenta, 100f);
        }

        if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.left), out hit, dist) && isConected == true)
        {
            hit.transform.gameObject.GetComponent<IstvanRoomGenDelete>().isConected = true;
            Debug.DrawRay(this.transform.position, transform.TransformDirection(Vector3.up) * 50, Color.magenta, 100f);
        }
        
        if(isConected &! flipped)
        {
            CheckGensize.connected++;
            flipped = true;
        }

        corners = 0;

        if (fwd == true)
        {
            corners++;
        }
        if (rgt == true)
        {
            corners++;
        }
        if (bck == true)
        {
            corners++;
        }
        if (lft == true)
        {
            corners++;
        }

        //CheckGensize.check = true;
    }

    public void Spawn()
    {
        if (isConected)
        {
            if (corners == 4) //just spawn a 4way
            {
                Instantiate(r5, this.transform.position, Quaternion.identity);
            }

            if (corners == 3) // just spawn a 3way + rotate
            {
                if (fwd == false)
                {
                    Instantiate(r4, this.transform.position, Quaternion.Euler(0, 90, 0));
                }
                if (rgt == false)
                {
                    Instantiate(r4, this.transform.position, Quaternion.Euler(0, 180, 0));
                }
                if (bck == false)
                {
                    Instantiate(r4, this.transform.position, Quaternion.Euler(0, 270, 0));
                }
                if (lft == false)
                {
                    Instantiate(r4, this.transform.position, Quaternion.Euler(0, 0, 0));
                }
            }

            if (corners == 2)//find L or straight + rotate
            {
                if (fwd && bck == true || lft && rgt == true)
                {
                    Debug.Log("straight");
                    if (fwd == true)
                    {
                        Instantiate(r3, this.transform.position, Quaternion.Euler(0, 0, 0));
                    }

                    if (rgt == true)
                    {
                        Instantiate(r3, this.transform.position, Quaternion.Euler(0, 90, 0));
                    }

                }

                else
                {
                    Debug.Log("L");
                    if (fwd == true)
                    {
                        if (rgt == true)
                        {
                            Instantiate(r2, this.transform.position, Quaternion.Euler(0, 0, 0));
                        }

                        else
                        {
                            Instantiate(r2, this.transform.position, Quaternion.Euler(0, 270, 0));
                        }
                    }

                    if (bck == true)
                    {
                        if (rgt == true)
                        {
                            Instantiate(r2, this.transform.position, Quaternion.Euler(0, 90, 0));
                        }

                        else
                        {
                            Instantiate(r2, this.transform.position, Quaternion.Euler(0, 180, 0));
                        }
                    }
                }
            }

            if (corners == 1 & !isBeegBoss & !start)//spawn a 1 + rotate
            {
                if (fwd == true)
                {
                    Instantiate(r1, this.transform.position, Quaternion.Euler(0, 0, 0));
                }

                if (rgt == true)
                {
                    Instantiate(r1, this.transform.position, Quaternion.Euler(0, 90, 0));
                }

                if (bck == true)
                {
                    Instantiate(r1, this.transform.position, Quaternion.Euler(0, 180, 0));
                }

                if (lft == true)
                {
                    Instantiate(r1, this.transform.position, Quaternion.Euler(0, 270, 0));
                }
            }

            if (corners == 1 && isBeegBoss & !start)//spawn end + rotate
            {
                if (fwd == true)
                {
                    Instantiate(r6, this.transform.position, Quaternion.Euler(0, 0, 0));
                }

                if (rgt == true)
                {
                    Instantiate(r6, this.transform.position, Quaternion.Euler(0, 90, 0));
                }

                if (bck == true)
                {
                    Instantiate(r6, this.transform.position, Quaternion.Euler(0, 180, 0));
                }

                if (lft == true)
                {
                    Instantiate(r6, this.transform.position, Quaternion.Euler(0, 270, 0));
                }
            }

            if (corners == 1 & !isBeegBoss && start)//spawn start + rotate
            {
                if (fwd == true)
                {
                    Instantiate(r7, this.transform.position, Quaternion.Euler(0, 0, 0));
                }

                if (rgt == true)
                {
                    Instantiate(r7, this.transform.position, Quaternion.Euler(0, 90, 0));
                }

                if (bck == true)
                {
                    Instantiate(r7, this.transform.position, Quaternion.Euler(0, 180, 0));
                }

                if (lft == true)
                {
                    Instantiate(r7, this.transform.position, Quaternion.Euler(0, 270, 0));
                }
            }
        }
        boom = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(boom)
        {
            Destroy(gameObject);
        }


    //    if (Input.GetKeyDown("k"))
    //    {
    //        randomNum = Random.Range(1, 9);
    //    }

    //    if (randomNum == 5)
    //    {
    //        Debug.Log("destroyed");
    //        Destroy(this.gameObject);
    //    }

    //    if (Input.GetKeyDown("j"))
    //    {
    //        RaycastHit hit;

    //        if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.forward), out hit, dist))
    //        {
    //            fwd = true;
    //        }

    //        if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.right), out hit, dist))
    //        {
    //            rgt = true;
    //        }

    //        if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.back), out hit, dist))
    //        {
    //            bck = true;
    //        }

    //        if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.left), out hit, dist))
    //        {
    //            lft = true;
    //        }

    //        for (int i = 0; i < CheckGensize.grid.Count; i++)
    //        {
    //            //for checkign how many conected
    //            if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.forward), out hit, dist) && isConected == true)
    //            {
    //                hit.transform.gameObject.GetComponent<IstvanRoomGenDelete>().isConected = true;
    //                Debug.DrawRay(this.transform.position, transform.TransformDirection(Vector3.up) * 50, Color.magenta, 100f);
    //            }

    //            if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.right), out hit, dist) && isConected == true)
    //            {
    //                hit.transform.gameObject.GetComponent<IstvanRoomGenDelete>().isConected = true;
    //                Debug.DrawRay(this.transform.position, transform.TransformDirection(Vector3.up) * 50, Color.magenta, 100f);
    //            }

    //            if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.back), out hit, dist) && isConected == true)
    //            {
    //                hit.transform.gameObject.GetComponent<IstvanRoomGenDelete>().isConected = true;
    //                Debug.DrawRay(this.transform.position, transform.TransformDirection(Vector3.up) * 50, Color.magenta, 100f);
    //            }

    //            if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.left), out hit, dist) && isConected == true)
    //            {
    //                hit.transform.gameObject.GetComponent<IstvanRoomGenDelete>().isConected = true;
    //                Debug.DrawRay(this.transform.position, transform.TransformDirection(Vector3.up) * 50, Color.magenta, 100f);
    //            }
    //        }

    //        corners = 0;

    //        if (fwd == true)
    //        {
    //            corners++;
    //        }
    //        if (rgt == true)
    //        {
    //            corners++;
    //        }
    //        if (bck == true)
    //        {
    //            corners++;
    //        }
    //        if (lft == true)
    //        {
    //            corners++;
    //        }


    //        /*if(lft && rgt && fwd && bck == false)
    //        {
    //            Destroy(gameObject);
    //        }*/
    //        /*Debug.DrawRay(this.transform.position, transform.TransformDirection(Vector3.forward) * 10, Color.red);
    //        Debug.DrawRay(this.transform.position, transform.TransformDirection(Vector3.right) * 10, Color.green);
    //        Debug.DrawRay(this.transform.position, transform.TransformDirection(Vector3.back) * 10, Color.blue);
    //        Debug.DrawRay(this.transform.position, transform.TransformDirection(Vector3.left) * 10, Color.white);*/
    //    }

    //    if (Input.GetKeyDown("h"))
    //    {


    //        if (corners == 4) //just spawn a 4way
    //        {
    //            Instantiate(r5, this.transform.position, Quaternion.identity);
    //        }

    //        if (corners == 3) // just spawn a 3way + rotate
    //        {
    //            if (fwd == false)
    //            {
    //                Instantiate(r4, this.transform.position, Quaternion.Euler(0, 90, 0));
    //            }
    //            if (rgt == false)
    //            {
    //                Instantiate(r4, this.transform.position, Quaternion.Euler(0, 180, 0));
    //            }
    //            if (bck == false)
    //            {
    //                Instantiate(r4, this.transform.position, Quaternion.Euler(0, 270, 0));
    //            }
    //            if (lft == false)
    //            {
    //                Instantiate(r4, this.transform.position, Quaternion.Euler(0, 0, 0));
    //            }
    //        }

    //        if (corners == 2)//find L or straight + rotate
    //        {
    //            if (fwd && bck == true || lft && rgt == true)
    //            {
    //                Debug.Log("straight");
    //                if (fwd == true)
    //                {
    //                    Instantiate(r3, this.transform.position, Quaternion.Euler(0, 0, 0));
    //                }

    //                if (rgt == true)
    //                {
    //                    Instantiate(r3, this.transform.position, Quaternion.Euler(0, 90, 0));
    //                }

    //            }

    //            else
    //            {
    //                Debug.Log("L");
    //                if (fwd == true)
    //                {
    //                    if (rgt == true)
    //                    {
    //                        Instantiate(r2, this.transform.position, Quaternion.Euler(0, 0, 0));
    //                    }

    //                    else
    //                    {
    //                        Instantiate(r2, this.transform.position, Quaternion.Euler(0, 270, 0));
    //                    }
    //                }

    //                if (bck == true)
    //                {
    //                    if (rgt == true)
    //                    {
    //                        Instantiate(r2, this.transform.position, Quaternion.Euler(0, 90, 0));
    //                    }

    //                    else
    //                    {
    //                        Instantiate(r2, this.transform.position, Quaternion.Euler(0, 180, 0));
    //                    }
    //                }
    //            }
    //        }

    //        if (corners == 1 & !isBeegBoss & !start)//spawn a 1 + rotate
    //        {
    //            if (fwd == true)
    //            {
    //                Instantiate(r1, this.transform.position, Quaternion.Euler(0, 0, 0));
    //            }

    //            if (rgt == true)
    //            {
    //                Instantiate(r1, this.transform.position, Quaternion.Euler(0, 90, 0));
    //            }

    //            if (bck == true)
    //            {
    //                Instantiate(r1, this.transform.position, Quaternion.Euler(0, 180, 0));
    //            }

    //            if (lft == true)
    //            {
    //                Instantiate(r1, this.transform.position, Quaternion.Euler(0, 270, 0));
    //            }
    //        }

    //        if (corners == 1 && isBeegBoss & !start)//spawn end + rotate
    //        {
    //            if (fwd == true)
    //            {
    //                Instantiate(r6, this.transform.position, Quaternion.Euler(0, 0, 0));
    //            }

    //            if (rgt == true)
    //            {
    //                Instantiate(r6, this.transform.position, Quaternion.Euler(0, 90, 0));
    //            }

    //            if (bck == true)
    //            {
    //                Instantiate(r6, this.transform.position, Quaternion.Euler(0, 180, 0));
    //            }

    //            if (lft == true)
    //            {
    //                Instantiate(r6, this.transform.position, Quaternion.Euler(0, 270, 0));
    //            }
    //        }

    //        if (corners == 1 & !isBeegBoss && start)//spawn start + rotate
    //        {
    //            if (fwd == true)
    //            {
    //                Instantiate(r7, this.transform.position, Quaternion.Euler(0, 0, 0));
    //            }

    //            if (rgt == true)
    //            {
    //                Instantiate(r7, this.transform.position, Quaternion.Euler(0, 90, 0));
    //            }

    //            if (bck == true)
    //            {
    //                Instantiate(r7, this.transform.position, Quaternion.Euler(0, 180, 0));
    //            }

    //            if (lft == true)
    //            {
    //                Instantiate(r7, this.transform.position, Quaternion.Euler(0, 270, 0));
    //            }
    //        }

    //        //BELOW THIS ARE THE ERROR STATES

    //        if (corners > 4)
    //        {
    //            Debug.LogWarning(this.name + " is reading more than 4 doors!");
    //        }

    //        if (corners < 1)
    //        {
    //            Debug.LogWarning(this.name + " is reading less than 1 door!");
    //        }
    //    }
    }
}