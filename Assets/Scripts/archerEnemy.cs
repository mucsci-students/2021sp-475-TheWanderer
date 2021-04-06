using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class archerEnemy : MonoBehaviour
{
    Vector3 initialPosition;
    private GameObject Wanderer;
    private GameObject Archer;
    private GameObject UnknownSoldier;

    public GameObject gameObj;
    ProgressBar Health;

    public GameObject arrow;
    private arrowEnemy ar;

    //Stats
    public float speed = 5.0f;
    public float jumpAmount =  20;
    public int HealthPoints = 100;
    private float move;
    private bool enableJump;

    //Body Components
    Rigidbody2D rb;
    Animator animator;

    //Attack System
    public Transform sight;
    public LayerMask playerLayers;

    //Sound system
    public AudioSource attack;
    public AudioSource damaged;

    public float attackDistance = 0.7f;
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
        ar = arrow.GetComponent<arrowEnemy>();
        ar.speed = Mathf.Abs(ar.speed);
        nextAttackTime = 0f;
        enableJump = false;
        walkTimer = 0.0f;
        move = 0.08f;
        moveHold = move;
    }

    // Update is called once per frame
    void Update()
    {
        Health.UpdateValue(HealthPoints);
        walkTimer += Time.deltaTime;
        
        transform.Translate(move, 0.0f, 0.0f);
        animator.SetFloat("Speed", Mathf.Abs(move));

        if(HealthPoints <= 0)
        {
            deathTimer += Time.deltaTime;
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
            ar.speed = -Mathf.Abs(ar.speed);
        }
        else
        {
            isFacingLeft = false;
            ar.speed = Mathf.Abs(ar.speed);
        }

        if(CanSeePlayer(attackDistance))
        {
            if(Time.time >= nextAttackTime)
            {
                //Stop();
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
       
       
        if (walkTimer >= 2f)
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
        
    }

    void Attack()
    {
        attack.Play();
        //Play animation
        animator.SetTrigger("attackTrigger"); 
        animator.SetTrigger("stopTrigger");
        if(ar.speed > 0)
            Instantiate(arrow, new Vector3(transform.position.x+ 2f, transform. position.y-1f, -5f), Quaternion.identity);
        else
            Instantiate(arrow, new Vector3(transform.position.x+ 2f, transform. position.y-1f, -5f), Quaternion.Euler(new Vector3(0f, 180f, 0f)));
        animator.SetTrigger("stopTrigger");
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
