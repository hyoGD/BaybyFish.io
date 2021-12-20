using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bone_fish : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("HideBone", 3);
    }
    private void Update()
    {
       
    }
    void HideBone()
    {
        Destroy(gameObject);
        Debug.Log("Destroy fish_bone");
    }
}
