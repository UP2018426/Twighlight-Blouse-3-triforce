using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DetectionIndicator : MonoBehaviour
{
    public Image image;
    private Camera cam;
    public Transform Enemy;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        image.transform.position = cam.WorldToScreenPoint(Enemy.transform.position);
    }

    private void CheckOnScreen()
    {
        float thing = Vector3.Dot((Enemy.position - cam.transform.position).normalized, cam.transform.forward);

        if (thing <= 0)
        {
            ToggleUI(false);
        }
        else
        {
            ToggleUI(true);
            transform.position = cam.WorldToScreenPoint(Enemy.position);
        }
    }

    private void ToggleUI(bool _value)
    {
        image.enabled = _value;
    }
}
