using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
public class Boss : MonoBehaviour
{
    public SkeletonAnimation anim;
    [SpineAnimation] public string die, upSpeed, moving;

    public LayerMask attack;
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private PolygonCollider2D Cricle; //body boss

    public float speed;
    public float atack;
    public float timeNumber,sodem;
    public int stop, n;

    public Transform player;
    public GameObject[] Wepons;
    public GameObject itemPlayfab;
    // public HowItem howitem;

    Vector2 movement;
    private Vector2 scaleCurrently;
    public Vector2 End;
    public Vector2 Vstart;

    public bool detect, bossDie;

 
    private void OnEnable()
    {
        if (Player.instance != null)
        {
            player = Player.instance.transform;
        }
    }
    void Start()
    {
        n = Random.Range(0, Wepons.Length);
        foreach (GameObject wepon in Wepons)
        {
            wepon.SetActive(false);
        }
        Wepons[n].SetActive(true);
        rb = GetComponent<Rigidbody2D>();
        Cricle = gameObject.GetComponent<PolygonCollider2D>();
        ScaleCurrently = transform.localScale; /*khởi tạo scale tại vị trí ban đầu*/
        
        switch (Player.instance.Kill)
        {
            case 12:
                ScaleCurrently += Vector2.one / 3;
                break;
            case 18:
                ScaleCurrently += Vector2.one / 2;
                break;
            case 24:
                ScaleCurrently += Vector2.one / 1.5f;
                break;
            case 30:
                ScaleCurrently += Vector2.one;
                break;
        }
        if (Player.instance.Kill > 30)
        {
            ScaleCurrently += Vector2.one;
        }
        End = new Vector3(Random.Range(enemyAI.instance2.minPos.x, enemyAI.instance2.maxPos.x), Random.Range(enemyAI.instance2.minPos.y, enemyAI.instance2.maxPos.y));
        Vstart = new Vector3(Random.Range(enemyAI.instance2.minPos.x, enemyAI.instance2.maxPos.x), Random.Range(enemyAI.instance2.minPos.y, enemyAI.instance2.maxPos.y));
    }


    // Update is called once per frame
    void Update()
    {
        if (bossDie) return;
        detect = Physics2D.OverlapCircle(transform.position, 10, attack);

        autoMove();

      
       
    }


    public void PlayAninmation(string _strAnim)
    {
        if (!anim.AnimationName.Equals(_strAnim))
        {
            anim.AnimationState.SetAnimation(0, _strAnim, true);
        }
    }

    private void FixedUpdate()
    {
        if (bossDie) return;
        timeNumber += Time.deltaTime;
        if (timeNumber >= 5f)
        {
            End = player.transform.position;
             Vstart = new Vector2(Random.Range(enemyAI.instance2.minPos.x, enemyAI.instance2.maxPos.x), Random.Range(enemyAI.instance2.minPos.y, enemyAI.instance2.maxPos.y));
            // End = new Vector3(Random.Range(enemyAI.instance2.minPos.x, enemyAI.instance2.maxPos.x), Random.Range(enemyAI.instance2.minPos.y, enemyAI.instance2.maxPos.y));
            timeNumber = 0;
            Debug.Log($"{End}");
        }

        if (detect)
        {
            //sodem += Time.deltaTime;
            //if (sodem >= 5)
            //{
            //    transform.Rotate(new Vector3(0, 0, Random.Range(0, 180)));
            //    sodem = 0;
            //}

            speed = 5f;
            PlayAninmation(moving);
        }
        else
        {
            speed = 8f;
            PlayAninmation(upSpeed);
        }

    }
    //private void ChasePlayer(Vector2 direction)
    //{
    //    rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
    //}

  
    public bool check = false;

    public Vector2 ScaleCurrently { get => scaleCurrently; set => scaleCurrently = value; }
    

    public void autoMove()
    {
        // if (!detect)
        // {
        if (!check)
        {
            if (Vector3.Distance(transform.position, End) <= 0.1f)
            {
                check = true;
            }
            else
            {
                Vector2 lookdir = (End - rb.position);
                float angle = Mathf.Atan2(lookdir.y, lookdir.x) * Mathf.Rad2Deg;
                var lookRotation = Quaternion.Euler(angle * Vector3.forward);
                // transform.rotation = lookRotation;
                transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
                transform.localScale = transform.position.x < End.x ? ScaleCurrently : new Vector2(ScaleCurrently.x, -ScaleCurrently.y);

                lookdir.Normalize();
                movement = lookdir;
                transform.position = Vector3.MoveTowards(transform.position, End, Time.deltaTime * speed);
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, Vstart) <= 0.1f)
            {
                check = false;
            }
            else
            {
                Vector2 lookdir = (Vstart - rb.position);
                float angle = Mathf.Atan2(lookdir.y, lookdir.x) * Mathf.Rad2Deg;
                var lookRotation = Quaternion.Euler(angle * Vector3.forward);
                // transform.rotation = lookRotation;
                transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 10);
                transform.localScale = transform.position.x < Vstart.x ? ScaleCurrently : new Vector2(ScaleCurrently.x, -ScaleCurrently.y);

                lookdir.Normalize();
                movement = lookdir;
                transform.position = Vector3.MoveTowards(transform.position, Vstart, Time.deltaTime * speed);
            }
        }
        //  }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Wepon"))
        {
            bossDie = true;
            Cricle.enabled = false;
            GameObject item = transform.position.x > 0 ? Instantiate(itemPlayfab, transform.position + Vector3.one, Quaternion.identity) : Instantiate(itemPlayfab, transform.position - Vector3.one, Quaternion.identity);
            PlayAninmation(die);
            Wepons[n].SetActive(false);

            Destroy(gameObject, 0.5f);

        }
    }

}
