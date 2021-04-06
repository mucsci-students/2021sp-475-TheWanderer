using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wanderer : MonoBehaviour
{
    Vector3 initialPosition;
    //HealthBar objects
    public GameObject gameObj;
    ProgressBar Health;

    //Stats
    public float speed = 15.0f;
    public float jumpAmount =  25;
    public int HealthPoints = 100;
    public int atk;
    private float move;
    public bool enableJump;

    //Body Components
    public RuntimeAnimatorController wanderer;
    Rigidbody2D rb;
    Animator animator;
    private SpriteRenderer sr;

    //Attack System
    public Transform attackPoint;
    public LayerMask enemyLayers;
    private float attackRange = 3f;
    private float attackRate = 2f;
    private float nextAttackTime = 0f;
    private bool isFacingLeft;

    //Sound system
    public AudioSource attack;
    public AudioSource damaged;
    public AudioSource land;

    //Power Up
    private float timer = 0;
    private float seconds = 20;
    private bool powerUp = false;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Health = gameObj.GetComponent<ProgressBar>();
        sr = GetComponent<SpriteRenderer>();

        enableJump = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (powerUp)
        {
            atk = 50;
            sr.color = Color.red;
        }
        else
        {
            atk = 35;
            sr.color = Color.white;
        }

        Health.UpdateValue(HealthPoints);
        move = 0.0f;
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) 
        {
            move = -speed;
            isFacingLeft = true;
           
        }
        else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) 
        {
            move = speed; 
            isFacingLeft = false;
        }

        if(enableJump && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddRelativeForce(new Vector3(0f, jumpAmount, 0f), ForceMode2D.Impulse);
            enableJump = false;
        }
        else if(Input.GetKey(KeyCode.Mouse1))
        {
            //animator.speed = 3f;
            move  = move*3;
        }

        transform.Translate(move * Time.deltaTime, 0.0f, 0.0f);
        animator.speed = 1.0f;

        if(rb.velocity.y != 0.0f) {
            animator.speed = 2f;
            animator.SetBool("isJumping", true);
        } 
        else if(rb.velocity.y == 0.0f)
        {
            onLanding();
        } 

        if (move != 0.0f)
        {
            Vector3 scaleTemp = transform.localScale;
            if(move > 0.0f)
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
    
    void OnCollisionEnter2D (Collision2D collider)
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
            if (HealthPoints <= 80)
            {
                HealthPoints += 20;
            }
            else if (HealthPoints > 80)
            {
                HealthPoints = 100;
            }
            Destroy(collider.gameObject);
        }

        if (collider.gameObject.CompareTag("attackUp"))
        { 
            PowerUp();

            Destroy(collider.gameObject);
        }
    }

    void PowerUp()
    {
        timer = 0;
        powerUp = true;
    }

    void Attack()
    {
        attack.Play();
        //Play animation
        animator.SetTrigger("attackTrigger");  
        animator.SetTrigger("stopTrigger");
        //Detect enemies in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //Damage them
        foreach(Collider2D enemy in hitEnemies)
        {
            if(enemy.GetComponent<enemyScript>() != null)
            {
               enemy.GetComponent<enemyScript>().takeDamage(atk);
                if (isFacingLeft)
                    enemy.GetComponent<Rigidbody2D>().AddForce(new Vector3(-10000f,0,0f));
                else
                    enemy.GetComponent<Rigidbody2D>().AddForce(new Vector3(10000f,0,0f));
                Debug.Log("We hit "+ enemy.name); 
            }
            else if(enemy.GetComponent<enemyScript2>() != null)
            {
                enemy.GetComponent<enemyScript2>().takeDamage(atk);
                if (isFacingLeft &&  !enemy.GetComponent<enemyScript2>().animator.GetBool("isGuarding"))
                    enemy.GetComponent<Rigidbody2D>().AddForce(new Vector3(-10000f,0,0f));
                else if(!isFacingLeft && !enemy.GetComponent<enemyScript2>().animator.GetBool("isGuarding"))
                    enemy.GetComponent<Rigidbody2D>().AddForce(new Vector3(10000f,0,0f));
                Debug.Log("We hit "+ enemy.name); 
            }
            else if(enemy.GetComponent<bossScript>() != null)
            {
                enemy.GetComponent<bossScript>().takeDamage(atk);
            }
            else if(enemy.GetComponent<bossScript2>() != null)
            {
                enemy.GetComponent<bossScript2>().takeDamage(atk);
            }
            else if(enemy.GetComponent<archerEnemy>() != null)
            {
                enemy.GetComponent<archerEnemy>().takeDamage(atk);
            }
            else if(enemy.GetComponent<soldierEnemy>() != null)
            {
                enemy.GetComponent<soldierEnemy>().takeDamage(atk);
            }
            
        }
    }

    public void onLanding()
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
