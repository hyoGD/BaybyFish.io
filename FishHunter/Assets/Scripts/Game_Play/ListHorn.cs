using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListHorn : MonoBehaviour
{
    public GameObject[] listHorn;
    public PolygonCollider2D poly;
    public int n;
    // Start is called before the first frame update

    private void Awake()
    {
        n = Random.Range(0, listHorn.Length - 1);
        foreach (GameObject horn in listHorn)
        {
            horn.SetActive(false);
        }
        listHorn[n].SetActive(true);
        poly = listHorn[n].GetComponent<PolygonCollider2D>();
    }
    void Start()
    {
       
        //n = Random.Range(0, listHorn.Length - 1);
        //foreach(GameObject horn in listHorn)
        //{
        //    horn.SetActive(false);
        //}
        //listHorn[n].SetActive(true);
        //poly = listHorn[n].GetComponent<PolygonCollider2D>();
        //  poly.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        changeHorn();   
    }

    void changeHorn()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            listHorn[n].SetActive(false);
            n++;
            if (n == listHorn.Length)
            {
                n = 0;
            }
            listHorn[n].SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            listHorn[n].SetActive(false);
            n--;
            if (n == -1)
            {
                n = listHorn.Length - 1;
            }
            listHorn[n].SetActive(true);
        }
    }
}
