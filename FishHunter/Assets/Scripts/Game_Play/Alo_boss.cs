using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alo_boss : MonoBehaviour
{
    // Start is called before the first frame update
   
    public GameObject ranger;

  
    bool reddy = true;
    bool nhapnhay;
    void Start()
    {
        reddy = true;
        StartCoroutine(Youreddy());
        Invoke("NotReddy", 5);
        GameController.controller.Warning();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    IEnumerator Youreddy()
    {
        if (reddy)
        {
            yield return new WaitForSeconds(0.3f);
            if (!nhapnhay)
            {
             ranger.SetActive(true);

            }
            else
            {
                ranger.SetActive(false);
            }
            nhapnhay = !nhapnhay;
            StartCoroutine(Youreddy());
        }
    }
    private void NotReddy()
    {
        reddy = false;
        nhapnhay = false;
        GameController.controller.UnWarning();
        Destroy(gameObject);
    }

}
