using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowItem : MonoBehaviour
{
    public GameObject[] items;
    private PolygonCollider2D poly;
    public int solupngItem;
   // public Boss boss;
    // Start is called before the first frame update
    void Start()
    {
        KhoitaoItem();
        for (int i = 0; i < items.Length; i++)
        {
            if (!items[i].activeSelf)
            {
                Destroy(items[i]);
            }
        }
        }
    // Update is called once per frame
    void Update()
    {
        if (items[solupngItem] == null)
        {
            Destroy(gameObject,5);
            Debug.Log("Destroy items");
        }
    }

    void KhoitaoItem()
    {     
       if (enemyAI.instance2.scaleCurrently.x == Vector2.one.x )
        {
            if (Player.instance.Kill < 6)
            {
                solupngItem = Random.Range(1, 3);
            }
            else
            {
                solupngItem = Random.Range(2, 4);
            }

        }
        else
        {
            if (Player.instance.Kill < 6)
            {
                solupngItem = Random.Range(1, 3);
            }
            if (Player.instance.Kill < 12)
            {
                solupngItem = Random.Range(2, 4);
            }

            if (Player.instance.Kill < 18)
            {
                solupngItem = Random.Range(4, 7);
            }
            else
            {
                solupngItem = Random.Range(6, items.Length);
            }
        }

        for (int i = 0; i <= solupngItem; i++)
        {
            items[i].SetActive(true);
        }
    }

   
}

