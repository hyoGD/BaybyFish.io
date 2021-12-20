using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class Player : MonoBehaviour
{
    public static Player instance;

    private void Awake()
    {
        if (instance == null)
        {
            // DontDestroyOnLoad(this);
            instance = this;
        }
    }

    public ParticleSystem dust;     // hieu ung
    public Slider slie;                 //slider boots

    public MovementJoysitck joystick;
    public GameController gamecontroller;

    private Rigidbody2D rb;
    private PolygonCollider2D polygon;
    public PolygonCollider2D listwepon;

    public TextMesh input_F;
    // public Text Score;
    public TextMeshProUGUI highScore;
    public TextMeshProUGUI meatTxt;

    private GameObject obj;
    public GameObject weapon;
    public GameObject panelGameOver;
    // public GameObject bubble;
    public GameObject shield;
    // public GameObject rain;

    public Transform playerSpine;
    public float speed;
    // public float speedSlow;
    private int kill;
    public int Kill { get => kill; set => kill = value; }
    public int meatNum;
    //public GameObject[] Fishs;
    public int n;

    public bool FacingRight = true;
    public bool die = false;

    public Vector2 minPos;
    public Vector2 maxPos;
    public Vector3 scaleCurrently;



    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        polygon = playerSpine.GetComponent<PolygonCollider2D>();

        n = PlayerPrefs.GetInt("selectOption", 0);
        var Spine = playerSpine.GetComponent<PlayerSpine>();
        Spine.n = n;
        if (n > 11 || n == 3)
        {
            weapon.transform.position += new Vector3(0.4f, 0, 0);

        }
        else if (n == 6 || n == 7)
        {
            weapon.transform.position += new Vector3(0.2f, 0, 0);

        }

        if (!PlayerPrefs.HasKey("YOURNAME"))
        {

            input_F.text = "Player";
        }
        else
        {
            input_F.text = PlayerPrefs.GetString("YOURNAME");
        }
        scaleCurrently = playerSpine.transform.localScale;
        dust.transform.parent = playerSpine.transform;

        weapon.gameObject.transform.parent = playerSpine.gameObject.transform;
        transform.position = new Vector3(Random.Range(minPos.x, maxPos.x), Random.Range(minPos.y, maxPos.y), transform.position.z);



        listwepon = weapon.GetComponent<ListHorn>().poly;
        listwepon.enabled = false;
        polygon.enabled = false;

        shield.SetActive(true);
        Invoke("NotReddy", 3);

        CreatDust();
        var main = dust.main;
        main.loop = true;


    }
    public bool check;
    private void FixedUpdate()
    {

        if (check)
        {
            rb.position = new Vector3(Mathf.Clamp(transform.position.x, minPos.x, maxPos.x),
                         Mathf.Clamp(transform.position.y, minPos.y, maxPos.y),
                         Mathf.Clamp(transform.position.z, transform.position.z, transform.position.z));
        }
        if (input_F.text.Length == 0)
        {
            input_F.text = "Player";
        }
        if (!die)
        {

            if (joystick.joysiickVec.y != 0)
            {
                rb.velocity = new Vector2(joystick.joysiickVec.x * speed, joystick.joysiickVec.y * speed);
                //  CreatDust();
            }
            else
            {
                if (rb.velocity.x == 0)
                {
                    rb.velocity = new Vector2(+speed, rb.velocity.y);

                }
                else
                {

                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
                   
                }
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Sta && !die)
        {
            var Spine = playerSpine.GetComponent<PlayerSpine>();
            var main = dust.main;
            var emission = dust.emission;
            slie.value -= 0.01f;
            if (slie.value < 0.01)
            {
                speed = 3;
                // main.loop = false;
                main.simulationSpeed = 1;
                //  main.maxParticles = 20;
                emission.rateOverTime = 10;
                Spine.PlayAninmation(Spine.moving);
                // main.startLifetime = 2;

                gamecontroller.UnRush();
            }
            else
            {
                speed = 7.5f;
                main.simulationSpeed = 5;
                //  main.maxParticles = 50;
                emission.rateOverTime = 20;

                Spine.PlayAninmation(Spine.bootSpeed);
            }
        }
        else
        {
            // if (boot < 100)
            // {
            slie.value += 0.01f;
            // boot += 1;
            //}
        }
        Upspeed();
        RotatePlayer();

    }

    private void RotatePlayer()
    {
        if (joystick.joysiickVec == Vector2.zero) return;
        if (!die)
        {
            //if (joystick.joysiickVec.x > 0 && !FacingRight )
            //{
            //    playerSpine.transform.rotation = Quaternion.Euler(0, 0, 0);
            //    // CreatDust();
            //    Flip();
            //    //var velocityLt = dust.velocityOverLifetime;
            //    //velocityLt.x = -0.5f;

            //    //weapon.GetComponent<SpriteRenderer>().sortingOrder = 0;
            //}
            //else if (joystick.joysiickVec.x < 0 && FacingRight)
            //{
            //    playerSpine.transform.rotation = Quaternion.Euler(0, 180, 0);
            //    //  CreatDust();
            //    Flip();
            //    //var velocityLt = dust.velocityOverLifetime;
            //    //velocityLt.x = 0.5f;

            //    //weapon.GetComponent<SpriteRenderer>().sortingOrder = 0;
            //}

            float angle = Mathf.Atan2(joystick.joysiickVec.y, joystick.joysiickVec.x) * Mathf.Rad2Deg;
            var lookRotion = Quaternion.Euler(angle * Vector3.forward);
            //  playerSpine.transform.rotation = lookRotion;
            playerSpine.transform.rotation = Quaternion.Lerp(playerSpine.rotation, lookRotion, Time.deltaTime * 20);
            playerSpine.transform.localScale = joystick.joysiickVec.x > 0 ? scaleCurrently : new Vector3(scaleCurrently.x, -scaleCurrently.y, scaleCurrently.z);


        }
        /*
        * tat ca cac doan tren dung de check khi qua quay sang trai or sang phai se chuyen vector Y cua player sang 180 do!
       */
    }
    private void Flip()
    {
        // Flips the player.
        // CreatDust();
        FacingRight = !FacingRight;
    }

    private void Upspeed()
    {
        var Spine = playerSpine.GetComponent<PlayerSpine>();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (joystick.joysiickVec.y != 0 && !die && slie.value > 0.01)
            {
                // slie.value -= 0.001f;
                Sta = true;
                speed *= 2.5f;
                var main = dust.main;
                main.simulationSpeed = 5;
                // main.maxParticles = 50;
                var emission = dust.emission;
                emission.rateOverTime = 20;
                Spine.PlayAninmation(Spine.bootSpeed);
                slie.transform.localScale = new Vector2(1f, 1.3f);

                gamecontroller.Tutorial();
                gamecontroller.Rush();
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space) && !die)
        {
            Sta = false;
            // slie.value += 0.001f;
            if (speed > 3)
            {
                speed /= 2.5f;

                var main = dust.main;
                main.simulationSpeed = 1;
                // main.maxParticles = 20;
                var emission = dust.emission;
                emission.rateOverTime = 10;
                rb.velocity = new Vector2(rb.velocity.x / 3, rb.velocity.y / 3);
                Spine.PlayAninmation(Spine.moving);
                gamecontroller.UnRush();
            }
            slie.transform.localScale = new Vector2(1, 1f);
            //gamecontroller.mute =true;
        }
    }

    public GameObject diePlayFab;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag.Equals("Item"))
        {
            slie.value += 0.1f;
            gamecontroller.EatItem();
        }
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            gamecontroller.HighScore();
            gamecontroller.Fight();
            // gamecontroller.UnrusFish();
            StartCoroutine(gamecontroller.Continue());


            if (gamecontroller.enemyList.Count >= 10)
            {
                gamecontroller.enemyList.RemoveAt(gamecontroller.enemyList.Count - 1);
            }
            //Handheld.Vibrate();
        }

        {
            if (collision.gameObject.tag.Equals("Weponn"))
            {
                die = true;
                //  Handheld.Vibrate();
                gamecontroller.UnRush();
                gamecontroller.Fight();
                gameObject.tag.Equals("");
                gameObject.layer = 0;
                rb.gravityScale = 10;
                polygon.enabled = false;
                weapon.SetActive(false);
                playerSpine.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                playerSpine.transform.localScale = scaleCurrently;
                gameObject.GetComponent<ScaleTime>().SlowMotion();
                /* khoi tao item */
                GameObject item = transform.position.x > 0 ? Instantiate(diePlayFab, transform.position + Vector3.one, Quaternion.identity) : Instantiate(diePlayFab, transform.position - Vector3.one, Quaternion.identity);
               
                gamecontroller.hightScore.text = "HIGH SCORE:                     " + PlayerPrefs.GetInt("HIGHSCORE", 0).ToString();
                dust.Stop();
                StartCoroutine(Lose());
            }
        }
    }


    void CreatDust()    //ham khoi tao goi den Partical effect
    {
        dust.Play();
    }
    public bool Sta;

    public void BootSpeed()
    {
        var Spine = playerSpine.GetComponent<PlayerSpine>();
        if (joystick.joysiickVec.y != 0 && !die)
        {
            Sta = true;
            //  slie.value -= 0.001f;
            speed *= 2.5f;

            var main = dust.main;
            main.simulationSpeed = 5;
            //main.maxParticles = 50;
            var emission = dust.emission;
            emission.rateOverTime = 20;

            Spine.PlayAninmation(Spine.bootSpeed);

            slie.transform.localScale = new Vector2(1, 1.3f);

            gamecontroller.Tutorial();
            gamecontroller.Rush();
        }
    }
    public void UnbootSpeed()
    {
        var Spine = playerSpine.GetComponent<PlayerSpine>();
        Sta = false;
        if (speed > 3f && !die)
        {
            speed /= 2.5f;

            var main = dust.main;
            main.simulationSpeed = 1;
            //   main.maxParticles = 20;
            var emission = dust.emission;
            emission.rateOverTime = 10;
            rb.velocity = new Vector2(rb.velocity.x / 3, rb.velocity.y / 3);
            Spine.PlayAninmation(Spine.moving);
            gamecontroller.UnRush();
        }
        slie.transform.localScale = new Vector2(1, 1);

    }


    IEnumerator Lose()
    {
        yield return new WaitForSeconds(2.5f);
        panelGameOver.SetActive(true);

        gamecontroller.OverTime();
        gamecontroller.Killed_enemy();
        Debug.Log("You Lose");
    }

    private void NotReddy()
    {
        // reddy = false;
        // nhapnhay = false;
        shield.SetActive(false);
        listwepon.enabled = true;
        polygon.enabled = true;
        // playerSpine.gameObject.SetActive(true);
        //  CreatDust();
    }

    public void Meat()
    {
        meatNum++;
        meatTxt.text = meatNum.ToString();
        if (meatNum > PlayerPrefs.GetInt("HIGHSCORE", 0))
        {
            PlayerPrefs.SetInt("HIGHSCORE", meatNum);
        }
    }


    #region cancel
    /* 
    void HighScore()
    {
        kill++;
        Score.text = "Kill: " + kill.ToString();
        if (kill > PlayerPrefs.GetInt("HIGHSCORE", 0))
        {
            PlayerPrefs.SetInt("HIGHSCORE", kill);
        }
        switch (kill)
        {
            case 3:
                scaleCurrently += new Vector3(0.1f, 0.1f, 0);
                weapon.transform.localScale += new Vector3(0.1f, 0f, 0);
                break;
            case 7:
                scaleCurrently += new Vector3(0.1f, 0.1f, 0);
                weapon.transform.localScale += new Vector3(0.1f, 0f, 0);
                input_F.gameObject.transform.position += Vector3.up / 2;
                break;
            case 9:
                // transform.localScale += new Vector3(0.1f, 0.1f, 0);
                // weapon.transform.localScale += new Vector3(0.1f, 0.1f, 0);
                break;
            case 11:
                scaleCurrently += new Vector3(0.1f, 0.1f, 0);
                weapon.transform.localScale += new Vector3(0.1f, 0f, 0);
                break;
            case 13:
                // transform.localScale += new Vector3(0.1f, 0.1f, 0);
                weapon.transform.localScale += new Vector3(0f, 0f, 0);
                break;
            case 15:
                scaleCurrently += new Vector3(0.1f, 0.1f, 0);
                weapon.transform.localScale += new Vector3(0.1f, 0.1f, 0);

                break;
            case 16:
                //   transform.localScale += new Vector3(0.1f, 0.1f, 0);
                // weapon.transform.localScale += new Vector3(0.05f, 0, 0);
                break;
            case 17:
                scaleCurrently += new Vector3(0.2f, 0.1f, 0);
                weapon.transform.localScale += new Vector3(0.1f, 0.1f, 0);
                break;
            case 18:
                scaleCurrently += new Vector3(0.2f, 0.1f, 0);
                input_F.gameObject.transform.position += Vector3.up / 2;
                break;
            case 19:
                //  scaleCurrently += new Vector3(0.2f, 0.1f, 0);
                weapon.transform.localScale += new Vector3(0f, 0f, 0);
                break;
            case 20:
                scaleCurrently += new Vector3(0.2f, 0.1f, 0);
                //  weapon.transform.localScale += new Vector3(0.1f, 0.1f, 0);
                // input_F.gameObject.transform.position += Vector3.up;
                break;
        }
    }
    IEnumerator Rain()
    {
        yield return new WaitForSeconds(5);
        GameObject Rain_V = Instantiate(rain, new Vector3(Random.Range(12, 49), 21, 0), Quaternion.identity);
       Destroy(Rain_V, 2);
        StartCoroutine(Rain());
    }
   

    public List<GameObject> enemyList = new List<GameObject>();
    public Transform hs;


    /*
    IEnumerator Continue()
    {

        yield return new WaitForSeconds(2);

        if (enemyList.Count <= 20)
        {
            Transform bot;
            if (FacingRight)
            {
                bot = Instantiate<Transform>(hs, new Vector2(minPos.x+10, Random.Range(minPos.y, maxPos.y)), Quaternion.identity);
            }
            else
            {
                bot = Instantiate<Transform>(hs, new Vector2(maxPos.x-10, Random.Range(minPos.y, maxPos.y)), Quaternion.identity);
            }
            enemyList.Add(bot.gameObject);

        }
    }
    


    IEnumerator InstanEnemy()
    {
        yield return new WaitForSeconds(5);
        if (enemyList.Count <= 20)
        {
            if (FacingRight)
            {
                enemyList.Add(Instantiate<GameObject>(hs.gameObject, new Vector2(transform.position.x - 10, Random.Range(minPos.y, maxPos.y)), Quaternion.identity));

            }
            else
            {
                enemyList.Add(Instantiate<GameObject>(hs.gameObject, new Vector2(transform.position.x + 10, Random.Range(minPos.y, maxPos.y)), Quaternion.identity));
            }
            Debug.Log("bonus");
            //   ListName.CheckPhanTuKotrungLAp();
            StartCoroutine(InstanEnemy());
        }
    }


    public bool nhapnhay;
    public bool reddy;


    IEnumerator Youreddy()
    {
        if (reddy)
        {
            yield return new WaitForSeconds(0.2f);
            if (!nhapnhay)
            {
                playerSpine.gameObject.SetActive(true);
                weapon.gameObject.SetActive(true);

            }
            else
            {
                playerSpine.gameObject.SetActive(false);
                weapon.gameObject.SetActive(false);
            }
            nhapnhay = !nhapnhay;
            StartCoroutine(Youreddy());
        }
    }


    private void FirsPlay()
    {

        for (int i = 0; i < 5; i++)
        {
            enemyList.Add(Instantiate<GameObject>(hs.gameObject, new Vector2(Random.Range(minPos.x, maxPos.x), Random.Range(minPos.y, maxPos.y)), Quaternion.identity));
        }
    }
    */
    #endregion
}


