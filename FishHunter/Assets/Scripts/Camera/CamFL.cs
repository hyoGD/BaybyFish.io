using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFL : MonoBehaviour
{
    public Transform player;

    public Vector3 offset;

    public float smooth = 1f;

    private Vector3 velocity = Vector3.zero;

    public bool check;
    public Vector2 maxPos;
    public Vector2 minPos;

   
    // Start is called before the first frame update
    void Start()
    {
       
    }
    //private void OnEnable()
    //{
    //    if (Player.instance != null)
    //    {
    //        player = Player.instance.transform;
    //    }
    //}
    // Update is called once per frame
    private void FixedUpdate()
    {
        Vector3 camfl = player.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, camfl, ref velocity, smooth);
        if (check)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minPos.x, maxPos.x),
                Mathf.Clamp(transform.position.y, minPos.y, maxPos.y),
                Mathf.Clamp(transform.position.z, transform.position.z, transform.position.z));
        }
    }
   

}




