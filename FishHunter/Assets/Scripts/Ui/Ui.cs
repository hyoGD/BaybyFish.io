using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class Ui : MonoBehaviour
{
    public ScrollSnapRect swpie;
    public ChangeSkin changeSkin;
    public GameObject Panel_shop;
   // public GameObject EffecPlayfab;

    public string yourName;

    public InputField input_F;
    public TextMeshProUGUI Welcom;
    // public Text highScore;
    public Button unlock, select;


    public TextMeshProUGUI txtProfile;
    public string[] profile;
    public int n;
    public ListSkin[] Listskin;

    // public List<string> list = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
      
        if (!PlayerPrefs.HasKey("YOURNAME"))
        {

            input_F.text = "Player";
           
        }
        else
        {
            //if (PlayerPrefs.GetString("YOURNAME") == "")
            //{
            //    input_F.text = "Player";
            //}
            //else
            //{
                input_F.text = PlayerPrefs.GetString("YOURNAME");
           // }
        }


        if (!PlayerPrefs.HasKey("SELECTED"))
        {
            PlayerPrefs.SetInt("SELECTED", 0);
            Load_selected();
        }
        else
        {
            Load_selected();
        }
        foreach (ListSkin l in Listskin)
        {
            if (l.gift == 0)
            {
                l.bought = true;
            }
            else
            {
                l.bought = PlayerPrefs.GetInt(l.name, 0) == 0 ? false : true;  // luu skin da mua
            }
        }
        profile = new string[16];
        ADDlist();
      //  Instantiate(EffecPlayfab);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.Backspace))
        {
            PlayerPrefs.DeleteAll();
        }

        updateUI();

    }
    public void Shop()
    {
        Panel_shop.SetActive(true);
    
    }
    public void Exit_shop()
    {
        Panel_shop.SetActive(false);
        changeSkin.FirtSkin();
    }

    public void Enter_Name()
    {
        yourName = input_F.text;
        Welcom.text = "Welcome " + yourName + "...";
        if (input_F.text.Length == 0)
        {
            Welcom.text = "Welcome Player";
            input_F.text = "Player";
        }
        PlayerPrefs.SetString("YOURNAME", yourName);

        StartCoroutine(PlayGame());
    }

    IEnumerator PlayGame()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
    }

    private void updateUI()
    {
        n = swpie._currentPage;
        ListSkin lits = Listskin[n];
        if (lits.bought)
        {
            unlock.gameObject.SetActive(false);
            if (s==n)
            {
                select.gameObject.SetActive(false);
               // selected = true;
            }
            else
            {
                select.gameObject.SetActive(true);
              //  selected = false;
            }
        }
        else
        {
            unlock.gameObject.SetActive(true);
            select.gameObject.SetActive(false);
        }

        for (int i = 0; i < profile.Length; i++)
        {
            if (i == n)
            {
                txtProfile.text = profile[i];
            }
        }
    }

    public void ADDlist()
    {
        profile[0] = "Giới thiệu thông tin fish 1";
        profile[1] = "Giới thiệu thông tin fish 2";
        profile[2] = "Giới thiệu thông tin fish 3";
        profile[3] = "Giới thiệu thông tin fish 4";
        profile[4] = "Giới thiệu thông tin fish 5";
        profile[5] = "Giới thiệu thông tin fish 6";
        profile[6] = "Giới thiệu thông tin fish 7";
        profile[7] = "Giới thiệu thông tin fish 8";
        profile[8] = "Giới thiệu thông tin fish 9";
        profile[9] = "Giới thiệu thông tin fish 10";
        profile[10] = "Giới thiệu thông tin fish 11";
        profile[11] = "Giới thiệu thông tin fish 12";
        profile[12] = "Giới thiệu thông tin fish 13";
        profile[13] = "Giới thiệu thông tin fish 14";
        profile[14] = "Giới thiệu thông tin fish 15";
        profile[15] = "Giới thiệu thông tin fish 16";
    }
    public void showRewarded()
    {
        ////  Adsmanager.Instance.ShowVideoReward((b) =>
        //       {
        //           if (b)
        //           {
        ListSkin lits = Listskin[n];
        lits.bought = true;
        PlayerPrefs.SetInt(lits.name, 1);
        Debug.LogWarning("Unlock");
        //           }
        //       });
    }

    public void ShowInter()
    {
        //  Adsmanager.Instance.ShowInters();
    }
    public int s;
    public bool selected = false;
    public void SelectSkin()
    {

        s = n;
        PlayerPrefs.SetInt("selectOption", n);
       // Save_selected();
        //if (selected == false)
        //{
        //    selected = true;
        //}

        // Update_select();
          Save_selected();

        //audioS.clip = clik;
        //audioS.Play();

    }
    public void Update_select()
    {

        if (selected == false)
        {
            select.gameObject.SetActive(true);
            // Selected_skin.SetActive(false);
        }
        else
        {
            //Selected_skin.SetActive(true);
            select.gameObject.SetActive(false);
        }
    }


    private void Load_selected()
    {
        // selected = PlayerPrefs.GetInt("SELECTED") == 1;
        s = PlayerPrefs.GetInt("SELECTED", 0);
    }

    private void Save_selected()
    {
        // PlayerPrefs.SetInt("SELECTED", selected ? 1 : 0);
        PlayerPrefs.SetInt("SELECTED", s);
    }
}
