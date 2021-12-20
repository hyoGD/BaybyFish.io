using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Runtime : MonoBehaviour
{
    public TextMeshProUGUI txt_minute, txt_second;
    public float minute, second;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.instance.die) return;
        second += Time.deltaTime;
        int secondfix = (int)second;
        //Debug.Log(second);
        if (second < 10)
        {
            txt_second.text = "0" + secondfix.ToString();
        }
        else
        {
            txt_second.text = secondfix.ToString();
            if (secondfix == 60)
            {
                second = 0;
                minute++;
                if (minute < 10)
                {
                    txt_minute.text = "0" + minute.ToString()+":";
                }
                else
                {
                    txt_minute.text = minute.ToString()+":";
                }
            }
        } 
    }
  

    
}
