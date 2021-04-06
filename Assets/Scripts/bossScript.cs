using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossScript : MonoBehaviour
{
    //Spawn Locations
    public Transform LocationOne;
    public Transform LocationTwo;
    public Transform LocationThree;
    public Transform LocationFour;

    public GameObject wanderer;
    public GameObject archer;
    public GameObject unknownSoldier;

    //Revial Mechanics
    public GameObject spiritOne;
    public GameObject spiritTwo;
    public GameObject spiritThree;
    public GameObject spiritFour;

    public GameObject spawner;
    public GameObject warning;

    bossScript2 oneR;
    bossScript2 twoR;
    bossScript2 threeR;
    bossScript2 fourR;


    public GameObject gameObj;
    ProgressBar Health;

    public GameObject fireball;
    private fireballScript fb;

    //Stats
    public float speed;
    public int HealthPoints = 100;
    public int atk;

    //Body Components
    Rigidbody2D rb;
    Animator animator;

    //Attack System
    public Transform sight;
    public Transform attackPoint;
    public LayerMask playerLayers;

    //Sound system
    public AudioSource attack;
    public AudioSource damaged;
    public AudioSource fireAttack;
    public AudioSource teleprt;


    public float sightRange = 1f;
    public float attackRange = 0.7f;
    public float attackRate = 1f;
    private float nextAttackTime;
    private float teleport;
    private int randomNumber = 0;
    private int previousNumber = 0;
    public float reviveTimer;
    private int hit;
    private int hitGoal = 3;
    bool isFacingLeft;
    private bool phaseTwo;

    // Start is called before the first frame update
    void Start()
    {
        phaseTwo = false;
        teleport = 0f;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Health = gameObj.GetComponent<ProgressBar>();

        oneR = spiritOne.GetComponent<bossScript2>();
        twoR = spiritTwo.GetComponent<bossScript2>();
        threeR = spiritThree.GetComponent<bossScript2>();
        fourR = spiritFour.GetComponent<bossScript2>();

        fb = fireball.GetComponent<fireballScript>();
        fb.speed = Mathf.Abs(fb.speed);

        transform.position = LocationOne.transform.position;
        reviveTimer = 25;

    }

    // Update is called once per frame
    void Update()
    {
        Health.UpdateValue(HealthPoints);
        teleport += Time.deltaTime;
        if(HealthPoints == 100)
        {
            teleport = 0;
        }

        if(isFacingLeft)
        {
            Vector3 scaleTemp = transform.localScale;
            scaleTemp.x = -Mathf.Abs(scaleTemp.x);
            transform.localScale = scaleTemp;
            fb.speed = -Mathf.Abs(fb.speed);
        }
        else
        {
            Vector3 scaleTemp = transform.localScale;
            scaleTemp.x = Mathf.Abs(scaleTemp.x);
            transform.localScale = scaleTemp;
            fb.speed = Mathf.Abs(fb.speed);
        }

        if (reviveTimer <= 0 && spiritOne.gameObject.activeInHierarchy == false && spiritTwo.gameObject.activeInHierarchy == false 
                             && spiritTwo.gameObject.activeInHierarchy == false && spiritFour.gameObject.activeInHierarchy == false)
        {
            //Destroy(spawner);
            Destroy(warning);
            Destroy(this.gameObject);
        }
        else if(reviveTimer <= 0)
        {
            reviveBoss();
            reviveTimer = 25;
        }

        if(!phaseTwo)
        {
            if(teleport >= 15f && HealthPoints >= 1 || hit == 3 && HealthPoints >= 1)
            {
                Teleport();
                hit = 0;
            }
        }
        else
        {
            if(teleport >= 10f && HealthPoints >= 1 || hit == 3 && HealthPoints >= 1)
            {
                Teleport();
                hit = 0;
            }
        }

        if(transform.position == LocationOne.position && HealthPoints >= 1 || transform.position == LocationTwo.position && HealthPoints >= 1)
        {
            if(CanSeePlayer(sightRange))
            {
                if(Time.time >= nextAttackTime)
                {
                    Attack();
                    nextAttackTime = Time.time + 1f / attackRate;
                }
            }

        }
        else if(transform.position == LocationThree.position && HealthPoints >= 1 || transform.position == LocationFour.position && HealthPoints >= 1)
        {
            if(Time.time >= nextAttackTime)
            {
                if(phaseTwo)
                {
                    animator.SetTrigger("attackTrigger");  
                    fireAttack.Play();
                    if(fb.speed > 0)
                    {
                        Instantiate(fireball, new Vector3(transform.position.x+ 4f, transform. position.y-1f, -5f), Quaternion.identity);
                        Instantiate(fireball, new Vector3(transform.position.x+ 2f, transform. position.y-1f, -5f), Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(fireball, new Vector3(transform.position.x+ 4f, transform. position.y-1f, -5f), Quaternion.Euler(new Vector3(0f, 180f, 0f)));
                        Instantiate(fireball, new Vector3(transform.position.x+ 2f, transform. position.y-1f, -5f), Quaternion.Euler(new Vector3(0f, 180f, 0f)));
                    }
                    nextAttackTime = Time.time + 1f / attackRate;
                }
                else
                {
                    animator.SetTrigger("attackTrigger");  
                    if(fb.speed > 0)
                        Instantiate(fireball, new Vector3(transform.position.x+ 2f, transform. position.y-1f, -5f), Quaternion.identity);
                    else
                        Instantiate(fireball, new Vector3(transform.position.x+ 2f, transform. position.y-1f, -5f), Quaternion.Euler(new Vector3(0f, 180f, 0f)));
                    nextAttackTime = Time.time + 1f / attackRate;
                }
                
            }
           
        }

        if(HealthPoints <= 50 && HealthPoints >= 1)
        {
            phaseTwo = true;
            spawner.SetActive(true);
        }

        if(HealthPoints <= 0)
        {
            if(oneR.HealthPoints >= 1)
                spiritOne.SetActive(true);
            if(twoR.HealthPoints >= 1)
                spiritTwo.SetActive(true);
            if(threeR.HealthPoints >= 1)
                spiritThree.SetActive(true);
            if(fourR.HealthPoints >= 1)
                spiritFour.SetActive(true);
            reviveTimer -= Time.deltaTime;
            Debug.Log(reviveTimer);
            gameObj.SetActive(false);
            warning.SetActive(true);
        }

        if(wanderer.gameObject.activeInHierarchy == true)
        {
            if(transform.position.x > wanderer.transform.position.x)
            {
                isFacingLeft = true;
            }
            else
            {
                isFacingLeft = false;
            }
        }
        else if(archer.gameObject.activeInHierarchy == true)
        {
            if(transform.position.x > archer.transform.position.x)
            {
                isFacingLeft = true;
            }
            else
            {
                isFacingLeft = false;
            }
        }
        else if(unknownSoldier.gameObject.activeInHierarchy == true)
        {
            if(transform.position.x > unknownSoldier.transform.position.x)
            {
                isFacingLeft = true;
            }
            else
            {
                isFacingLeft = false;
            }
        }
    }

    void Attack()
    {
        attack.Play();
        //Play animation
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
    private void Teleport()
    {
        randomNumber = Random.Range(1,5);
        if(randomNumber == previousNumber)
        {
            if(randomNumber == 1)
            {
                randomNumber = 2;
            }
            else if(randomNumber == 2)
            {
                randomNumber = 3;
            }
            else if(randomNumber == 3)
            {
                randomNumber = 4;
            }
            else if(randomNumber == 4)
            {
                randomNumber = 1;
            }
        }
        teleprt.Play();
        if(randomNumber == 1)
        {
            transform.position = LocationOne.transform.position;
        }
        else if(randomNumber == 2)
        {
            transform.position = LocationTwo.transform.position;
        }
        else if(randomNumber == 3)
        {
            transform.position = LocationThree.transform.position;
        }
        else if(randomNumber == 4)
        {
            transform.position = LocationFour.transform.position;
        }
        previousNumber = randomNumber;
        teleport = 0.0f; 
        Debug.Log(randomNumber);
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
            }
        }
        return val;
    }
    public void takeDamage(int damage)
    {
        damaged.Play();
        hit++;
        if(!phaseTwo)
        {
            damage = damage-32;
        }
        else 
        {
            damage = damage-27;
        }
        HealthPoints = HealthPoints - damage;
        animator.SetTrigger("hitTrigger");
        animator.SetFloat("health", HealthPoints);
    }
    
    private void reviveBoss()
    {
        if(spiritOne.gameObject.activeInHierarchy == true)
        {
            spiritOne.SetActive(false);
        }
        if(spiritTwo.gameObject.activeInHierarchy == true)
        {
            spiritTwo.SetActive(false);
        }
        if(spiritThree.gameObject.activeInHierarchy == true)
        {
            spiritThree.SetActive(false);
        }
        if(spiritFour.gameObject.activeInHierarchy == true)
        {
            spiritFour.SetActive(false);
        }
        atk+=10;
        HealthPoints = 25;
        animator.SetFloat("health", HealthPoints);
        animator.SetTrigger("reviveTrigger");  
        gameObj.SetActive(true);
        warning.SetActive(false);
    }
}
