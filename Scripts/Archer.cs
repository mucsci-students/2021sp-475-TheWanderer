using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{
    Vector3 initialPosition;
    //HealthBar objects
    public GameObject gameObj;
    ProgressBar Health;
    //Arrow Objects
    public GameObject arrow;
    private arrowScript ar;
    public GameObject border1;
    public GameObject border2;

    //Stats
    public float speed = 10.0f;
    public float jumpAmount = 25;
    public int HealthPoints = 80;
    private float move;
    public bool enableJump;
    private GameObject teleport;

    //Body Components
    public RuntimeAnimatorController archer;
    Rigidbody2D rb;
    Animator animator;
    private SpriteRenderer sr;

    //Attack System
    private float attackRate = 2f;
    private  float nextAttackTime = 0f;

    //Sound system
    public AudioSource attack;
    public AudioSource damaged;
    public AudioSource land;

    //Power up
    private float timer = 0;
    private float seconds = 20;
    public bool powerUp = false;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Health = gameObj.GetComponent<ProgressBar>();
        ar = arrow.GetComponent<arrowScript>();
        ar.speed = Mathf.Abs(ar.speed);
        sr = GetComponent<SpriteRenderer>();

        enableJump = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (powerUp)
        {
            sr.color = Color.red;
        }
        else
        {
            sr.color = Color.white;
        }
        Health.UpdateValue(HealthPoints);
        move = 0.0f;


        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            move = -speed;
            ar.speed = -Mathf.Abs(ar.speed);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            move = speed;
            ar.speed = Mathf.Abs(ar.speed);
        }

        if (enableJump && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddRelativeForce(new Vector3(0f, jumpAmount, 0f), ForceMode2D.Impulse);
            enableJump = false;
        }
        else if (Input.GetKey(KeyCode.Mouse1))
        {
            if(GameObject.FindGameObjectWithTag("arrow"))
            {
                teleport = GameObject.FindGameObjectWithTag("arrow");
                if(teleport.transform.position.x > border1.transform.position.x && teleport.transform.position.x < border2.transform.position.x)
                    transform.position = new Vector3(teleport.transform.position.x, transform.position.y, teleport.transform.position.z); 
            }
        }

        transform.Translate(move * Time.deltaTime, 0.0f, 0.0f);
        animator.speed = 1.0f;

        if (rb.velocity.y != 0.0f)
        {
            animator.SetBool("isJumping", true);
        }
        else if (rb.velocity.y == 0.0f)
        {
            onLanding();
        }

        if (move != 0.0f)
        {
            Vector3 scaleTemp = transform.localScale;
            if (move > 0.0f)
                scaleTemp.x = Mathf.Abs(scaleTemp.x);
            else
                scaleTemp.x = -Mathf.Abs(scaleTemp.x);
            transform.localScale = scaleTemp;
        }

        if(Time.time >= nextAttackTime)
        {
            if(Input.GetKeyDown(KeyCode.Mouse0) && move == 0.0f)
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
                
        }
        animator.SetFloat("Speed", Mathf.Abs(move));

        if (powerUp)
        {
            timer += Time.deltaTime;
            if (timer > seconds)
            {
                timer = 0;
                powerUp = false;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Land"))
        {
            land.Play();
            enableJump = true;
        }
        else if(collider.gameObject.CompareTag("water"))
        {
            takeDamage(5);
            transform.position = initialPosition;
            
        }

        if (collider.gameObject.CompareTag("health"))
        {
            if (HealthPoints <= 60)
            {
                HealthPoints += 20;
            }
            else
            {
                HealthPoints = 80;
            }
            Destroy(collider.gameObject);
        }

        if (collider.gameObject.CompareTag("attackUp"))
        {
            timer = 0;
            PowerUp();
            Destroy(collider.gameObject);
        }
    }

    void PowerUp()
    {
        powerUp = true;
    }

    void Attack()
    {
        //Play animation
        animator.SetTrigger("attackTrigger"); 
        animator.SetTrigger("stopTrigger");
        if(ar.speed > 0)
            Instantiate(arrow, new Vector3(transform.position.x+ 2f, transform. position.y-1f, -5f), Quaternion.identity);
        else
            Instantiate(arrow, new Vector3(transform.position.x+ 2f, transform. position.y-1f, -5f), Quaternion.Euler(new Vector3(0f, 180f, 0f)));
        animator.SetTrigger("stopTrigger");
            
    }
    private void onLanding()
    {
        animator.SetBool("isJumping", false);
    }

    public void takeDamage(int damage)
    {
        damaged.Play();
        HealthPoints = HealthPoints - damage;
        animator.SetTrigger("hitTrigger");
        animator.SetFloat("health", HealthPoints);
    }
}
