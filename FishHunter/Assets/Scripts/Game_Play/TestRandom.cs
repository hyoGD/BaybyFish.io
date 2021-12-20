using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TestRandom : MonoBehaviour
{
    public TextAsset jsonTxt;
    public PlayerList myPlayerlist = new PlayerList();
    public List<string> litss = new List<string>();
    public string only_name;
    //  public Text name;
    // Start is called before the first frame update
    private void Awake()
    {
       
    }
    void Start()
    {
        myPlayerlist = JsonUtility.FromJson<PlayerList>(jsonTxt.text);
      
    }

    // Update is called once per frame
    void Update()
    {
       

    }
    


}
