using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getTouch : MonoBehaviour
{
    //public GameObject particle;
    public GameObject projectile;
    public GameObject clone;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            transform.position = touchPosition;
        }
    }
}

