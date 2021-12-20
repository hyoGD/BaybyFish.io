using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
   // public HowItem how;

    private SpriteRenderer sprite;
    private PolygonCollider2D poly;

    public GameObject ItemPlayFab;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        poly = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
      
        if (collision.gameObject.tag.Equals("Player") || collision.gameObject.tag.Equals("Enemy"))
        {
            GameObject AnimItem = Instantiate(ItemPlayFab, transform.position, Quaternion.identity);
            Destroy(AnimItem, 1);
          
            Destroy(gameObject);

          
           
        }
        if (collision.gameObject.tag.Equals("Player"))
        {
            Player.instance.Meat();
        }


    }

  
}

