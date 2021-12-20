using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAI : MonoBehaviour
{
    public static enemyAI instance2;
    private void Awake()
    {
        if (instance2 == null)
        {
            // DontDestroyOnLoad(this);
            instance2 = this;
        }
    }
    public ParticleSystem dust;

    public float speed;
    // public float checkRadius;
    public float attackRadius;
    public TextMesh txt;
    public bool isFacingLeft;
    public bool isAgro = false;

    public bool isSearching;

    public Transform castPoint1;
    public Transform castPoint2;
    public GameObject Wepon;
    public GameObject diePlayFab;
    public GameObject shield;
    public GameObject gamecontroller;
    public GameObject ranNames;

    public LayerMask whatIsplayer, attackRanger;

    public Transform player;
    private Rigidbody2D rb;
    private PolygonCollider2D Cricle;
    public PolygonCollider2D listwepon;
    private Vector2 movement;

    public bool isInAttackRange;
    public Vector2 minPos;
    public Vector2 maxPos;

    public Vector2 vstart, vEnd;
    public bool check = false; //attack = false;
    public float speedAuto;
    public float timeNumber;

    public GameObject[] cc;
    public int n;

    public bool die;
    //public bool nhapnhay;
    //public bool reddy;
    float ran1;
    public Vector2 scaleCurrently;


    public Transform plSpine;
    public PlayerSpine pl;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Cricle = plSpine.GetComponent<PolygonCollider2D>();
        listwepon = Wepon.GetComponent<ListHorn>().poly;
      //  pl.anim.GetComponent<MeshRenderer>().sortingOrder = 1;
        if (gamecontroller == null)
        {
            gamecontroller = GameObject.FindGameObjectWithTag("GameController");
        }
        if (ranNames == null)
        {
            ranNames = GameObject.FindGameObjectWithTag("RandomNames");
        }

        ShowName();

        n = Random.Range(0, 16);
        pl.n = n;
        if (n > 11 || n == 3)
        {
            Wepon.transform.position += new Vector3(0.5f, 0, 0);
        }
        else if (n == 6 || n == 7)
        {
            Wepon.transform.position += new Vector3(0.2f, 0, 0);
        }

        if (Player.instance.Kill < 10 || Player.instance.Kill % 5 == 0)
        {
            // reddy = true;
            /* khi bắt đầu trò chơi enemy sẽ bị loại bỏ các box va chạm theo thời gian hợp lý và sử dụng biến bool để check */
            // reddy = true;

            listwepon.enabled = false;
            Cricle.enabled = false;
            shield.SetActive(true);
            Invoke("NotReddy", 3);
        }

        else if (Player.instance.Kill >= 10)  /*so sánh nếu số điểm kill lớn hơn 10 thì sẽ tăng kich thuoc của enemy theo biến random*/
        {
            ran1 = Random.Range(1.2f, 1.5f);
            float ran2 = ran1;
            float ranW1 = ran1 / 2 + 1f;
            float ranW2 = ran2;

            plSpine.transform.localScale = new Vector2(ran1, ran2);
            Wepon.transform.localScale = new Vector2(ranW1, ranW2 - 0.3f);
            //Debug.LogWarning($"ran1:{ran1}, ran2:{ran2}");
            //  reddy = false;
            NotReddy();
        }


        // Wepon.gameObject.transform.parent = cc[n].gameObject.transform;
        castPoint2.gameObject.SetActive(false);
        castPoint2.gameObject.transform.parent = plSpine.gameObject.transform;

        scaleCurrently = plSpine.transform.localScale; /*khởi tạo scale tại vị trí ban đầu*/

        /*khởi tạo vị trí đầu và vị trí cuối theo tọa độ random min và max */
        vstart = new Vector3(Random.Range(minPos.x, maxPos.x), Random.Range(minPos.y, maxPos.y));

        vEnd = new Vector3(Random.Range(minPos.x, maxPos.x), Random.Range(minPos.y, maxPos.y));
        //vEnd = player.position;

        dust.Play();
        var main = dust.main;
        main.loop = true;


    }

    private void OnEnable()
    {
        if (Player.instance != null)
        {
            player = Player.instance.transform;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (die)
        {
            return;
        }
        isInAttackRange = Physics2D.OverlapCircle(rb.position, attackRadius, attackRanger); /*tạo 1 biến mặt lạ để check phạm vi theo True or Fales */

        if (CanSeePlayer())  /*hàm bool tại hàm sử lý phát hiện bằng RayCastHit2D */
        {
            isAgro = true;
        }
        else
        {
            if (isAgro)
            {
                if (!isSearching)
                {
                    isSearching = true;
                    Invoke("StopChasingPlayer", 0f);  //goi ham stop... sau 3s                 
                }
            }
        }
        if (isAgro)
        {
            RotateEnemy();
        }

    }


    private void FixedUpdate()
    {
        if (die)
        {
            return;
        }
        timeNumber += Time.deltaTime;
        if (timeNumber >= 10f)
        {
            vstart = new Vector2(Random.Range(minPos.x, maxPos.x), Random.Range(minPos.y, maxPos.y));
            //vEnd = new Vector2(Random.Range(minPos.x, maxPos.x), Random.Range(minPos.y, maxPos.y));
            vEnd = player.transform.position;
            timeNumber = 0;
        }
        if (Vector2.Distance(new Vector2(vstart.x, 0), new Vector2(vEnd.x, 0)) < 10)
        {
            vEnd = new Vector2(Random.Range(minPos.x, maxPos.x), Random.Range(minPos.y, maxPos.y));
            Debug.Log("Vend < 10 => change Vend");
        }


        AutoMove();
        ExitAgro();
    }

    void ExitAgro()
    {
        var main = dust.main;
        var emission = dust.emission;

        if (isAgro) /* nếu như đã phát hiện enmy sẽ lao đến tấn công */
        {

            ChasePlayer(movement);
            main.simulationSpeed = 5;
            //  main.maxParticles = 50;

            emission.rateOverTime = 20;
            pl.PlayAninmation(pl.bootSpeed);
            Invoke("StopChasingPlayer", 3);

        }

        else
        {
            //var main = dust.main;
            //main.loop = false;
            if (isInAttackRange)
            {
                //  var main = dust.main;              
                main.simulationSpeed = 5;
                // main.maxParticles = 50;
                // var emission = dust.emission;
                emission.rateOverTime = 20;
                speedAuto = 7.5f;

                pl.PlayAninmation(pl.bootSpeed);
            }
            else
            {
                // var main = dust.main;             
                main.simulationSpeed = 1;
                //  main.maxParticles = 20;
                //var emission = dust.emission;
                emission.rateOverTime = 10;

                speedAuto = 3;

                pl.PlayAninmation(pl.moving);
            }

        }

    }

    private void OffisAgro()
    {
        isAgro = false;
    }

    #region Auto move Enemy theo vector đầu và vector cuối!
    private void AutoMove()
    {
        if (!isAgro)
        {
            if (!check)
            {
                if (Vector3.Distance(transform.position, vEnd) <= 0.1f)
                {
                    check = true;
                    // dust.Play();
                }
                else
                {
                    RotateEnemy();
                    transform.position = Vector3.MoveTowards(transform.position, vEnd, Time.deltaTime * speedAuto);
                }
            }
            else
            {
                if (Vector3.Distance(transform.position, vstart) <= 0.1f)
                {
                    check = false;
                    // dust.Play();
                }
                else
                {
                    RotateEnemy();

                    transform.position = Vector3.MoveTowards(transform.position, vstart, Time.deltaTime * speedAuto);
                }
            }
            //var main = dust.main;
            //main.simulationSpeed = 1;          
        }
    }
    #endregion

    void RotateEnemy()
    {
        if (isAgro)
        {
            Vector2 lookdir = (player.position - transform.position);
            float angle = Mathf.Atan2(lookdir.y, lookdir.x) * Mathf.Rad2Deg;
            var lookRotation = Quaternion.Euler(angle * Vector3.forward);
            //  plSpine.transform.rotation = lookRotation;
            plSpine.transform.rotation = Quaternion.Lerp(plSpine.rotation, lookRotation, Time.deltaTime * 10f);
            plSpine.transform.localScale = transform.position.x < player.position.x ? scaleCurrently : new Vector2(scaleCurrently.x, -scaleCurrently.y);
            // txt.transform.localScale = transform.position.x < player.position.x ? new Vector2(2, 2) : new Vector2(-2, 2);
            lookdir.Normalize();
            movement = lookdir;
        }
        if (!check)
        {
            Vector3 lookdir = (vEnd - rb.position).normalized;
            float angle = Mathf.Atan2(lookdir.y, lookdir.x) * Mathf.Rad2Deg;
            var lookRotation = Quaternion.Euler(angle * Vector3.forward);
          //  Quaternion targetRotation = Quaternion.LookRotation(lookdir);
            plSpine.transform.rotation = Quaternion.Lerp(plSpine.rotation, lookRotation, Time.deltaTime * 10f);
            // plSpine.transform.rotation = lookRotation;
            plSpine.transform.localScale = transform.position.x < vEnd.x ? scaleCurrently : new Vector2(scaleCurrently.x, -scaleCurrently.y)/*new Vector2(0.5f, -0.5f)*/;
            //  txt.transform.localScale = transform.position.x < vEnd.x ? new Vector2(2, 2) : new Vector2(-2, 2);
            //lookdir.Normalize();
        }
        else
        {
            Vector3 lookdir = (vstart - rb.position).normalized;
            float angle = Mathf.Atan2(lookdir.y, lookdir.x) * Mathf.Rad2Deg;
            var lookRotation = Quaternion.Euler(angle * Vector3.forward);
           
            plSpine.transform.rotation = Quaternion.Lerp(plSpine.rotation, lookRotation, Time.deltaTime * 10f);
         // plSpine.transform.rotation = lookRotation;
            plSpine.transform.localScale = rb.position.x < vstart.x ? scaleCurrently : new Vector2(scaleCurrently.x, -scaleCurrently.y);
            //  txt.transform.localScale = transform.position.x < vstart.x ? new Vector2(2, 2) : new Vector2(-2, 2);
            // lookdir.Normalize();
         
        }
    }

    private void ChasePlayer(Vector2 direction)
    {
        if (Vector3.Distance(transform.position, player.position) > 1f)
        {
            rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
        }
        else
        {
            StopChasingPlayer();
        }


    }
    private void StopChasingPlayer()
    {
        isAgro = false;
        isSearching = false;
        // rb.velocity = new Vector2(0, 0);

    }

    #region Tạo một tia raycas từ 2 vị trí 1 và 2 để có thẻ xác định đc tầm cần tấn công
    private bool CanSeePlayer(/*float distance*/)
    {
        bool val = false;
        //float castDist = distance;
        //if (isFacingLeft)
        //{
        //    castDist = -distance;
        //}
        // Vector2 endPos = castPoint1.position + Vector3.right * castDist;
        Vector2 endPos = castPoint2.position;
        //raycast check
        RaycastHit2D hit = Physics2D.Linecast(castPoint1.position, endPos, 1 << LayerMask.NameToLayer("Action"));
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player") /*|| hit.collider.gameObject.CompareTag("Item")*/)
            {
                val = true;
            }
            else
            {
                val = false;
            }

            Debug.DrawLine(castPoint1.position, hit.point, Color.yellow);
        }
        else
        {
            //Debug.DrawLine(castPoint1.position, endPos, Color.blue);
            Debug.DrawLine(castPoint1.position, endPos, Color.blue);
        }
        //end ratcast check
        return val;
    }
    #endregion


   

    #region cancel
    /*
    IEnumerator Youreddy()
    {
        if (reddy)
        {
            yield return new WaitForSeconds(0.2f);
            if (!nhapnhay)
            {
                plSpine.gameObject.SetActive(true);
                Wepon.gameObject.SetActive(true);

            }
            else
            {
                plSpine.gameObject.SetActive(false);
                Wepon.gameObject.SetActive(false);
            }
            nhapnhay = !nhapnhay;
            StartCoroutine(Youreddy());

        }
        
    }
    */
    #endregion

    private void NotReddy()
    {
        // reddy = false;
        // nhapnhay = false;
        shield.SetActive(false);
        listwepon.enabled = true;
        Cricle.enabled = true;
        castPoint2.gameObject.SetActive(true);
        // plSpine.gameObject.SetActive(true);
        //  dust.Play();
       // pl.anim.GetComponent<MeshRenderer>().sortingOrder = 2;
    }


    #region Name of Enemy
    void ShowName()
    {
        var ran = ranNames.GetComponent<TestRandom>();
        var lastname = ran.myPlayerlist.AllName[Random.Range(0, ran.myPlayerlist.AllName.Length)];

        string name = lastname.ListName[Random.Range(0, lastname.ListName.Length)];
        //while (!ran.litss.Contains(name))
        //{
        //    ran.litss.Add(name);
        //    txt.text = name;
        //}
          
            if (!ran.litss.Contains(name))
            {
               // name = ran.litss[i];
                ran.litss.Add(name);
               
                txt.text = name;
            }
            else
            {
                Destroy(gameObject);
            }
        
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Wepon") || collision.gameObject.tag.Equals("Weponn"))
        {
            die = true;
            GameObject effect = transform.position.x > 0 ? Instantiate(diePlayFab, transform.position + Vector3.one, Quaternion.identity) : Instantiate(diePlayFab, transform.position - Vector3.one, Quaternion.identity);
            Wepon.SetActive(false);
            listwepon.enabled = false;
            pl.PlayAninmation(pl.die);
            pl.anim.timeScale = 0.5f;
            pl.anim.GetComponent<MeshRenderer>().sortingOrder = 0;
            // plSpine.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            Cricle.enabled = false;
            Destroy(gameObject, 0.5f);

            if (gamecontroller.GetComponent<GameController>().enemyList.Count >= 10)
            {
                gamecontroller.GetComponent<GameController>().enemyList.RemoveAt(gamecontroller.GetComponent<GameController>().enemyList.Count - 1);
                //  Player.instance.enemyList.RemoveAll(Player.instance.enemyList.Remove);
            }
            var ran = ranNames.GetComponent<TestRandom>();
            ran.litss.RemoveAt(Random.Range(0, ran.litss.Count - 1));

        }
        if (collision.gameObject.tag.Equals("Item"))
        {

            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag.Equals("Player")|| collision.gameObject.tag.Equals("Enemy"))
        {
            scaleCurrently += new Vector2(0.1f, 0.1f);
        }

    }
}
