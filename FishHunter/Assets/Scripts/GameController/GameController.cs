using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameController : MonoBehaviour
{
    public static GameController controller;
    private void Awake()
    {
        if (controller == null)
        {
            // DontDestroyOnLoad(this);
            controller = this;
        }
    }
    public Text kill;
    //  public Text killed_txt;
    public TextMeshProUGUI minute, second, killed, score, hightScore;
    public Player player;
    public Runtime runtime;
    public GameObject bubble;
    public GameObject boss;
    // public GameObject ranger;
    public GameObject report;
    public GameObject tutorial;
    public GameObject[] kills;


    public AudioSource sound_eat, sound_fight, sound_rush, sound_warning, music;
    //  public AudioSource Music;
    public AudioClip Ocean;

    public List<GameObject> enemyList = new List<GameObject>();
    public Transform hs;
    public bool first = false;
    //public Vector2 minPos;
    //public Vector2 maxPos;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.GetInt("HIGHSCORE", 0);
        FirsPlay();
        StartCoroutine(Bubble());

        StartCoroutine(InstanEnemy());

        //  music = GetComponent<AudioSource>();
        Onmusic();
        first = PlayerPrefs.GetInt("FirstTouch") == 1;
        if (!first)
        {
            Invoke("TutorialReddy", 3);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Backspace))
        {
            PlayerPrefs.DeleteAll();
        }
        HideKill();
    }

    public void OverTime()
    {
        int secondfix = (int)runtime.second;
        if (secondfix < 10)
        {
            second.text = $"0{secondfix.ToString()}";
        }
        else
        {
            second.text = $"{secondfix.ToString()}";
        }

        int minutefix = (int)runtime.minute;
        if (minutefix < 10)
        {
            minute.text = $"0{minutefix.ToString()}:";
        }
        else
        {
            minute.text = $"{minutefix.ToString()}:";
        }

    }

    public void Killed_enemy()
    {
        if (player.Kill < 10)
        {
            killed.text = $"0{player.Kill.ToString()}";
        }
        else
        {
            killed.text = player.Kill.ToString();
        }
        if (player.meatNum < 10)
        {
            score.text = $"0{player.meatNum.ToString()}";
        }
        else
        {
            score.text = player.meatNum.ToString();
        }

        if (player.meatNum > PlayerPrefs.GetInt("HIGHSCORE", 0))
        {
            PlayerPrefs.SetInt("HIGHSCORE", player.meatNum);
        }
    }

    public void HighScore()
    {
        player.Kill++;
        kill.text = "Kill: " + player.Kill.ToString();
        Tinhkill();
        switch (player.Kill)
        {
            case 3:
                player.scaleCurrently += new Vector3(0.1f, 0.1f, 0);
                player.weapon.transform.localScale += new Vector3(0.1f, 0f, 0);
                break;
            case 6:
                //  ranger.SetActive(true);
                Instantiate(report);
                if (player.gameObject.transform.position.x > 25)
                {
                    Instantiate(boss, new Vector2(player.minPos.x + 10, Random.Range(player.minPos.y, player.maxPos.y)), Quaternion.identity);
                }
                else
                {
                    Instantiate(boss, new Vector2(player.maxPos.x - 10, Random.Range(player.minPos.y, player.maxPos.y)), Quaternion.identity);
                }
                break;
            case 7:
                player.scaleCurrently += new Vector3(0.1f, 0.1f, 0);
                player.weapon.transform.localScale += new Vector3(0.1f, 0f, 0);
                player.input_F.gameObject.transform.position += Vector3.up / 2;
                break;
            case 9:
                // transform.localScale += new Vector3(0.1f, 0.1f, 0);
                // weapon.transform.localScale += new Vector3(0.1f, 0.1f, 0);
                break;
            case 11:
                player.scaleCurrently += new Vector3(0.1f, 0.1f, 0);
                player.weapon.transform.localScale += new Vector3(0.1f, 0f, 0);
                break;
            case 12:
                Instantiate(report);
                if (player.gameObject.transform.position.x > 25)
                {
                    Instantiate(boss, new Vector2(player.minPos.x + 10, Random.Range(player.minPos.y, player.maxPos.y)), Quaternion.identity);
                }
                else
                {
                    Instantiate(boss, new Vector2(player.maxPos.x - 10, Random.Range(player.minPos.y, player.maxPos.y)), Quaternion.identity);
                }
                break;
            case 13:
                // transform.localScale += new Vector3(0.1f, 0.1f, 0);
                player.weapon.transform.localScale += new Vector3(0f, 0f, 0);
                break;
            case 15:
                player.scaleCurrently += new Vector3(0.1f, 0.1f, 0);
                player.weapon.transform.localScale += new Vector3(0.1f, 0.1f, 0);

                break;
            case 16:
                //   transform.localScale += new Vector3(0.1f, 0.1f, 0);
                // weapon.transform.localScale += new Vector3(0.05f, 0, 0);
                break;
            case 17:
                player.scaleCurrently += new Vector3(0.1f, 0.1f, 0);
                player.weapon.transform.localScale += new Vector3(0.1f, 0.1f, 0);
                break;
            case 18:
                player.scaleCurrently += new Vector3(0.1f, 0.1f, 0);
                player.input_F.gameObject.transform.position += Vector3.up / 2;
                Instantiate(report);
                if (player.gameObject.transform.position.x > 25)
                {
                    Instantiate(boss, new Vector2(player.minPos.x + 10, Random.Range(player.minPos.y, player.maxPos.y)), Quaternion.identity);
                }
                else
                {
                    Instantiate(boss, new Vector2(player.maxPos.x - 10, Random.Range(player.minPos.y, player.maxPos.y)), Quaternion.identity);
                }
                break;
            case 19:
                //  scaleCurrently += new Vector3(0.2f, 0.1f, 0);
                player.weapon.transform.localScale += new Vector3(0f, 0f, 0);
                break;
            case 20:
                player.scaleCurrently += new Vector3(0.1f, 0.1f, 0);
                //  weapon.transform.localScale += new Vector3(0.1f, 0.1f, 0);
                // input_F.gameObject.transform.position += Vector3.up;
                break;
            case 24:
                player.scaleCurrently += new Vector3(0.1f, 0.1f, 0);
                player.input_F.gameObject.transform.position += Vector3.up / 2;
                Instantiate(report);
                if (player.gameObject.transform.position.x > 25)
                {
                    Instantiate(boss, new Vector2(player.minPos.x + 10, Random.Range(player.minPos.y, player.maxPos.y)), Quaternion.identity);
                }
                else
                {
                    Instantiate(boss, new Vector2(player.maxPos.x - 10, Random.Range(player.minPos.y, player.maxPos.y)), Quaternion.identity);
                }
                break;
            case 30:
                player.scaleCurrently += new Vector3(0.1f, 0.1f, 0);
                player.input_F.gameObject.transform.position += Vector3.up / 2;
                Instantiate(report);
                if (player.gameObject.transform.position.x > 25)
                {
                    Instantiate(boss, new Vector2(player.minPos.x + 10, Random.Range(player.minPos.y, player.maxPos.y)), Quaternion.identity);
                }
                else
                {
                    Instantiate(boss, new Vector2(player.maxPos.x - 10, Random.Range(player.minPos.y, player.maxPos.y)), Quaternion.identity);
                }
                break;
        }
    }

    private void FirsPlay()
    {

        for (int i = 0; i < 5; i++)
        {
            //if (player.transform.position.y > 0)
            //{
            //    enemyList.Add(Instantiate<GameObject>(hs.gameObject, new Vector2(Random.Range(player.minPos.x, player.maxPos.x),player.minPos.y /*Random.Range(player.minPos.y, player.maxPos.y)*/), Quaternion.identity));
            //}
            //else
            //{
            //    enemyList.Add(Instantiate<GameObject>(hs.gameObject, new Vector2(Random.Range(player.minPos.x, player.maxPos.x), player.maxPos.y /*Random.Range(player.minPos.y, player.maxPos.y)*/), Quaternion.identity));
            //}
            enemyList.Add(Instantiate<GameObject>(hs.gameObject, new Vector2(Random.Range(player.minPos.x, player.maxPos.x), Random.Range(player.minPos.y, player.maxPos.y)), Quaternion.identity));
        }
    }

    IEnumerator Bubble()
    {
        yield return new WaitForSeconds(1.5f);
        Quaternion target = Quaternion.Euler(0, 0, Random.Range(0, 360));
        GameObject Bubble_V = Instantiate(bubble, new Vector3(Random.Range(-11, 63), -22, 0), target);
        Destroy(Bubble_V, 6.5f);
        StartCoroutine(Bubble());
    }

    public IEnumerator Continue()
    {

        yield return new WaitForSeconds(1);

        if (enemyList.Count <= 20)
        {
            Transform bot;
            if (player.transform.position.y > 0)
            {
                //bot = Instantiate<Transform>(hs, new Vector2(player.minPos.x /*+ 10*/, Random.Range(player.minPos.y, player.maxPos.y)), Quaternion.identity);
                bot = Instantiate<Transform>(hs, new Vector2(Random.Range(player.minPos.x, player.maxPos.x), player.minPos.y), Quaternion.identity);
            }
            else
            {
                bot = Instantiate<Transform>(hs, new Vector2(Random.Range(player.minPos.x, player.maxPos.x), player.maxPos.y), Quaternion.identity);
            }
            enemyList.Add(bot.gameObject);

        }
    }

    IEnumerator InstanEnemy()
    {
        int second;
        if (player.Kill < 15)
        {
            second = 5;
        }
        else
        {
            second = 2;
        }
        yield return new WaitForSeconds(second);
        if (enemyList.Count <= 20)
        {
            if (player.transform.position.y > 0)
            {
                enemyList.Add(Instantiate<GameObject>(hs.gameObject, new Vector2(Random.Range(player.minPos.x, player.maxPos.x), player.minPos.y), Quaternion.identity));
            }
            else
            {
                enemyList.Add(Instantiate<GameObject>(hs.gameObject, new Vector2(Random.Range(player.minPos.x, player.maxPos.x), player.maxPos.y), Quaternion.identity));
            }
            Debug.Log("bonus");
            //   ListName.CheckPhanTuKotrungLAp();
            StartCoroutine(InstanEnemy());
        }
    }


    #region Sound
    public void Fight()
    {
        // sound.clip = fight;
        sound_fight.Play();
    }

    public void EatItem()
    {
        // sound.clip = eat;
        sound_eat.Play();
    }

    public void Rush()
    {
        sound_rush.Play();
    }

    public void UnRush()
    {
        sound_rush.Stop();
    }

    public void Warning()
    {
        sound_warning.Play();
    }
    public void UnWarning()
    {
        sound_warning.Stop();
    }

    #endregion

    #region Music
    public void Onmusic()
    {
        music.clip = Ocean;
        music.Play();
    }

    #endregion


    #region Tutorial
    public void Tutorial()
    {

        first = true;
        tutorial.SetActive(false);

        PlayerPrefs.SetInt("FirstTouch", first ? 1 : 0);

    }

    void TutorialReddy()
    {
        tutorial.SetActive(true);
    }
    #endregion

    #region Hiển thị số kill
    public int n = 0;
    public void Tinhkill()
    {
        n++;
        //switch (n)
        //{
        //    case 2:
        //        kills[0].SetActive(true);
        //        break;
        //    case 3:
        //        kills[1].SetActive(true);
        //        kills[0].SetActive(false);
        //        break;
        //    case 4:
        //        kills[2].SetActive(true);
        //        kills[1].SetActive(false);
        //        break;
        //    case 5:
        //        kills[3].SetActive(true);
        //        kills[2].SetActive(false);
        //        break;
        //    case 6:
        //        kills[4].SetActive(true);
        //        kills[3].SetActive(false);
        //        break;
        //}
        for (int i = 0; i < kills.Length; i++)
        {
            if (i == n - 2)
            {
                kills[i].SetActive(true);
                if (i == kills.Length)
                {
                    n = 0;
                }
            }
            else
            {
                kills[i].SetActive(false);
            }
        }
        num = 0;
    }
    public float num;
    void HideKill()
    {
        num += Time.deltaTime;

        for (int i = 0; i < kills.Length; i++)
        {
            if (num >= 3)
            {
                kills[i].SetActive(false);
                n = 0;
                // num = 0;
            }
        }
    }
    #endregion
}
