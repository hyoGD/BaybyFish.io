using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleTime : MonoBehaviour
{
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 1)
        {
            Invoke("Normal", 0.2f);
        }
    }
    public void SlowMotion()
    {
        if (Time.timeScale == 1.0f)
        {
            Time.timeScale = 0.2f;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }
       
      //  Destroy(this, 0.6f);
    }

    public void Normal()
    {
        Time.timeScale = 1;
    }
}
