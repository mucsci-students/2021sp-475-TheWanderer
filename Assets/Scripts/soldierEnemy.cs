using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soldierEnemy : MonoBehaviour
{
    Vector3 initialPosition;
    private GameObject Wanderer;
    private GameObject Archer;
    private GameObject UnknownSoldier;

    public GameObject gameObj;
    ProgressBar Health;

    //Stats
    public float speed = 5.0f;
    public float jumpAmount =  20;
    public int HealthPoints = 100;
    public int atk;
    private float move;
    private bool enableJump;

    //Body Components
    Rigidbody2D rb;
    Animator animator;

    //Attack System
    public Transform attackPoint;
    public Transform sight;
    public LayerMask playerLayers;

    //Sound system
    public AudioSource attack;
    public AudioSource damaged;

    public float agroRange;
    public float attackDistance = 0.7f;
    public float attackRange = 0.7f;
    public float attackRate = 1f;
    private float nextAttackTime;
    private float deathTimer;
    private float walkTimer;
    private float moveHold;

    bool isFacingLeft;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Health = gameObj.GetComponent<ProgressBar>();
        nextAttackTime = 0f;
        enableJump = false;
        walkTimer = 0.0f;
        move = 0.08f;
        moveHold = move;
    }

    // Update is called once per frame
    void Update()
    {
        animator.speed = 3f;
        Health.UpdateValue(HealthPoints);
        walkTimer += Time.deltaTime;
        
        transform.Translate(move, 0.0f, 0.0f);
        animator.SetFloat("Speed", Mathf.Abs(move));

        if(HealthPoints <= 0)
        {
            deathTimer += Time.deltaTime;
            atk = 0;
            Stop();
            StopChasingPlayer();
            if(deathTimer >= 2f)
            {
                destroy();
            }
            
        }

        if(move < 0)
        {
            isFacingLeft = true;
        }
        else
        {
            isFacingLeft = false;
        }

        if(CanSeePlayer(attackDistance))
        {
            if(Time.time >= nextAttackTime)
            {
                StopChasingPlayer();
                Stop(); 
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
        else if(CanSeePlayer(agroRange) && HealthPoints >= 1)
        {
            ChasePlayer();
        }
       
        else if(!CanSeePlayer(agroRange) && !CanSeePlayer(attackDistance) && HealthPoints >= 1)
        {
            StopChasingPlayer();
            animator.SetTrigger("stopTrigger");  
            if(moveHold >= 0.0f && move == 0.0f && HealthPoints >= 1)
            {
                move = 0.08f;
            }
            else if (moveHold <= 0.0f && move == 0.0f && HealthPoints >= 1)
            {
                move = -0.08f;
            }
            
            
        }
       
        if (walkTimer >= Random.Range(3f,8f) && !CanSeePlayer(agroRange))
        {
            shiftDirection();
            walkTimer = 0;
        }

        if(rb.velocity.y != 0.0f) 
        {
            animator.speed = 2f;
            animator.SetBool("isJumping", true);
        } 
        else if(rb.velocity.y == 0.0f)
        {
            onLanding();
        } 

        if (move != 0.0f)
        {
            animator.speed = 100f;
            Vector3 scaleTemp = transform.localScale;
            if(move > 0.0f)
                scaleTemp.x = Mathf.Abs(scaleTemp.x);
            else
                scaleTemp.x = -Mathf.Abs(scaleTemp.x);
            transform.localScale = scaleTemp;
        }
        
    }
       
    void Stop()
    {
        moveHold = move;
        move = 0f;
    }
    
    void OnCollisionEnter2D (Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Land"))
        {
            enableJump = true;
        }
        else if(collider.gameObject.CompareTag("water"))
        {
            takeDamage(5);
            transform.position = initialPosition;
            
        }
        else if(collider.gameObject.CompareTag("border"))
        {
            shiftDirection();
        }
        else if(collider.gameObject.CompareTag("border2"))
        {
            shiftDirection();
        }
        
    }

    void Attack()
    {
        //Play animation
        attack.Play();
        animator.SetTrigger("attackTrigger");  
        animator.SetTrigger("stopTrigger");
        //Detect enemies in range
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);

        //Damage them
        foreach(Collider2D player in hitPlayer)
        {
            if(player.gameObject.CompareTag("wanderer"))
            {
                player.GetComponent<Wanderer>().takeDamage(atk);
            }
            else if(player.gameObject.CompareTag("archer"))
            {
                player.GetComponent<Archer>().takeDamage(atk);
            }
            else if(player.gameObject.CompareTag("unknownSoldier"))
            {
                player.GetComponent<UnknownSoldier>().takeDamage(atk);
            }
        }
    }

    void shiftDirection()
    {
        move = -move;
    }

    void ChasePlayer()
    {
        if(!isFacingLeft)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }
    }

    void StopChasingPlayer()
    {
        rb.velocity = new Vector2(0, 0);
    }

    public void onLanding()
    {
        animator.SetBool("isJumping", false);
    }

    bool CanSeePlayer(float distance)
    {
        bool val = false;
        var castDist = distance;

        if(isFacingLeft)
        {
            castDist = -distance;
        }
        Vector2 endPos = sight.position + Vector3.right * castDist;
        RaycastHit2D hit = Physics2D.Linecast(sight.position, endPos, playerLayers);

        if(hit.collider != null)
        {
            if(hit.collider.gameObject.CompareTag("wanderer") || hit.collider.gameObject.CompareTag("archer")
            || hit.collider.gameObject.CompareTag("unknownSoldier"))
            {
              val = true;
            }
            else
            {
                val = false;
                move = moveHold;
            }
        }
        return val;
    }

    public void takeDamage(int damage)
    {
        damaged.Play();
        HealthPoints = HealthPoints - damage;
        animator.SetTrigger("hitTrigger");
        animator.SetFloat("health", HealthPoints);
    }

    public void destroy()
    {
        Destroy(gameObject);
    }

}
