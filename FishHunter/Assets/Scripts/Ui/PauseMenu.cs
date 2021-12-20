using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    public Image win, lose;
    public GameObject player;
    int n;
    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        n = PlayerPrefs.GetInt("HIGHSCORE", 0);
      //  Debug.LogError("hightScore: " + PlayerPrefs.GetInt("HIGHSCORE", 0));
    }

    private void Update()
    {
        // Debug.Log("killed: " + player.GetComponent<Player>().kill);
        //Debug.Log("killed: " + Player.instance.kill);

        if (Player.instance.Kill > n)
        {
            win.enabled = true;
            lose.enabled = false;
        }
        else
        {
            win.enabled = false;
            lose.enabled = true;
        }
    }
    public void Home(int sceneID)
    {
        Adsmanager.Instance.ShowInters();

        SceneManager.LoadScene(sceneID);
        Time.timeScale = 1;
       
    }

    public void Pause()
    {    
        pausePanel.SetActive(true);
       // Time.timeScale = 0;
    }


    public void Resume()
    {
        //Time.timeScale = 1;
        pausePanel.SetActive (false);
    }

    public void Replay(int sceneID)
    {
        Adsmanager.Instance.ShowInters();

        SceneManager.LoadScene(sceneID);
        Time.timeScale = 1;
    }


   

}
