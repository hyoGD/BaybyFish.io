using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour
{
    public GameObject[] kills;

    public int n=0;

   // public int N { get => n; set => n = value; }

    // Start is called before the first frame update
    void Start()
    {
       
        foreach(GameObject kill in kills)
        {
            kill.SetActive(false);
        }

        kills[n].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

       

    }

 
}
