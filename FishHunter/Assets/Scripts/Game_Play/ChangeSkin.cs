using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSkin : MonoBehaviour
{
    public static ChangeSkin change;

    private void Awake()
    {
        if (change == null)
        {
            change = this;
           // DontDestroyOnLoad(this);
        }
    }

    public GameObject[] Skin;
    public int n;
    public float speed;
    public RectTransform pattern;
     public  Vector3 posCurrently, posContine;

   public AudioSource music;
    public AudioClip xmas;
   
  
    // Start is called before the first frame update
    void Start()
    {
        FirtSkin();
        //  pattern = GetComponent<RectTransform>();
        posCurrently = pattern.position;
        music = GetComponent<AudioSource>();
        music.clip = xmas;
        music.Play();
        if (PlayerPrefs.GetInt("MUSIC") == 1)
        {
            music.volume = 0;
        }
        else
        {
            music.volume = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //pattern.position += new Vector3(1f * Time.deltaTime * speed, 0);
        //if (Vector3.Distance(posCurrently,pattern.position) >10)
        //{
        //   pattern.position = posCurrently;
        //}
        //posContine = pattern.position;
    } 

    public void FirtSkin()
    {
        n = Random.Range(0, 16);
        PlayerPrefs.SetInt("selectOption", n);
        foreach (GameObject skinChidle in Skin)
        {
            skinChidle.SetActive(false);
        }
        Skin[n].SetActive(true);
    }
}
