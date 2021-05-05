using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spin : MonoBehaviour
{
    RectTransform z;
    private void Start()
    {
        z = gameObject.GetComponent<RectTransform>();
    }

    private void Update()
    {
        z.Rotate(new Vector3(0, 0, 10));
    }
}
