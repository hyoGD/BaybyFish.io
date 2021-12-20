using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{

    public Transform player;
    public Vector3 offset;
    public bool check;
   // public Vector2 minPos, maxPos;

    public float smooth;

    private Vector3 velocity = Vector3.zero;

    private void FixedUpdate()
    {
        Vector3 newPosition = player.position + offset;
       // newPosition.y = transform.position.y;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smooth);
        //if (check)
        //{
        //    transform.position = new Vector3(Mathf.Clamp(transform.position.x, minPos.x, maxPos.x),
        //        Mathf.Clamp(transform.position.y,transform.position.y, transform.position.y),
        //        Mathf.Clamp(transform.position.z, transform.position.z, transform.position.z));
        //}

        //transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);

    }
    private void LateUpdate()
    {
        //Vector3 camfl = player.position + offset;
       // transform.position = Vector3.SmoothDamp(transform.position, camfl, ref velocity, smooth)

       

      
    }
}
